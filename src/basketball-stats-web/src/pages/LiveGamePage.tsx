import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import BoxScoreTable from "../components/BoxScoreTable";

export default function LiveGamePage() {
    const { gameId } = useParams();
    const [game, setGame] = useState(null);
    const [homeTeamPlayers, setHomeTeamPlayers] = useState([]);
    const [awayTeamPlayers, setAwayTeamPlayers] = useState([]);
    const [selectedPlayer, setSelectedPlayer] = useState(null);
    const allPlayers = [...homeTeamPlayers, ...awayTeamPlayers];
    const [boxScore, setBoxScore] = useState(null);

    useEffect(() => {
        fetch(`http://localhost:5255/api/games/${gameId}`)
            .then(res => res.json())
            .then(data => setGame(data))
            .catch(err => console.error(err))
    }, [gameId]);

    useEffect(() => {
        if (!game) return;

        fetch(`http://localhost:5255/api/player/team/${game.homeTeamId}`)
            .then(res => res.json())
            .then(data => setHomeTeamPlayers(data))
            .catch(err => console.error(err))

        fetch(`http://localhost:5255/api/player/team/${game.awayTeamId}`)
            .then(res => res.json())
            .then(data => setAwayTeamPlayers(data))
            .catch(err => console.error(err))

    }, [game]);

    const recordEvent = async (type) => {
        const newEvent = {
            gameId: gameId,
            playerId: selectedPlayer.id,
            teamId: selectedPlayer.teamId,
            type: type
        }

        try {
            const res = await fetch("http://localhost:5255/api/gameevent", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(newEvent)
            });

            if (!res.ok) {
                console.error("Failed to record event");
                return;
            } 
            fetchBoxScore();

        } catch (err) {
            console.error(err);
        }
    }

    const fetchBoxScore = async () => {
        try {
            const res = await fetch(`http://localhost:5255/api/games/${gameId}/boxscore`);

            const data = await res.json();
            setBoxScore(data);
        } catch (err) {
            console.error(err);
        }
    }

    useEffect(() => {
        if (!gameId) return;
        fetchBoxScore();
    }, [gameId]);

    return (
        <>
            <h1>Live Game</h1>

            {game && (
                <div>
                    <p>Game ID: {game.id}</p>
                    <p>Home Team: {game.homeTeamId}</p>
                    <p>Away Team: {game.awayTeamId}</p>
                    <p>Status: {game.status}</p>
                </div>
            )}

            {homeTeamPlayers && (
                <div>
                    <h2>Home Team:</h2>
                    <ul>
                        {homeTeamPlayers.map((p) => (
                            <li
                                key={p.id}
                                onClick={() => setSelectedPlayer(p)}
                            >
                                #{p.jerseyNumber} - {p.name}
                            </li>
                        ))}
                    </ul>
                </div>
            )}

            {awayTeamPlayers && (
                <div>
                    <h2>Away Team:</h2>
                    <ul>
                        {awayTeamPlayers.map((p) => (
                            <li 
                                key={p.id}
                                onClick={() => setSelectedPlayer(p)}
                            >
                                #{p.jerseyNumber} - {p.name}
                            </li>
                        ))}
                    </ul>
                </div>
            )}

            {selectedPlayer && (
                <div>
                    <h3>Selected Player:</h3>
                    <p>#{selectedPlayer.jerseyNumber} - {selectedPlayer.name}</p>
                </div>
                
            )}

            <div>
                <button onClick={() => recordEvent("TwoPointMade")}>
                    2PT Made
                </button>

                <button onClick={() => recordEvent("TwoPointMissed")}>
                    2PT Missed
                </button>

                <button onClick={() => recordEvent("ThreePointMade")}>
                    3PT Made
                </button>

                <button onClick={() => recordEvent("ThreePointMissed")}>
                    3PT Missed
                </button>

                <button onClick={() => recordEvent("FreeThrowMade")}>
                    FT Made
                </button>

                <button onClick={() => recordEvent("FreeThrowMissed")}>
                    FT Missed
                </button>

                <button onClick={() => recordEvent("Rebound")}>
                    Rebound
                </button>

                <button onClick={() => recordEvent("Assist")}>
                    Assist
                </button>

                <button onClick={() => recordEvent("Steal")}>
                    Steal
                </button>

                <button onClick={() => recordEvent("Block")}>
                    Block
                </button>

                <button onClick={() => recordEvent("Turnover")}>
                    Turnover
                </button>

                <button onClick={() => recordEvent("Foul")}>
                    Foul
                </button>
            </div>

            <BoxScoreTable boxScore={boxScore} players={allPlayers}/>
        </>
    )
}