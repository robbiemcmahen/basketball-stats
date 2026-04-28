import { useState, useEffect } from "react"

export default function PlayersPage() {
    const [teams, setTeams] = useState([]);
    const [players, setPlayers] = useState([]);
    const [playerName, setPlayerName] = useState("");
    const [playerNumber, setPlayerNumber] = useState("");
    const [selectedTeamId, setSelectedTeamId] = useState("");

    useEffect(() => {
            fetch("http://localhost:5255/api/teams")
                .then(res => res.json())
                .then(data => setTeams(data))
                .catch(err => console.error(err));
        }, []);

    useEffect(() => {
        fetch("http://localhost:5255/api/player")
            .then(res => res.json())
            .then(data => setPlayers(data))
            .catch(err => console.error(err));
    }, []);

    const handlePlayerCreate = async (e) => {
        e.preventDefault();

        const newPlayer = {
            name: playerName,
            jerseyNumber: Number(playerNumber),
            teamId: Number(selectedTeamId)
        };

        try {
            const res = await fetch("http://localhost:5255/api/player", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(newPlayer)
            });

            const createdPlayer = await res.json();

            setPlayers([...players, createdPlayer]);

            setPlayerName("");
            setPlayerNumber("");
            setSelectedTeamId("");
        } catch(err) {
            console.error(err);
        }
    }

    return (
        <>
            <form onSubmit={handlePlayerCreate}>
                <label>Player Name:</label>
                <input
                    type="text"
                    value={playerName}
                    onChange={(e) => setPlayerName(e.target.value)}
                />

                <label>Jersey Number:</label>
                <input
                    type="number"
                    value={playerNumber}
                    onChange={(e) => setPlayerNumber(e.target.value)}
                />

                <label>Team:</label>
                <select
                    value={selectedTeamId}
                    onChange={(e) => setSelectedTeamId(e.target.value)}
                >
                    {teams.map((t) => (
                        <option key={t.id} value={t.id}>{t.name}</option>
                    ))}
                </select>

                <button
                    type="submit"
                >
                    Create
                </button>
            </form>

            <ul>
                {players.map((p) => (
                    <li key={p.id}>
                        #{p.jerseyNumber} {p.name} - {p.teamId}
                    </li>
                ))}
            </ul>
        </>
        
    )
}