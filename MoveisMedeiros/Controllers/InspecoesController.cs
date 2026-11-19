using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.Models;

namespace MoveisMedeiros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InspecoesController : ControllerBase
{
    private readonly AppDbContext _db;

    public InspecoesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var lista = await _db.Inspecoes
            .Include(x => x.Obra)
            .Include(x => x.Itens)
            .OrderByDescending(x => x.DataInspecao)
            .Select(x => new
            {
                x.Id,
                x.ObraId,
                Obra = x.Obra!.NomeObra,
                x.DataInspecao,
                x.Observacoes,
                Itens = x.Itens
            })
            .ToListAsync();

        return Ok(lista);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(InspecaoDto dto)
    {
        var obraExiste = await _db.Obras
            .AnyAsync(x => x.Id == dto.ObraId);

        if (!obraExiste)
        {
            return BadRequest(new
            {
                mensagem = "Selecione uma obra válida."
            });
        }

        var categoriasPermitidas = new[]
        {
            "Montagem dos móveis",
            "Acabamento",
            "Portas e ferragens",
            "Limpeza do local"
        };

        var statusPermitidos = new[]
        {
            "Conforme",
            "Inconforme",
            "Ajustar"
        };

        if (dto.Itens == null ||
            dto.Itens.Count != categoriasPermitidas.Length ||
            dto.Itens.Any(i =>
                !categoriasPermitidas.Contains(i.Categoria) ||
                !statusPermitidos.Contains(i.Status)))
        {
            return BadRequest(new
            {
                mensagem = "Confira os itens e status do checklist."
            });
        }

        var inspecao = new Inspecao
        {
            ObraId = dto.ObraId,

            Observacoes = dto.Observacoes?.Trim() ?? "",

            DataInspecao = DateTime.Now,

            Itens = dto.Itens.Select(i => new ItemInspecao
            {
                Categoria = i.Categoria,
                Status = i.Status
            }).ToList()
        };

        _db.Inspecoes.Add(inspecao);

        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Listar),
            new { id = inspecao.Id },
            inspecao
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var inspecao = await _db.Inspecoes.FindAsync(id);

        if (inspecao == null)
            return NotFound();

        _db.Inspecoes.Remove(inspecao);

        await _db.SaveChangesAsync();

        return NoContent();
    }
}