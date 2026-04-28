using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class GameEventController : ControllerBase
{
    private readonly GameEventService _gameEventService;

    public GameEventController(GameEventService gameEventService)
    {
        _gameEventService = gameEventService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GameEvent>>> GetEvents()
    {
        return Ok(await _gameEventService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameEvent>> GetEvent(int id)
    {
        var gameEvent = await _gameEventService.GetById(id);

        if (gameEvent == null)
        {
            return NotFound();
        }

        return Ok(gameEvent);
    } 

    [HttpGet("game/{gameId}")]
    public async Task<ActionResult<List<GameEvent>>> GetEventsByGame(int gameId)
    {
        return Ok(await _gameEventService.GetByGameId(gameId));

    }

    [HttpPost]
    public async Task<ActionResult<GameEvent>> CreateEvent(GameEvent gameEvent)
    {
        var createdEvent = await _gameEventService.Create(gameEvent);

        return CreatedAtAction(
            nameof(GetEvent),
            new { id = createdEvent.Id },
            createdEvent
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var deleted = await _gameEventService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}