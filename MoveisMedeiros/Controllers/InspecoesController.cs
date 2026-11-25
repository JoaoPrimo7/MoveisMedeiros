using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Data;
using MoveisMedeiros.DTOs;
using MoveisMedeiros.Models;

namespace MoveisMedeiros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InspecoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InspecoesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListarHistorico()
        {
            var historico = await _context.Inspecoes
                .Include(i => i.Obra)
                .OrderByDescending(i => i.DataInspecao)
                .Select(i => new
                {
                    i.Id,
                    NomeObra = i.Obra != null ? i.Obra.NomeObra : "Obra removida",
                    DataInspecao = i.DataInspecao.ToString("dd/MM/yyyy 'às' HH:mm"),
                    i.Montagem,
                    i.Acabamento,
                    i.Ferragens,
                    i.Limpeza,
                    i.Observacoes
                })
                .ToListAsync();

            return Ok(historico);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarInspecao([FromBody] InspecaoDto dto)
        {
            var obraExiste = await _context.Obras.AnyAsync(o => o.Id == dto.ObraId);
            if (!obraExiste)
                return NotFound(new { mensagem = "Obra não encontrada." });

            var inspecao = new Inspecao
            {
                ObraId = dto.ObraId,
                DataInspecao = DateTime.Now,
                Montagem = dto.Montagem,
                Acabamento = dto.Acabamento,
                Ferragens = dto.Ferragens,
                Limpeza = dto.Limpeza,
                Observacoes = dto.Observacoes
            };

            _context.Inspecoes.Add(inspecao);
            await _context.SaveChangesAsync();

            return Ok(inspecao);
        }
    }
}