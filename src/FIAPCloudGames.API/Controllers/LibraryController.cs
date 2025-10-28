using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.API.Controllers
{
    /// <summary>
    /// Gerencia operações de bibliotecas de jogos dos usuários.
    /// </summary>
    /// <remarks>
    /// Permite criar biblioteca para um usuário, listar e consultar bibliotecas, adquirir jogos e remover uma biblioteca.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    public class LibraryController(ILibraryService libraryService) : ControllerBase
    {
        /// <summary>
        /// Cria uma nova biblioteca para o usuário.
        /// </summary>
        /// <param name="dto">Dados contendo o ID do usuário.</param>
        /// <returns>Biblioteca criada.</returns>
        /// <response code="201">Biblioteca criada com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(LibraryReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] LibraryDto dto)
        {
            try
            {
                var libraryDto = await libraryService.RegisterAsync(dto.UserId);
                return Created($"/api/library/{libraryDto.Id}", libraryDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Lista todas as bibliotecas.
        /// </summary>
        /// <returns>Lista de bibliotecas.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(IEnumerable<LibraryReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll()
        {
            var libraries = await libraryService.GetAllAsync();
            return Ok(libraries);
        }

        /// <summary>
        /// Busca a biblioteca pelo id.
        /// </summary>
        /// <param name="id">ID da biblioteca.</param>
        /// <returns>Biblioteca encontrada.</returns>
        /// <response code="200">Biblioteca encontrada.</response>
        /// <response code="404">Biblioteca não encontrada.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(LibraryReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var libraryDto = await libraryService.GetByIdAsync(id);
            if (libraryDto == null)
            {
                return NotFound();
            }
            return Ok(libraryDto);
        }

        /// <summary>
        /// Remove uma biblioteca.
        /// </summary>
        /// <param name="id">ID da biblioteca.</param>
        /// <response code="204">Biblioteca removida com sucesso.</response>
        /// <response code="404">Biblioteca não encontrada.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await libraryService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Adquire um jogo para o usuário.
        /// </summary>
        /// <param name="dto">Dados contendo o ID do usuário e do jogo.</param>
        /// <returns>Mensagem de sucesso.</returns>
        /// <response code="200">Jogo adquirido com sucesso.</response>
        /// <response code="400">Não foi possível adquirir o jogo.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpPost("acquire-game")]
        [Authorize(Roles = "Admin,User")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AcquireGame([FromBody] AcquireGameDto dto)
        {
            var success = await libraryService.AcquireGameAsync(dto.UserId, dto.GameId);
            if (!success)
            {
                return BadRequest();
            }

            return Ok(new { message = "Jogo adquirido com sucesso." });
        }

        /// <summary>
        /// Lista a biblioteca de um usuário específico.
        /// </summary>
        /// <param name="userId">ID do usuário.</param>
        /// <returns>Biblioteca do usuário.</returns>
        /// <response code="200">Biblioteca retornada com sucesso.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(typeof(LibraryReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetLibraryByUserId(Guid userId)
        {
            var libraryDto = await libraryService.GetLibraryByUserIdAsync(userId);
            return Ok(libraryDto);
        }
    }
}
