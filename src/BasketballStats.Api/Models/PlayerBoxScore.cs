public class PlayerBoxScore
{
    public int PlayerId { get; set; }
    public int TeamId { get; set; }

    public int Points { get; set; }
    public int FieldGoalsMade { get; set; }
    public int FieldGoalsAttempted { get; set; }
    public int ThreePointsMade { get; set; }
    public int ThreePointsAttempted { get; set; }
    public int FreeThrowsMade { get; set; }
    public int FreeThrowsAttempted { get; set; }

    public int Rebounds { get; set; }
    public int Assists { get; set; }
    public int Steals { get; set; }
    public int Blocks { get; set; }
    public int Turnovers { get; set; }
    public int Fouls { get; set; }
}