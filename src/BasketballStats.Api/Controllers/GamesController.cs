using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class GamesControler : ControllerBase
{
    private readonly GameService _gameService;

    public GamesControler(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public ActionResult<List<Game>> GetGames()
    {
        return Ok(_gameService.GetAll());
    }
    
    [HttpGet("{id}")]
    public ActionResult<Game> GetGame(int id)
    {
        var game = _gameService.GetById(id);

        if (game == null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    [HttpPost]
    public ActionResult<Game> CreateGame(Game game)
    {
        var createdGame = _gameService.Create(game);

        return CreatedAtAction(
            nameof(GetGame),
            new { id = createdGame.Id},
            createdGame
        );
    }

    [HttpPatch("{id}/status")]
    public ActionResult<Game> UpdateGameStatus(int id, [FromBody] GameStatus status)
    {
        var updatedGame = _gameService.UpdateStatus(id, status);

        if (updatedGame == null)
        {
            return NotFound();
        }

        return Ok(updatedGame);
    }
}