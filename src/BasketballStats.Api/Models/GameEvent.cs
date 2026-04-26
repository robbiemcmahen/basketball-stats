public class GameEvent
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public int TeamId { get; set; }
    public GameEventType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}