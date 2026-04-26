public class GameEventService
{
    private readonly List<GameEvent> _events = new();

    public List<GameEvent> GetAll() {
        return _events;
    }

    public GameEvent? GetById(int id)
    {
        return _events.FirstOrDefault(e => e.Id == id);
    }

    public List<GameEvent> GetByGameId(int gameId)
    {
        return _events.Where(e => e.GameId == gameId).ToList();
    }

    public GameEvent Create(GameEvent gameEvent) {
        gameEvent.Id = _events.Count + 1;
        gameEvent.CreatedAt = DateTime.UtcNow;

        _events.Add(gameEvent);

        return gameEvent;
    }

    public bool Delete(int id)
    {
        var gameEvent = GetById(id);

        if (gameEvent == null)
        {
            return false;
        }

        _events.Remove(gameEvent);
        return true;
    }
}