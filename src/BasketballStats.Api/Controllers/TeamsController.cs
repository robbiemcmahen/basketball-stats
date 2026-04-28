using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly TeamService _teamService;

    public TeamsController(TeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Team>>> GetTeams()
    {
        return Ok(await _teamService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Team>> GetTeam(int id)
    {
        var team = await _teamService.GetById(id);

        if (team == null)
        {
            return NotFound();
        }

        return Ok(team);
    }

    [HttpPost]
    public async Task<ActionResult<Team>> CreateTeam(Team team)
    {
        var createdTeam = await _teamService.Create(team);

        return CreatedAtAction(
            nameof(GetTeam),
            new { id = createdTeam.Id },
            createdTeam
        );
    }
}