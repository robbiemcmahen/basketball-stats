import { BrowserRouter, Routes, Route } from "react-router-dom";
import './App.css'
import TeamsPage from './pages/TeamsPage'
import PlayersPage from "./pages/PlayersPage";
import GameSetupPage from "./pages/GameSetupPage";

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/teams" element={<TeamsPage/>}/>
        <Route path="/players" element={<PlayersPage/>}/>
        <Route path="/game-setup" element={<GameSetupPage/>}/>
      </Routes>
    </BrowserRouter>
  )
}

export default App
