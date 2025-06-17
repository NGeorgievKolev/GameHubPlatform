using GameService.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameSessionService _service;

    public GameController(GameSessionService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var id = await _service.CreateGameAsync();
        var game = await _service.GetGameAsync(id);
        return Ok(game);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var game = await _service.GetGameAsync(id);
        return game == null ? NotFound() : Ok(game);
    }

    [HttpPost("{id}/move")]
    public async Task<IActionResult> Move(string id, [FromQuery] int position)
    {
        var game = await _service.MakeMoveAsync(id, position);
        return game == null ? NotFound() : Ok(game);
    }
}