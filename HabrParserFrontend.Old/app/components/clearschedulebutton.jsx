import React from "react";

export default class ClearScheduleButton extends React.Component {
    constructor(props) {
        super(props);

        this.state = { isDeleting: false };
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    async send() {
        this.setState({ isDeleting: true });
        await fetch("https://localhost:44373/api/schedule", {
            method: "DELETE",
        }).then((resp) => {
            console.log(resp.status);
        });
        this.setState({ isDeleting: false });
    }

    async handleSubmit(e) {
        await this.send();
        await this.props.rerenderParentCallback();
    }

    render() {
        let buttonText = "Delete";
        if (this.state.isDeleting) {
            buttonText = "Deleting...";
        }
        return (
            <button className="btn btn-primary" onClick={this.handleSubmit}>
                {buttonText}
            </button>
        );
    }
}
