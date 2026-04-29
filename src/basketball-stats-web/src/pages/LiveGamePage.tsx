import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import BoxScoreTable from "../components/BoxScoreTable";

export default function LiveGamePage() {
    const { gameId } = useParams();
    const [game, setGame] = useState(null);
    const [homeTeam, setHomeTeam] = useState(null);
    const [awayTeam, setAwayTeam] = useState(null);
    const [homeTeamPlayers, setHomeTeamPlayers] = useState([]);
    const [awayTeamPlayers, setAwayTeamPlayers] = useState([]);
    const [selectedPlayer, setSelectedPlayer] = useState(null);
    const allPlayers = [...homeTeamPlayers, ...awayTeamPlayers];
    const [boxScore, setBoxScore] = useState(null);
    const [events, setEvents] = useState([]);

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

    useEffect(() => {
        if (!game) return;

        fetch(`http://localhost:5255/api/teams/${game.homeTeamId}`)
            .then(res => res.json())
            .then(data => setHomeTeam(data))
            .catch(err => console.error(err))

        fetch(`http://localhost:5255/api/teams/${game.awayTeamId}`)
            .then(res => res.json())
            .then(data => setAwayTeam(data))
            .catch(err => console.error(err))
    }, [game])

    const getPlayerName = (playerId) => {
        const player = allPlayers.find(p => p.id == playerId);
        return player ? player.name: "Unknown";
    }

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
            fetchEvents();

        } catch (err) {
            console.error(err);
        }
    }

    const fetchEvents = async () => {
        try {
            const res = await fetch(`http://localhost:5255/api/gameevent/game/${gameId}`);

            const data = await res.json();

            setEvents(data);
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
        fetchEvents();
        fetchBoxScore();
    }, [gameId]);

    const undoLastEvent = async () => {
        if (events.length === 0) {
            return;
        }

        const lastEvent = events[events.length - 1];

        try {
            const res = await fetch(`http://localhost:5255/api/gameevent/${lastEvent.id}`, {
                method: "DELETE"
            });

            if (!res.ok) {
                console.error("Failed to undo last event");
                return;
            }

            await fetchEvents();
            await fetchBoxScore();
        } catch (err) {
            console.error(err);
        }
    }

    return (
        <>
            <h1>Live Game</h1>

            {game && (
                <div>
                    <p>Game ID: {game.id}</p>
                    <p>Home Team: {homeTeam?.name}</p>
                    <p>Away Team: {awayTeam?.name}</p>
                    <p>Status: {game.status}</p>
                </div>
            )}

            <div className="live-game-layout">
                <section className="team-column">
                    {homeTeamPlayers && (
                        <div>
                            <h2>Home Team:</h2>
                            <ul>
                                {homeTeamPlayers.map((p) => (
                                    <button
                                        key={p.id}
                                        onClick={() => setSelectedPlayer(p)}
                                        className={selectedPlayer?.id === p.id ? "player-card selected" : "player-card"}
                                    >
                                        #{p.jerseyNumber} - {p.name}
                                    </button>
                                ))}
                            </ul>
                        </div>
                )}
                </section>

                <main className="stat-panel">
                    {selectedPlayer && (
                        <div>
                            <h3>Selected Player:</h3>
                            <p>#{selectedPlayer.jerseyNumber} - {selectedPlayer.name}</p>
                        </div>
                        
                    )}

                    <div className="stat-buttons">
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
                </main>

                <section className="team-column">
                    {awayTeamPlayers && (
                        <div>
                            <h2>Away Team:</h2>
                            <ul>
                                {awayTeamPlayers.map((p) => (
                                    <button
                                        key={p.id}
                                        onClick={() => setSelectedPlayer(p)}
                                        className={selectedPlayer?.id === p.id ? "player-card selected" : "player-card"}
                                    >
                                        #{p.jerseyNumber} - {p.name}
                                    </button>
                                ))}
                            </ul>
                        </div>
                    )}
                </section>
            </div>

            <button onClick={undoLastEvent} className="undo-button">
                Undo Last Event
            </button>

            <section className="stats-lower">
                <h2>Event Log</h2>

                {events.length === 0 ? (
                    <p>No events recorded yet.</p>
                ) : (
                    <ul>
                        {events.reverse().map(e => (
                            <li key={e.id}>
                                {getPlayerName(e.playerId)} - {e.type}
                            </li>
                        ))}
                    </ul>
                )
                }

                <BoxScoreTable boxScore={boxScore} players={allPlayers}/>
            </section>

            
        </>
    )
}