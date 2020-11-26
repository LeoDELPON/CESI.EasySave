import React from 'react';
import {
    Container,
    Row,
    Col
} from 'react-bootstrap';


const TitleOurSchool = () => {
    return (
        <>
        <span className="title-our-school">
        <a href="https://www.cesi.fr/" target="_blank" rel="noopener noreferrer">
            <h2 className="title-our-school-content">
                NOTRE ECOLE
            </h2>
        </a>
        </span>
        </>
    );
}

const SchoolLogo = () => {
    return (
        <img 
        src={process.env.PUBLIC_URL + "/img/ImageSite/cesi.png"}
        className="school-section-logo"
        alt="school logo"/>
    );
}

const ArchitectureComponent = () => {
    return (
        <>
        <Container 
        fluid
        className="school-section-container-fluid">
            <Row
            className="school-section-row">
                <Col 
                xs={8}
                className="school-section-col">
                    <Container className="school-section-col-content">
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
                <Col className="school-section-col-colour">
                <SchoolLogo/>
                </Col>
                <TitleOurSchool/>
            </Row>
        </Container>
        </>
    );
}



export const OurSchoolHumboldt = () => {
    return (
        <ArchitectureComponent/>
    );
}