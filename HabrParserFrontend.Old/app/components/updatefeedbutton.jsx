import React from "react";

export default class UpdateFeedButton extends React.Component {
    constructor(props) {
        super(props);

        this.state = { message: "", isLoading: false };
        this.onClick = this.onClick.bind(this);
    }

    onClick() {
        this.updateFeed();
    }

    updateFeed() {
        this.setState({ isLoading: true });
        fetch("https://localhost:44373/api/feed", {
            method: "POST",
        })
            .then((response) => response.text())
            .then((data) => this.setState({ message: data }));
    }

    render() {
        let text = "Update Feed";
        if (this.state.message.length > 0) {
            alert(this.state.message);
            this.setState({ message: "", isLoading: false });
        }
        if (this.state.isLoading) {
            text = "Loading...";
        }
        return (
            <button className="btn btn-secondary" onClick={this.onClick}>
                {text}
            </button>
        );
    }
}
