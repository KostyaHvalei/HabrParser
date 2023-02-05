import React from "react";

export default class ScheduleForm extends React.Component {
    constructor(props) {
        super(props);

        this.state = { schedule: "" };

        this.onChange = this.onChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    onChange(e) {
        let value = e.target.value;
        this.setState({ schedule: value });
    }

    async send() {
        let cronschedule = { cronschedule: this.state.schedule };
        const response = await fetch("https://localhost:44339/api/schedule", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(cronschedule),
        }).then((data) => console.log(data));
    }

    async handleSubmit(e) {
        await this.send();
        await this.props.rerenderParentCallback();
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <div className="form-group">
                    <label>Schedule: </label>
                    <input
                        type="text"
                        value={this.state.schedule}
                        onChange={this.onChange}
                    />
                </div>
                <input type="submit" value="Send" className="btn btn-primary" />
            </form>
        );
    }
}
