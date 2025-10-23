using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.API.Controllers
{
    /// <summary>
    /// Gerencia operações de usu�rios.
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
        /// Cria um novo usuário.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            try
            {
                var userDto = await _userService.RegisterAsync(dto.Name, dto.Email, dto.Password);
                return Created($"/api/user/{userDto.Id}", userDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Lista todos os usuários.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Busca usuário por ID.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userDto = await _userService.GetByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        /// <summary>
        /// Atualiza dados de um usuário.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(Guid id, [FromBody] UserDto dto)
        {
            try
            {
                var userDto = await _userService.UpdateAsync(id, dto.Name, dto.Email, dto.Password);
                if (userDto == null)
                {
                    return NotFound();
                }

                return Ok(userDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Remove um usuário.
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
