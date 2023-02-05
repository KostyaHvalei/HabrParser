import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Nav from "./components/nav.jsx";
import ArticlesList from "./components/articleslist.jsx";
import History from "./components/history.jsx";
import Schedule from "./components/schedule.jsx";
const ReactDOM = require("react-dom/client");
const React = require("react");

ReactDOM.createRoot(document.getElementById("app")).render(
    <Router>
        <div className="container">
            <Nav />
            <Routes>
                <Route path="/" element={<ArticlesList />} />
                <Route path="/history" element={<History />} />
                <Route path="/schedule" element={<Schedule />} />
            </Routes>
        </div>
    </Router>
);
