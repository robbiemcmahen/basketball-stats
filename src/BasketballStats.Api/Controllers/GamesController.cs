using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class GamesControler : ControllerBase
{
    private readonly GameService _gameService;
    private readonly BoxScoreService _boxScoreService;

    public GamesControler(GameService gameService, BoxScoreService boxScoreService)
    {
        _gameService = gameService;
        _boxScoreService = boxScoreService;
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

    [HttpGet("{id}/boxscore")]
    public ActionResult<GameBoxScore> GetBoxScore(int id)
    {
        var game = _gameService.GetById(id);

        if (game == null)
        {
            return NotFound();
        }

        var boxScore = _boxScoreService.GetBoxScore(id);

        return Ok(boxScore);
    }
}