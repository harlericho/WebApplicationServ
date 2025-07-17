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
            var query = from r in _context.RespuestasXEmpresa
                        join p in _context.Persos on r.Diseno equals p.BinId
                        where r.FechaRespuesta > r.Hora1
                           && r.FechaRespuesta > r.Hora2
                           && r.FechaRespuesta > r.Hora3
                           && r.FechaRespuesta > r.Hora4
                        select new { p.Id, p.BinId, p.Cliente, p.Fecha, r.FechaRespuesta };

            var datos = await query.ToListAsync();

            if (!datos.Any())
                return "No hay registros que cumplan la condición.";

            var sb = new StringBuilder();
            foreach (var item in datos)
            {
                sb.AppendLine($"ID: {item.Id}, BinID: {item.BinId}, Cliente: {item.Cliente}, FechaPerso: {item.Fecha}, FechaRespuesta: {item.FechaRespuesta}");
            }

            await File.WriteAllTextAsync(ruta, sb.ToString());

            _logger.LogInformation($"Archivo generado en: {ruta}");
            return $"Archivo generado con {datos.Count} registros.";
        }
    }
}
