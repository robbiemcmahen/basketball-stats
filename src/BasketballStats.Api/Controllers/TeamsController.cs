using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class TeamsController : ControllerBase
{
    private static readonly List<Team> Teams = new()
    {
        new Team { Id = 1, Name = "Chuds" }
    };

    [HttpGet]
    public ActionResult<List<Team>> GetTeams()
    {
        return Ok(Teams);
    }

    [HttpGet("{id}")]
    public ActionResult<Team> GetTeam(int id)
    {
        var team = Teams.FirstOrDefault(t => t.Id == id);

        if (team == null)
        {
            return NotFound();
        }

        return Ok(team);
    }

    [HttpPost]
    public ActionResult<Team> CreateTeam(Team team)
    {
        team.Id = Teams.Count + 1;
        Teams.Add(team);

        return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
    }
}