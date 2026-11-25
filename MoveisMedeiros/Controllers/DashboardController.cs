using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Data;

namespace MoveisMedeiros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetResumo()
        {
            var obrasCadastradas = await _context.Obras.CountAsync();
            var vistoriasRealizadas = await _context.Inspecoes.CountAsync();

            
            var avisosPendentes = await _context.Inspecoes.CountAsync(i =>
                i.Montagem != "Conforme" ||
                i.Acabamento != "Conforme" ||
                i.Ferragens != "Conforme" ||
                i.Limpeza != "Conforme");

            return Ok(new
            {
                obras = obrasCadastradas,
                vistorias = vistoriasRealizadas,
                avisos = avisosPendentes
            });
        }
    }
}