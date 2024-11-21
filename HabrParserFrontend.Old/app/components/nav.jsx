import React from "react";
import { Link } from "react-router-dom";
import UpdateFeedButton from "./updatefeedbutton.jsx";

export default function Nav() {
    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <button
                    className="navbar-toggler"
                    type="button"
                    data-toggle="collapse"
                    data-target="#navbarNav"
                    aria-controls="navbarNav"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <Link className="nav-link" to="/">
                                Articles
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/history">
                                History
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/schedule">
                                Schedule
                            </Link>
                        </li>
                        <li className="nav-item">
                            <UpdateFeedButton />
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
    );
}
