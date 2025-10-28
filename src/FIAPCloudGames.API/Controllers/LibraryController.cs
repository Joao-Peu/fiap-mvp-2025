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
            var libraryDto = await libraryService.GetByIdAsync(id);
            if (libraryDto == null)
            {
                return NotFound();
            }
            return Ok(libraryDto);
        }

        /// <summary>
        /// Remove um usuário.
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
        /// Adquire um jogo para o usuário.
        /// </summary>
        [HttpPost("acquire-game")]
        [Authorize(Roles = "Admin,User")]
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
        /// Lista biblioteca do usuario
        /// </summary>
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetLibraryByUserId(Guid userId)
        {
            var libraryDto = await libraryService.GetLibraryByUserIdAsync(userId);
            return Ok(libraryDto);
        }
    }
}
