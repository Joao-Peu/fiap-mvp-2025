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
    [Authorize(Roles = "Admin")]
    public class LibraryController(ILibraryService libraryService) : ControllerBase
    {
        /// <summary>
        /// Cria uma nova biblioteca para o usuário.
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] LibraryDto dto)
        {
            try
            {
                var library = libraryService.Register(dto.UserId);
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
        public IActionResult GetAll()
        {
            var libraries = libraryService.GetAll();
            return Ok(libraries);
        }

        /// <summary>
        /// Busca a biblioteca pelo id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetById(Guid id)
        {
            var library = libraryService.GetById(id);
            if (library == null)
            {
                return NotFound();
            }
            return Ok(library);
        }

        /// <summary>
        /// Remove um usuário.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var success = libraryService.Delete(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Adquire um jogo para o usuário.
        /// </summary>
        [HttpPost("acquire-game")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult AcquireGame([FromBody] AcquireGameDto dto)
        {
            var success = libraryService.AcquireGame(dto.UserId, dto.GameId);
            if (!success)
            {
                // ToDo: passar essas validações para a service
                return BadRequest(new { error = "Usuário ou jogo não encontrado." });
            }

            return Ok(new { message = "Jogo adquirido com sucesso." });
        }

        /// <summary>
        /// Lista biblioteca do usuario
        /// </summary>
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetLibraryByUserId(Guid userId)
        {
            var library = libraryService.GetLibraryByUserId(userId);
            return Ok(library);
        }
    }
}
