import { BrowserRouter, Routes, Route } from "react-router-dom";
import './App.css'
import TeamsPage from './pages/TeamsPage'
import PlayersPage from "./pages/PlayersPage";

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/teams" element={<TeamsPage/>}/>
        <Route path="/players" element={<PlayersPage/>}/>
      </Routes>
    </BrowserRouter>
  )
}

export default App
