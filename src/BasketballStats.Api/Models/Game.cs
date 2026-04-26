public class Game
{
    public int Id { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public GameStatus Status { get; set; } = GameStatus.NotStarted;
}