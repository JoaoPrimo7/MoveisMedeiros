using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Api.DTOs;
using MoveisMedeiros.DTOs;
using MoveisMedeiros.Models;

namespace MoveisMedeiros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ObrasController : ControllerBase
{
    private readonly AppDbContext _db;

    public ObrasController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var obras = await _db.Obras
            .OrderByDescending(x => x.CriadaEm)
            .Select(x => new
            {
                x.Id,
                x.NomeCliente,
                x.NomeObra,
                x.Local,
                x.PrazoEntrega,
                TotalInspecoes = x.Inspecoes.Count
            })
            .ToListAsync();

        return Ok(obras);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Buscar(int id)
    {
        var obra = await _db.Obras
            .Include(x => x.Inspecoes)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (obra == null)
            return NotFound(new
            {
                mensagem = "Obra não encontrada."
            });

        return Ok(obra);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(ObraDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.NomeCliente) ||
            string.IsNullOrWhiteSpace(dto.NomeObra) ||
            string.IsNullOrWhiteSpace(dto.Local))
        {
            return BadRequest(new
            {
                mensagem = "Preencha cliente, nome da obra e local."
            });
        }

        var obra = new Obra
        {
            NomeCliente = dto.NomeCliente.Trim(),
            NomeObra = dto.NomeObra.Trim(),
            Local = dto.Local.Trim(),
            PrazoEntrega = dto.PrazoEntrega.Date
        };

        _db.Obras.Add(obra);

        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Buscar),
            new { id = obra.Id },
            obra
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(
        int id,
        ObraDto dto)
    {
        var obra = await _db.Obras.FindAsync(id);

        if (obra == null)
            return NotFound();

        obra.NomeCliente = dto.NomeCliente.Trim();
        obra.NomeObra = dto.NomeObra.Trim();
        obra.Local = dto.Local.Trim();
        obra.PrazoEntrega = dto.PrazoEntrega.Date;

        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var obra = await _db.Obras.FindAsync(id);

        if (obra == null)
            return NotFound();

        _db.Obras.Remove(obra);

        await _db.SaveChangesAsync();

        return NoContent();
    }
}