using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FIAPCloudGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _config;

        public AuthController(UserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.AuthenticateAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized(new { error = "Usu�rio ou senha inv�lidos." });

            var jwtKey = _config["Jwt:Key"] ?? "super_secret_key_123!";
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
