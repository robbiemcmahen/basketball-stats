using Microsoft.EntityFrameworkCore;

public class PlayerService
{
    private readonly AppDbContext _context;

    public PlayerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Player>> GetAll()
    {
        return await _context.Players.ToListAsync();
    }

    public async Task<Player?> GetById(int id)
    {
        return await _context.Players.FindAsync(id);

    }

    public async Task<List<Player>> GetByTeamId(int teamId)
    {
        return await _context.Players.Where(p => p.TeamId == teamId).ToListAsync();
    }

    public async Task<Player> Create(Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }
}