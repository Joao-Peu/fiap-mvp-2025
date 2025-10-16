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
    [Authorize(Roles = "Admin")]
    public class LibraryController(ILibraryService libraryService) : ControllerBase
    {
        /// <summary>
        /// Cria uma nova biblioteca para o usu�rio.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LibraryDto dto)
        {
            try
            {
                var library = await libraryService.RegisterAsync(dto.UserId);
                return Created($"/api/library/{library.Id}", library);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Lista todos as bibliotecas.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var libraries = await libraryService.GetAllAsync();
            return Ok(libraries);
        }

        /// <summary>
        /// Busca a biblioteca pelo id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var library = await libraryService.GetByIdAsync(id);
            if (library == null)
            {
                return NotFound();
            }
            return Ok(library);
        }

        /// <summary>
        /// Remove um usu�rio.
        /// </summary>
        [HttpDelete("{id}")]
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
        /// Adquire um jogo para o usu�rio.
        /// </summary>
        [HttpPost("acquire-game")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> AcquireGame([FromBody] AcquireGameDto dto)
        {
            var success = await libraryService.AcquireGameAsync(dto.UserId, dto.GameId);
            if (!success)
            {
                // ToDo: passar essas valida��es para a service
                return BadRequest(new { error = "Usu�rio ou jogo n�o encontrado." });
            }

            return Ok(new { message = "Jogo adquirido com sucesso." });
        }

        /// <summary>
        /// Lista biblioteca do usuario
        /// </summary>
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetLibraryByUserId(Guid userId)
        {
            var library = await libraryService.GetLibraryByUserIdAsync(userId);
            return Ok(library);
        }
    }
}
