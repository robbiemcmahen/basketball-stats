public class TeamService
{
    private readonly List<Team> _teams = new()
    {
        new Team { Id = 1, Name = "Chuds" }
    };

    public List<Team> GetAll() => _teams;

    public Team? GetById(int id) => 
        _teams.FirstOrDefault(t => t.Id == id);

    public Team Create(Team team)
    {
        team.Id = _teams.Count + 1;
        _teams.Add(team);
        return team;
    }
}