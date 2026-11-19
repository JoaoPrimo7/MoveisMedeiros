using Microsoft.IdentityModel.Tokens;
using MoveisMedeiros.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MoveisMedeiros.Api.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GerarToken(Usuario usuario)
    {
        // Informações do usuário que ficarão dentro do token
        var claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, usuario.Nome));
        claims.Add(new Claim(ClaimTypes.Email, usuario.Email));

        // Chave usada para gerar o token
        var chave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        // Tipo de assinatura do token
        var credenciais = new SigningCredentials(
            chave,
            SecurityAlgorithms.HmacSha256
        );

        // Tempo de validade do token
        var validade = DateTime.UtcNow.AddHours(8);

        // Criação do token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: validade,
            signingCredentials: credenciais
        );

        // Transforma o token em texto
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}