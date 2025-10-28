using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FIAPCloudGames.API.Controllers
{
    /// <summary>
    /// Endpoints de autenticação e emissão de token JWT.
    /// </summary>
    /// <remarks>
    /// Use este controller para autenticar um usuário e obter um token JWT.
    /// 
    /// O token deve ser enviado no header Authorization dos próximos requests:
    /// 
    /// Authorization: Bearer {seu_token}
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    public class AuthController(IUserService userService, IConfiguration config) : ControllerBase
    {
        /// <summary>
        /// Autentica um usuário e retorna o token JWT.
        /// </summary>
        /// <param name="dto">Credenciais de login (email e senha).</param>
        /// <returns>Token JWT para acesso aos endpoints protegidos.</returns>
        /// <remarks>
        /// Exemplo de request:
        /// 
        /// {
        ///   "email": "user@fiap.com",
        ///   "password": "Senha@123"
        /// }
        /// 
        /// Respostas possíveis:
        /// - 200 OK: token emitido com sucesso
        /// - 401 Unauthorized: credenciais inválidas
        /// </remarks>
        [HttpPost("login")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await userService.AuthenticateAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized(new { error = "Usuário ou senha inválidos." });

            var jwtKey = config["Jwt:Key"] ?? "super_secret_key_123!";
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email?.Value ?? string.Empty)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString });
        }
    }
}
