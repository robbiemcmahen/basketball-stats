using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _playerService;
    public PlayerController(PlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Player>>> GetPlayers()
    {
        return Ok(await _playerService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetPlayer(int id)
    {
        var player = await _playerService.GetById(id);

        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);
    }

    [HttpGet("team/{teamId}")]
    public async Task<ActionResult<List<Player>>> GetPlayersByTeam(int teamId)
    {
        return Ok(await _playerService.GetByTeamId(teamId));
    }

    [HttpPost]
    public async Task<ActionResult<Player>> CreatePlayer(Player player)
    {
        var createdPlayer = await _playerService.Create(player);

        return CreatedAtAction(
            nameof(GetPlayer),
            new { id = createdPlayer.Id },
            createdPlayer
        );
    }
}