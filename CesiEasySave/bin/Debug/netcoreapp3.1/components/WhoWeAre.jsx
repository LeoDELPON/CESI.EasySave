import React from 'react';
import {
    Container,
    Row,
    Col
} from 'react-bootstrap';


const TitleWhoWeAre = () => {
    return (
        <>
        <span className="title-who-we-are">
        <a href="https://www.cesi.fr/" target="_blank" rel="noopener noreferrer">
            <h2 className="title-who-we-are-content">
                QUI SOMMES-NOUS ?
            </h2>
        </a>
        </span>
        </>
    );
}

const WhoWeAreLogo = () => {
    return (
        <img 
        src={process.env.PUBLIC_URL + "/img/ImageSite/aboutus.png"}
        className="who-we-are-section-logo"
        alt="cesi logo"/>
    );
}

const ArchitectureComponent = () => {
    return (
        <>
        <Container 
        fluid
        className="who-we-are-section-container-fluid">
            <Row
            className="who-we-are-section-row">
                <Col 
                xs={7}
                className="who-we-are-section-col">
                    <TitleWhoWeAre/>
                    <Container fluid className="who-we-are-section-col-content">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
                        Aenean in faucibus elit. Nulla suscipit tellus vel pulvinar 
                        faucibus. Interdum et malesuada fames ac ante ipsum primis in 
                        faucibus. Ut pretium vel tellus sollicitudin faucibus. Mauris 
                        vel volutpat augue. Praesent venenatis lectus ac ullamcorper 
                        pulvinar. Pellentesque fringilla metus pretium mollis cursus.
                        Cras at condimentum metus, eget molestie velit. Vestibulum 
                        fringilla consectetur posuere. Donec orci dui, efficitur at 
                        tempus ac, hendrerit non mauris.
                    </Container>
                </Col>
                <Col className="who-we-are-section-col-colour">
                <WhoWeAreLogo/>
                </Col>
            </Row>
        </Container>
        </>
    );
}



export const WhoWeAreHumboldt = () => {
    return (
        <ArchitectureComponent/>
    );
}