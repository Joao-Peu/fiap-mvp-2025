using FIAPCloudGames.Application.Dtos;
using FIAPCloudGames.Application.Services;
using FIAPCloudGames.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;
        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] GameDto dto)
        {
            var game = _gameService.Register(dto.Title, dto.Description, dto.ReleaseDate, dto.Price);
            return Created($"/api/game/{game.Id}", game);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            var games = _gameService.GetAll();
            return Ok(games);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetById(Guid id)
        {
            var game = _gameService.GetById(id);
            if (game == null) return NotFound();
            return Ok(game);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] GameDto dto)
        {
            var game = _gameService.Update(id, dto.Title, dto.Description, dto.ReleaseDate, dto.Price);
            if (game == null) return NotFound();
            return Ok(game);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var success = _gameService.Delete(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
