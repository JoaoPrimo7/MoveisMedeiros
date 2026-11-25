using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Data;
using MoveisMedeiros.DTOs;
using MoveisMedeiros.Services;

namespace MoveisMedeiros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Senha == dto.Senha);

            if (usuario == null)
                return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

            var token = _tokenService.GerarToken(usuario);
            return Ok(new { token, usuario = new { usuario.Id, usuario.Nome, usuario.Email } });
        }
    }
}