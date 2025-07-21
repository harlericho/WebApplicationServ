using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApplicationServ.Data;

namespace WebApplicationServ.Services
{
    public class TxtGeneradorService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TxtGeneradorService> _logger;

        public TxtGeneradorService(AppDbContext context, ILogger<TxtGeneradorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string> GenerarTxtAsync(string ruta)
        {
            var query = (from r in _context.RespuestasXEmpresa
                         join p in _context.Persos on r.Diseno equals p.BinId
                         where r.FechaRespuesta > r.Hora1
                            && r.FechaRespuesta > r.Hora2
                            && r.FechaRespuesta > r.Hora3
                            && r.FechaRespuesta > r.Hora4
                         select new
                         {
                             p.Id,
                             p.BinId,
                             p.Cliente,
                             p.Fecha,
                             r.FechaRespuesta
                         }).Take(20);

            var datos = await query.ToListAsync();

            if (!datos.Any())
                return "No hay registros que cumplan la condición.";

            var fechaActual = DateTime.Now.ToString("yyyyMMdd");
            var totalConCabecera = datos.Count + 1;
            var sb = new StringBuilder();

            // Línea C: C	[fecha]	[total + 1]	+ 26 tabs
            sb.Append($"C\t{fechaActual}\t{1}");
            sb.Append(new string('\t', 26));
            sb.AppendLine();

            // Líneas S con tab después de cada campo
            foreach (var item in datos)
            {
                sb.Append("S\t");
                sb.Append(fechaActual).Append('\t');
                sb.Append(item.BinId).Append('\t');
                sb.Append(item.Cliente).Append('\t');
                sb.Append(item.Fecha).Append('\t');
                sb.Append(item.FechaRespuesta).Append('\t'); // <- tab final requerido
                sb.AppendLine();
            }

            // Línea ITEM: ITEM	[total + 1]	+ 27 tabs
            sb.Append($"ITEM\t{totalConCabecera}");
            sb.Append(new string('\t', 27));
            sb.AppendLine();

            await File.WriteAllTextAsync(ruta, sb.ToString());

            _logger.LogInformation($"Archivo generado en: {ruta}");
            return $"Archivo generado con {datos.Count} registros (más cabecera).";
        }
    }
}
