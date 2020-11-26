import React from 'react';
import {
    Container,
    Card,
    ListGroup,
    Button
} from 'react-bootstrap';
import {articles} from '../dataMock/articleMock';

const ComponentHelpUsOnPatreon = () => {
    return (
        <Container className="component-help-us-patreon-container" >
            <Card 
            className="component-help-us-patreon-card"
            bg="warning">
            <Card.Header className="component-help-us-patreon-header">Donations</Card.Header>
            <ListGroup variant="flush">
                <ListGroup.Item className="component-help-us-patreon-list">
                    <img 
                    src={process.env.PUBLIC_URL + "/img/Icones/patreon.png"} 
                    alt="logo patreon"
                    className="component-help-us-patreon-list-img"/>
                    <h2 className="component-help-us-patreon-list-title">
                        SUPPORTEZ NOUS SUR PATREON :)
                    </h2>
                    <a href="/">
                        <Button variant="warning" className="component-help-us-patreon-list-button">
                            Merci !
                        </Button>
                    </a>
                </ListGroup.Item>
            </ListGroup>
            </Card>
        </Container>
    );
}

const ComponentBestArticles = () => {
    return (
        <Container className="component-best-article-container">
            <Card
            className="component-best-article-card">
            <Card.Header
            className="component-best-article-card-header">Nos Articles Pref</Card.Header>
            <ListGroup variant="flush">
                {
                    articles.map(article => {
                        return(
                            <a href={article.reference}>
                                <ListGroup.Item>{article.titre}</ListGroup.Item>
                            </a>
                        );
                    })
                }
            </ListGroup>
            </Card>
        </Container>
    );
}

const ComponentMessageBox = () => {
    return (
        <Container className="component-message-box-container">
            <Card
            bg="dark"
            key={1}
            text="white"
            className="component-message-box-card">
                <Card.Body>
                <Card.Title> IMPORTANT ! </Card.Title>
                <Card.Text>
                    L'équipe Humboldt est activement à la recherche d'un stage pour
                    le 13 Janvier 2021. Si vous êtes intéressé, nous vous invitons à
                    nous contacter via la page Contactez-nous.
                </Card.Text>
                </Card.Body>
            </Card>
        </Container>
    );
}


export const ArticleHomeRigthHumboldt = () => {
    return(
        <>
            <ComponentHelpUsOnPatreon/>
            <ComponentMessageBox/>
            <ComponentBestArticles/>
        </>
    );
}