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
    public ActionResult<List<Team>> GetTeams()
    {
        return Ok(_teamService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Team> GetTeam(int id)
    {
        return _teamService.GetById(id);
    }

    [HttpPost]
    public ActionResult<Team> CreateTeam(Team team)
    {
        return _teamService.Create(team);
    }
}