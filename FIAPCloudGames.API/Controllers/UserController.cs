using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.API.Controllers
{
    /// <summary>
    /// Gerencia operações de usuários.
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

        public IActionResult Create([FromBody] UserDto dto)
        {
            try
            {
                var user = _userService.Register(dto.Name, dto.Email, dto.Password);
                return Created($"/api/user/{user.Id}", user);
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
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        /// <summary>
        /// Busca usuário por ID.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Atualiza dados de um usuário.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Update(Guid id, [FromBody] UserDto dto)
        {
            try
            {
                var user = _userService.Update(id, dto.Name, dto.Email, dto.Password);
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
        /// Remove um usuário.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            var success = _userService.Delete(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
