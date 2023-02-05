import React from "react";

export default class Article extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return(
        <div className="card mb-3">
            <div className="card-body">
                <h5 className="card-title">Title: {this.props.title}</h5>
                <p className="card-text">Author: {this.props.creator}</p>
                <p className="card-text">Content: {this.props.content}</p>
                <p className="card-text">Date of creation: {this.props.publishedAt}</p>
                <a className="btn btn-primary" target="_blank" href={this.props.link}>
                    Go to Habr.com
                </a>
            </div>
            <img className="card-img-bottom" src={this.props.imageLink} alt="" />
        </div>
        );
    }
}