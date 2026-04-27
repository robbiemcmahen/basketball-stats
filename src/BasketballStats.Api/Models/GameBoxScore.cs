public class GameBoxScore
{
    public int GameId { get; set; }
    public List<PlayerBoxScore> Players { get; set; } = new();
}