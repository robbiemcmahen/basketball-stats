using Microsoft.EntityFrameworkCore;

public class GameEventService
{
    private readonly AppDbContext _context;

    public GameEventService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GameEvent>> GetAll() {
        return await _context.GameEvents.ToListAsync();
    }

    public async Task<GameEvent?> GetById(int id)
    {
        return await _context.GameEvents.FindAsync(id);
    }

    public async Task<List<GameEvent>> GetByGameId(int gameId)
    {
        return await _context.GameEvents.Where(e => e.GameId == gameId).OrderBy(e => e.CreatedAt).ToListAsync();
    }

    public async Task<GameEvent> Create(GameEvent gameEvent) {
        gameEvent.CreatedAt = DateTime.UtcNow;

        _context.GameEvents.Add(gameEvent);
        await _context.SaveChangesAsync();

        return gameEvent;
    }

    public async Task<bool> Delete(int id)
    {
        var gameEvent = await GetById(id);

        if (gameEvent == null)
        {
            return false;
        }

        _context.GameEvents.Remove(gameEvent);
        await _context.SaveChangesAsync();
        return true;
    }
}