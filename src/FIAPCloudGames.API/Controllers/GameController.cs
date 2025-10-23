using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Interfaces;
using FIAPCloudGames.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameDto dto)
        {
            var gameDto = await _gameService.RegisterAsync(dto.Title, dto.Description, dto.ReleaseDate, dto.Price);
            return Created($"/api/game/{gameDto.Id}", gameDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetAllAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var gameDto = await _gameService.GetByIdAsync(id);
            if (gameDto == null)
            {
                return NotFound();
            }

            return Ok(gameDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGameDto dto)
        {
            var gameDto = await _gameService.UpdateAsync(id, dto.Title, dto.Description, dto.ReleaseDate, dto.Price);
            if (gameDto == null)
            {
                return NotFound();
            }

            return Ok(gameDto);
        }

        [HttpDelete("{id}")]
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
