import React from 'react';
import {
    Container,
    Row,
    Col
} from 'react-bootstrap';

const PurposeContent = () => {
    return (
        <span className="purpose-content">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
            Aenean in faucibus elit. Nulla suscipit tellus vel pulvinar 
            faucibus. Interdum et malesuada fames ac ante ipsum primis in 
            faucibus. Ut pretium vel tellus sollicitudin faucibus. Mauris 
            vel volutpat augue. Praesent venenatis lectus ac ullamcorper 
            pulvinar. Pellentesque fringilla metus pretium mollis cursus.
            Cras at condimentum metus, eget molestie velit. Vestibulum 
            fringilla consectetur posuere. Donec orci dui, efficitur at 
            tempus ac, hendrerit non mauris.
        </span>
    );
}

const PurposeImgBlock = () => {
    return (
        <img 
        src={process.env.PUBLIC_URL + "/img/ImageSite/purpose.png"} 
        alt="personne qui dépose un colis en haut d'une pile"
        className="purpose-about-us-img"/>
    );
}

const PurposeContainer = () => {
    return (
        <Row className="purpose-about-us-row">
            <Col xs={4} className="purpose-container-bg">
                <PurposeImgBlock/>
            </Col>
            <Col xs={8} className="purpose-container-content">
                <PurposeTitle/>
                <PurposeContent/>
            </Col>
        </Row>
    );
}


const PurposeTitle = () => {
    return (
        <div>
            <h2 className="purpose-title">
                NOTRE OBJECTIF 
            </h2>
        </div>
    );
}


export const PurposeAboutUsHumboldt = () => {
    return (
        <>
        <Container fluid className="purpose-about-us-container-fluid">
            <PurposeContainer/>
        </Container>
        </>
    );
}