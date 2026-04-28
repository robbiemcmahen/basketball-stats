import { useEffect, useState } from "react";

export default function TeamsPage() {
    const [teams, setTeams] = useState([]);
    const [teamName, setTeamName] = useState("");

    useEffect(() => {
        fetch("http://localhost:5255/api/teams")
            .then(res => res.json())
            .then(data => setTeams(data))
            .catch(err => console.error(err));
    }, []);

    const handleTeamCreate = async (e) => {
        e.preventDefault();

        const newTeam = {
            name: teamName
        };

        try {
            const res = await fetch("http://localhost:5255/api/teams", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(newTeam)
            });

            const createdTeam = await res.json();

            setTeams([...teams, createdTeam]);

            setTeamName("");
        } catch (err) {
            console.error(err);
        }
    }

    return (
        <>
            <form onSubmit={handleTeamCreate}>
                <label>Team Name: </label>
                <input
                    type="text"
                    value={teamName}
                    onChange={(e) => setTeamName(e.target.value)}
                >
                </input>

                <button
                    type="submit"
                >
                    Create
                </button>
            </form>

            <ul>
                {teams.map(team => (
                    <li key={team.id}>{team.name}</li>
                ))}
            </ul>
        </>
        
    );
}