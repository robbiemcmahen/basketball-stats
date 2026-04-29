import { useState, useEffect } from "react"
import { Link } from "react-router-dom"

export default function GameSetupPage() {
    const [teams, setTeams] = useState([]);
    const [homeTeamId, setHomeTeamId] = useState("");
    const [awayTeamId, setAwayTeamId] = useState("");
    const [games, setGames] = useState([]);

    useEffect(() => {
                fetch("http://localhost:5255/api/teams")
                    .then(res => res.json())
                    .then(data => setTeams(data))
                    .catch(err => console.error(err));
            }, []);

    useEffect(() => {
        fetch("http://localhost:5255/api/games")
            .then(res => res.json())
            .then(data => setGames(data))
            .catch(err => console.error(err))
    }, []);

    const getTeamName = (teamId) => {
        const team = teams.find(t => t.id === teamId);
        return team ? team.name : "Unknown";
    }


    const handleGameCreate = async (e) => {
        e.preventDefault();

        if (homeTeamId == awayTeamId) {
            console.log("Home team and away team must be different");
            return;
        }

        const newGame = {
            homeTeamId: homeTeamId,
            awayTeamId: awayTeamId,
            status: "NotStarted"
        };

        try {
            const res = await fetch("http://localhost:5255/api/games", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newGame)
        });

        const createdGame = await res.json();

        setGames([...games, createdGame]);

        setHomeTeamId("");
        setAwayTeamId("");
        
        } catch (err) {
            console.error(err);
        }

        
    }
    

    return (
        <>
            <form onSubmit={handleGameCreate}>
                <label>Home Team:</label>
                <select
                    value={homeTeamId}
                    onChange={(e) => setHomeTeamId(e.target.value)}
                >
                    <option value="">Select a team:</option>

                        {teams.map((t) => (
                            <option key={t.id} value={t.id}>{t.name}</option>
                        ))}
                </select>

                <label>Away Team:</label>
                <select
                    value={awayTeamId}
                    onChange={(e) => setAwayTeamId(e.target.value)}
                >
                    <option value="">Select a team:</option>
                        {teams.map((t) => (
                            <option key={t.id} value={t.id}>{t.name}</option>
                        ))}
                </select>

                <button
                    type="submit"
                >
                    Start Game
                </button>
            </form>

            <ul>
                {games.map((g) => (
                    <li key={g.id}>
                        {getTeamName(g.homeTeamId)} vs {getTeamName(g.awayTeamId)} - {g.status}
                        <Link to={`./${g.id}/live`}>Open Live Game</Link>
                    </li>
                ))}
            </ul>
        </>
    )
}