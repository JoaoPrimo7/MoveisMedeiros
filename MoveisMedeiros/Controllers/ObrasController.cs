using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Data;
using MoveisMedeiros.DTOs;
using MoveisMedeiros.Models;

namespace MoveisMedeiros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObrasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ObrasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListarObras()
        {
            var obras = await _context.Obras
                .OrderByDescending(o => o.Id)
                .ToListAsync();
            return Ok(obras);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarObra([FromBody] ObraDto dto)
        {
            var novaObra = new Obra
            {
                NomeCliente = dto.NomeCliente,
                NomeObra = dto.NomeObra,
                LocalObra = dto.LocalObra,
                PrazoEntrega = dto.PrazoEntrega
            };

            _context.Obras.Add(novaObra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ListarObras), new { id = novaObra.Id }, novaObra);
        }
    }
}