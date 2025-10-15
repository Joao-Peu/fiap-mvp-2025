using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.API.Controllers
{
    /// <summary>
    /// Gerencia opera��es de usu�rios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Cria um novo usu�rio.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(dto.Name, dto.Email, dto.Password);
                return Created($"/api/user/{user.Id}", user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Lista todos os usu�rios.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = (await _userService.GetAllAsync())
                .Select(u => new UserReadDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email?.Value

                });

            return Ok(users);
        }

        /// <summary>
        /// Busca usu�rio por ID.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email?.Value
            };

            return Ok(userDto);
        }

        /// <summary>
        /// Atualiza dados de um usu�rio.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(Guid id, [FromBody] UserDto dto)
        {
            try
            {
                var user = await _userService.UpdateAsync(id, dto.Name, dto.Email, dto.Password);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Remove um usu�rio.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _userService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
