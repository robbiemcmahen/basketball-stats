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
    public ActionResult<List<Player>> GetPlayers()
    {
        return Ok(_playerService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Player> GetPlayer(int id)
    {
        var player = _playerService.GetById(id);

        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);
    }

    [HttpGet("team/{teamId}")]
    public ActionResult<List<Player>> GetPlayersByTeam(int teamId)
    {
        return Ok(_playerService.GetByTeamId(teamId));
    }

    [HttpPost]
    public ActionResult<Player> CreatePlayer(Player player)
    {
        var createdPlayer = _playerService.Create(player);

        return CreatedAtAction(
            nameof(GetPlayer),
            new { Id = createdPlayer.Id },
            createdPlayer
        );
    }
}