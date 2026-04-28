export default function BoxScoreTable({ boxScore }) {
    if (!boxScore) {
        return <p>Loading box score...</p>;
        
    }

    return (
        <div>
            <h2>Box Score</h2>

            <table>
                <thead>
                    <tr>
                        <th>Player ID</th>
                        <th>Team ID</th>
                        <th>PTS</th>
                        <th>FGM</th>
                        <th>FGA</th>
                        <th>3PM</th>
                        <th>3PA</th>
                        <th>FTM</th>
                        <th>FTA</th>
                        <th>REB</th>
                        <th>AST</th>
                        <th>STL</th>
                        <th>BLK</th>
                        <th>TO</th>
                        <th>PF</th>
                    </tr>
                </thead>

                <tbody>
                    {boxScore.players.map((p) => (
                        <tr key={p.playerId}>
                            <td>{p.playerId}</td>
                            <td>{p.teamId}</td>
                            <td>{p.points}</td>
                            <td>{p.fieldGoalsMade}</td>
                            <td>{p.fieldGoalsAttempted}</td>
                            <td>{p.threePointsMade}</td>
                            <td>{p.threePointsAttempted}</td>
                            <td>{p.freeThrowsMade}</td>
                            <td>{p.freeThrowsAttempted}</td>
                            <td>{p.rebounds}</td>
                            <td>{p.assists}</td>
                            <td>{p.steals}</td>
                            <td>{p.blocks}</td>
                            <td>{p.turnovers}</td>
                            <td>{p.fouls}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}