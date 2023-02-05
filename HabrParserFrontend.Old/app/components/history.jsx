import React from "react";

export default class History extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            history: [],
            isLoaded: false,
        };
    }

    componentDidMount() {
        fetch("https://localhost:44339/api/history")
            .then((response) => response.json())
            .then((data) => {
                this.setState({ history: data, isLoaded: true });
            });
    }

    render() {
        const history = this.state.history.map((item, i) => (
            <li>
                <div key={item.id}>
                    <p>CountAdded: {item.countAdded}</p>
                    <p>Added at: {item.addedAt}</p>
                    <p>Automaticly: {item.automaticly.toString()}</p>
                </div>
            </li>
        ));

        const isLoaded = this.state.isLoaded;

        if (isLoaded) {
            return (
                <div
                    id="layout-content"
                    className="layout-content-wrapper col-md-6"
                >
                    <div className="panel-list">
                        <ul>{history.reverse()}</ul>
                    </div>
                </div>
            );
        } else {
            return (
                <h1>
                    <div className="col">Loading...</div>
                </h1>
            );
        }
    }
}
