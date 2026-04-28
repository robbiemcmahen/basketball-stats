public class BoxScoreService
{
    private readonly GameEventService _gameEventService;
    private readonly AppDbContext _context;

    public BoxScoreService(GameEventService gameEventService, AppDbContext context)
    {
        _gameEventService = gameEventService;
        _context = context;
    }

    public async Task<GameBoxScore> GetBoxScore(int gameId)
    {
        var events = await _gameEventService.GetByGameId(gameId);

        var playerStats = events
            .GroupBy(e => e.PlayerId)
            .Select(group =>
            {
                var boxScore = new PlayerBoxScore
                {
                    PlayerId = group.Key,
                    TeamId = group.First().TeamId
                };

                foreach (var gameEvent in group)
                {
                    ApplyEvent(boxScore, gameEvent);
                }
                return boxScore;

            })
            .ToList();

        return new GameBoxScore
        {
            GameId = gameId,
            Players = playerStats
        };
    }

    private void ApplyEvent(PlayerBoxScore boxScore, GameEvent gameEvent)
    {
        switch (gameEvent.Type)
        {
            case GameEventType.TwoPointMade:
                boxScore.FieldGoalsMade++;
                boxScore.FieldGoalsAttempted++;
                boxScore.Points += 2;
                break;
            
            case GameEventType.TwoPointMissed:
                boxScore.FieldGoalsAttempted++;
                break;

            case GameEventType.ThreePointMade:
                boxScore.FieldGoalsMade++;
                boxScore.FieldGoalsAttempted++;
                boxScore.ThreePointsMade++;
                boxScore.ThreePointsAttempted++;
                boxScore.Points += 3;
                break;
            
            case GameEventType.ThreePointMissed:
                boxScore.FieldGoalsAttempted++;
                boxScore.ThreePointsAttempted++;
                break;

            case GameEventType.FreeThrowMade:
                boxScore.FreeThrowsMade++;
                boxScore.FreeThrowsAttempted++;
                boxScore.Points++;
                break;

            case GameEventType.FreeThrowMissed:
                boxScore.FreeThrowsAttempted++;
                break;
            
            case GameEventType.Rebound:
                boxScore.Rebounds++;
                break;

            case GameEventType.Assist:
                boxScore.Assists++;
                break;

            case GameEventType.Steal:
                boxScore.Steals++;
                break;

            case GameEventType.Block:
                boxScore.Blocks++;
                break;

            case GameEventType.Turnover:
                boxScore.Turnovers++;
                break;

            case GameEventType.Foul:
                boxScore.Fouls++;
                break;
        }
    }
}