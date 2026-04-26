public class PlayerService
{
    private readonly List<Player> _players = new()
    {
        new Player{ Id = 1, Name = "Player", JerseyNumber = 1, TeamId = 1 }
    };

    public List<Player> GetAll() => _players;

    public Player? GetById(int id)
    {
        return _players.FirstOrDefault(p => p.Id == id);
    }

    public List<Player> GetByTeamId(int teamId)
    {
        return _players.Where(p => p.TeamId == teamId).ToList();
    }

    public Player Create(Player player)
    {
        player.Id = _players.Count + 1;
        _players.Add(player);
        return player;
    }
}