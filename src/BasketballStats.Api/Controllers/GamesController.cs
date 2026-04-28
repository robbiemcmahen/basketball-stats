using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly GameService _gameService;
    private readonly BoxScoreService _boxScoreService;

    public GamesController(GameService gameService, BoxScoreService boxScoreService)
    {
        _gameService = gameService;
        _boxScoreService = boxScoreService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Game>>> GetGames()
    {
        return Ok(await _gameService.GetAll());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetGame(int id)
    {
        var game =  await _gameService.GetById(id);

        if (game == null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    [HttpPost]
    public async Task<ActionResult<Game>> CreateGame(Game game)
    {
        var createdGame =  await _gameService.Create(game);

        return CreatedAtAction(
            nameof(GetGame),
            new { id = createdGame.Id},
            createdGame
        );
    }

    [HttpPatch("{id}/status")]
    public async Task<ActionResult<Game>> UpdateGameStatus(int id, [FromBody] GameStatus status)
    {
        var updatedGame = await _gameService.UpdateStatus(id, status);

        if (updatedGame == null)
        {
            return NotFound();
        }

        return Ok(updatedGame);
    }

    [HttpGet("{id}/boxscore")]
    public async Task<ActionResult<GameBoxScore>> GetBoxScore(int id)
    {
        var game = await _gameService.GetById(id);

        if (game == null)
        {
            return NotFound();
        }

        var boxScore = await _boxScoreService.GetBoxScore(id);

        return Ok(boxScore);
    }
}