using Microsoft.EntityFrameworkCore;

public class GameService
{
    private readonly AppDbContext _context;
    
    public GameService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Game>> GetAll()
    {
        return await _context.Games.ToListAsync();
    }

    public async Task<Game?> GetById(int id)
    {
        return await _context.Games.FindAsync(id);
    }

    public async Task<Game> Create(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<Game?> UpdateStatus(int id, GameStatus status)
    {
        var game = await GetById(id);

        if (game == null)
        {
            return null;
        }

        game.Status = status;
        await _context.SaveChangesAsync();
        return game;
    }
}