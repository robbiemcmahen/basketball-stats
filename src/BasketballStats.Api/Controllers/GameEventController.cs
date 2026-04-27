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
    public ActionResult<List<GameEvent>> GetEvents()
    {
        return Ok(_gameEventService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<GameEvent> GetEvent(int id)
    {
        var gameEvent = _gameEventService.GetByGameId(id);

        if (gameEvent == null)
        {
            return NotFound();
        }

        return Ok(gameEvent);
    } 

    [HttpGet("game/{gameId}")]
    public ActionResult<List<GameEvent>> GetEventsByGame(int gameId)
    {
        return Ok(_gameEventService.GetByGameId(gameId));

    }

    [HttpPost]
    public ActionResult<GameEvent> CreateEvent(GameEvent gameEvent)
    {
        var createdEvent = _gameEventService.Create(gameEvent);

        return CreatedAtAction(
            nameof(GetEvent),
            new { id = createdEvent.Id },
            createdEvent
        );
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(int id)
    {
        var deleted = _gameEventService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}