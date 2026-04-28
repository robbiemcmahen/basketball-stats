using Microsoft.EntityFrameworkCore;

public class TeamService
{
    private readonly AppDbContext _context;

    public TeamService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Team>> GetAll()
    {
        return await _context.Teams.ToListAsync();
    }

    public async Task<Team?> GetById(int id)
    {
        return await _context.Teams.FindAsync(id);
    }
        
    public async Task<Team> Create(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();

        return team;
    }
}