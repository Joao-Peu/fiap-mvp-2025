using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.API.Controllers
{

    /// <summary>
    /// Operações de gerenciamento de usuários.
    /// </summary>
    /// <remarks>
    /// Endpoints para criação, leitura, atualização e remoção de usuários.
    /// Requer autenticação com token JWT e, na maioria dos casos, papel Admin.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
    {
        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="dto">Dados do usuário (nome, email, senha).</param>
        /// <returns>Usuário criado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(UserReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            try
            {
                var userDto = await userService.RegisterAsync(dto.Name, dto.Email, dto.Password);
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
        /// <returns>Lista de usuários.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<UserReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Busca usuário por ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Usuário encontrado.</returns>
        /// <response code="200">Usuário encontrado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(UserReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userDto = await userService.GetByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        /// <summary>
        /// Atualiza dados de um usuário.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <param name="dto">Dados atualizados do usuário.</param>
        /// <returns>Usuário atualizado.</returns>
        /// <response code="200">Usuário atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(UserReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserDto dto)
        {
            try
            {
                var userDto = await userService.UpdateAsync(id, dto.Name, dto.Email, dto.Password);
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
        /// <param name="id">ID do usuário.</param>
        /// <response code="204">Usuário removido com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await userService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
