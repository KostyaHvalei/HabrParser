import React from "react";
import ScheduleForm from "./scheduleform.jsx";
import ClearScheduleButton from "./clearschedulebutton.jsx";

export default class Schedule extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            schedule: "",
            isLoaded: false,
        };

        this.getSchedule = this.getSchedule.bind(this);
    }
    async componentDidMount() {
        await this.getSchedule();
    }

    async getSchedule() {
        await fetch("https://localhost:44373/api/schedule")
            .then((response) => response.text())
            .then((data) => {
                this.setState({ schedule: data, isLoaded: true });
            });
    }

    async rerenderCallback() {
        await this.getSchedule();
    }

    render() {
        const isLoaded = this.state.isLoaded;

        if (isLoaded) {
            if (this.state.schedule.length >= 15) {
                return (
                    <div
                        id="layout-content"
                        className="layout-content-wrapper col-md-6"
                    >
                        <p>There is no schedule. You can make it:</p>
                        <ScheduleForm
                            rerenderParentCallback={this.getSchedule}
                        />
                    </div>
                );
            } else {
                return (
                    <div
                        id="layout-content"
                        className="layout-content-wrapper col-md-6"
                    >
                        <p>Current Cron Schedule: {this.state.schedule}</p>
                        <p>You can update it: </p>
                        <ScheduleForm
                            rerenderParentCallback={this.getSchedule}
                        />
                        <p>Or delete it:</p>
                        <ClearScheduleButton
                            rerenderParentCallback={this.getSchedule}
                        />
                    </div>
                );
            }
        } else {
            return (
                <h1>
                    <div className="col">Loading...</div>
                </h1>
            );
        }
    }
}
