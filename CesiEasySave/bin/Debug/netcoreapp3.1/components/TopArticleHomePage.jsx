import React from 'react';
import {
    Container,
    Jumbotron,
    Card,
    Col,
    Row,
    Badge
} from 'react-bootstrap';
import {articles} from '../dataMock/articleMock';


const ListComponentsArticle = () => {
    const newList = [];
    for(var i = 0; i< articles.length; ++i) {
        if(articles[i].like > 100)
            newList.push(articles[i]);
        if(newList.length > 3)
            break;
    }
    return (
        <>
        <Jumbotron>
            <h2 className="list-article-title">
                Articles Populaires
            </h2>
            <Row> 
                {
                    newList.map((article, index) => {
                        return (
                            <Col className="list-article-jumbotron" index={index}>
                                <ComponentArticle articleContent={article} index={index}/> 
                            </Col>
                        );
                    })
                }
            </Row>
        </Jumbotron>
        </>
    );
}

const BadgeComponentArticle = ({categorie}) => {
    return (
        <a href={categorie}>
            <Badge variant="danger">{categorie}</Badge>
        </a>
    );
}

const ComponentArticle = props => {
    const {articleContent, index} = props;
    return (
        <>
        <div >
            <a href={articleContent.reference}>
                <Card
                    bg={index % 2 === 0 ? "dark" : "warning"}
                    key={index}
                    text={index % 2 === 0 ? "white" : "black"}
                    style={{ width: '18rem' }}
                    className="list-article-card">
                    {/* <Card.Header>Header</Card.Header> */}
                    <Card.Body key={index+10}>
                        <Card.Title className="list-article-author" key={index+20}>
                            {articleContent.auteur}
                        </Card.Title>
                        <Card.Text key={index+30}>
                            {articleContent.titre}
                        </Card.Text>
                        <br></br>
                        <div >
                            <Row key={index+40}>
                                <Col key={index+50} >
                                    <span className="list-article-number-icon">
                                    {articleContent.like} 
                                    </span>
                                    <img 
                                    src={process.env.PUBLIC_URL + "/img/Icones/heart.png"}
                                    alt="logo coeur"
                                    className="list-article-icon"/>
                                </Col>
                                <Col key={index+60}>
                                    <span className="list-article-number-icon">
                                        {articleContent.comment}
                                    </span>
                                    <img 
                                    src={process.env.PUBLIC_URL + "img/Icones/comment.png"}
                                    alt="logo commentaire"
                                    className="list-article-icon"/>
                                </Col>
                            </Row>
                        </div>
                    </Card.Body>
                </Card>
            </a>
            <BadgeComponentArticle categorie={articleContent.categorie}/>
        </div>
        </>
    );
}


export const TopArticleHomeHumboldt = () => {
    return (
        <>  
            <Container fluid>
            </Container>
            <Container fluid>
                <ListComponentsArticle/>
            </Container>
        </>
    );
}