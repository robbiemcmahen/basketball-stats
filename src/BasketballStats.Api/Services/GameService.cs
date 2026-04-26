using Microsoft.AspNetCore.Http.HttpResults;

public class GameService
{
    private readonly List<Game> _games = new()
    {
        new Game
        {
            Id = 1,
            HomeTeamId = 1,
            AwayTeamId = 2,
            Status = GameStatus.NotStarted
        }
    };

    public List<Game> GetAll()
    {
        return _games;
    }

    public Game? GetById(int id)
    {
        return _games.FirstOrDefault(g => g.Id == id);
    }

    public Game Create(Game game)
    {
        game.Id = _games.Count + 1;
        _games.Add(game);
        return game;
    }

    public Game? UpdateStatus(int id, GameStatus status)
    {
        var game = GetById(id);

        if (game == null)
        {
            return null;
        }

        game.Status = status;
        return game;
    }
}