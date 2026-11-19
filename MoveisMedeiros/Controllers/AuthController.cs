
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Api.Data;
using MoveisMedeiros.Models;
using MoveisMedeiros.DTOs;
namespace AuthController
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
       
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());

        
            if (usuario == null || usuario.Senha != loginDto.Senha)
            {
                return Unauthorized("Email ou senha inválidos.");
            }


            return Ok(new { Email = usuario.Email });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario registerDto)
        {
   
            if (await _context.Usuarios.AnyAsync(u => u.Email.ToLower() == registerDto.Email.ToLower()))
            {
                return BadRequest("Este Email já está em uso."); 
            }

            var novoUsuario = new Usuario
            {
                Email = registerDto.Email,
              
                Senha = registerDto.Senha
            };

        
            _context.Usuarios.Add(novoUsuario);


            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(Register), new { id = novoUsuario.Id }, new { novoUsuario.Email, novoUsuario.Senha });
        }
    }
}