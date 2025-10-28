using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.API.Controllers
{
    /// <summary>
    /// Operações de gerenciamento de jogos.
    /// </summary>
    /// <remarks>
    /// Endpoints para criação, listagem, consulta, atualização e remoção de jogos.
    /// Requer autenticação com token JWT e papel Admin, exceto onde indicado.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Cria um novo jogo.
        /// </summary>
        /// <param name="dto">Dados do jogo (título, descrição, lançamento e preço).</param>
        /// <returns>Jogo criado.</returns>
        /// <response code="201">Jogo criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(GameReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] CreateGameDto dto)
        {
            var gameDto = await _gameService.RegisterAsync(dto.Title, dto.Description, dto.ReleaseDate, dto.Price);
            return Created($"/api/game/{gameDto.Id}", gameDto);
        }

        /// <summary>
        /// Lista todos os jogos.
        /// </summary>
        /// <returns>Lista de jogos.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GameReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetAllAsync();
            return Ok(games);
        }

        /// <summary>
        /// Busca um jogo pelo ID.
        /// </summary>
        /// <param name="id">ID do jogo.</param>
        /// <returns>Jogo encontrado.</returns>
        /// <response code="200">Jogo encontrado.</response>
        /// <response code="404">Jogo não encontrado.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GameReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var gameDto = await _gameService.GetByIdAsync(id);
            if (gameDto == null)
            {
                return NotFound();
            }

            return Ok(gameDto);
        }

        /// <summary>
        /// Atualiza dados de um jogo.
        /// </summary>
        /// <param name="id">ID do jogo.</param>
        /// <param name="dto">Dados para atualização.</param>
        /// <returns>Jogo atualizado.</returns>
        /// <response code="200">Jogo atualizado com sucesso.</response>
        /// <response code="404">Jogo não encontrado.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpPatch("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(GameReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGameDto dto)
        {
            var gameDto = await _gameService.UpdateAsync(id, dto.Title, dto.Description, dto.ReleaseDate, dto.Price);
            if (gameDto == null)
            {
                return NotFound();
            }

            return Ok(gameDto);
        }

        /// <summary>
        /// Remove um jogo.
        /// </summary>
        /// <param name="id">ID do jogo.</param>
        /// <response code="204">Jogo removido com sucesso.</response>
        /// <response code="404">Jogo não encontrado.</response>
        /// <response code="401">Não autenticado.</response>
        /// <response code="403">Sem permissão.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _gameService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
