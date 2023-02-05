import React from "react";
import Article from "./article.jsx";

export default class ArticlesList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            articles: [],
            isLoaded: false,
        };
    }

    componentDidMount() {
        fetch("https://localhost:44373/api/feed")
            .then((response) => response.json())
            .then((data) => {
                this.setState({ articles: data, isLoaded: true });
            });
    }
    render() {
        const articles = this.state.articles.map((item, i) => (
            <li key={item.id}>
                <Article 
                    id={item.id}
                    title={item.title}
                    creator={item.creator}
                    previewText={item.previewText}
                    publicationDate={item.publicationDate}
                    link={item.link}
                    imageLink={item.imageLink}
                />
            </li>
        ));
        const isLoaded = this.state.isLoaded;

        if (isLoaded) {
            if(articles.length > 0){
                return (
                    <ul>{articles}</ul>
                );
            }
            else{
                return <h2>There is no articles in db</h2>;
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
