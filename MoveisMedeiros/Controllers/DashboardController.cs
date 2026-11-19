using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;

namespace MoveisMedeiros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _db;

    public DashboardController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Resumo()
    {
        var obras = await _db.Obras.CountAsync();

        var inspecoes = await _db.Inspecoes.CountAsync();

        var avisos = await _db.ItensInspecao.CountAsync(x =>
            x.Status == "Inconforme" ||
            x.Status == "Ajustar");

        return Ok(new
        {
            obras,
            vistorias = inspecoes,
            avisos
        });
    }
}