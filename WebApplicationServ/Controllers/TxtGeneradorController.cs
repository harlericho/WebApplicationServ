using Microsoft.AspNetCore.Mvc;
using WebApplicationServ.Services;

namespace WebApplicationServ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TxtGeneradorController : Controller
    {
        private readonly TxtGeneradorService _service;

        public TxtGeneradorController(TxtGeneradorService service)
        {
            _service = service;
        }
        [HttpGet("generar")]
        public async Task<IActionResult> GenerarArchivoTxt()
        {
            var ruta = Path.Combine(Directory.GetCurrentDirectory(), "Generado.txt");
            var resultado = await _service.GenerarTxtAsync(ruta);
            return Ok(new { mensaje = resultado, ruta });
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
