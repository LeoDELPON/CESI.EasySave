import React from 'react';
import { 
    Col,
    Container,
    Row,
    Button
} from 'react-bootstrap';

const PurposeLookingForPirate = () => {
    return(
        <>
        <div className="looking-for-job-content-div">
            <span>
                En clicquant sur ce bouton, il vous est possible de proposer des missions de tout type (programmation, referencement ou autres...)
                en fonction de certains critères comme la catégorie, le niveau de difficulté, et encore pleins d'autres !
            </span>
            <a href="/">
                <Button 
                variant="warning"
                className="looking-for-job-button">
                    Poster une mission
                </Button>
            </a>
        </div>
        </>
    );
}

const TraitPurposeLookingForPirate = () => {
    return(
        <>
        <div className="trait">

        </div>
        </>
    );
}


const LookingForPirateTitle = () => {
    return(
        <>
        <div>
            <h2 className="looking-for-job-content-title">
                Une mission à proposer ?
            </h2>
            <h3 className="looking-for-job-content-subtitle">
                Vous êtes un PNJ à la recherche de braves aventuriers en quête de sensation forte ?
            </h3>
        </div>
        </>
    );
}



const PurposeImgBlock = () => {
    return(
        <img 
        src={process.env.PUBLIC_URL + "img/ImageSite/mission.png"} 
        alt="représentation d'une fusée"
        className="looking-for-job-img"/>
    );
}

const LookingForPirateContainer = () => {
    return(
        <>
        <TraitPurposeLookingForPirate/>
        <Container fluid className="looking-for-job-container-fluid">
            <Row>
                <Col xs={4} className="looking-for-job-background">
                    <PurposeImgBlock/>
                </Col>
                <Col xs={8} className="looking-for-job-content">
                    <LookingForPirateTitle/>
                    <PurposeLookingForPirate/>
                </Col>
            </Row>
        </Container>
        </>
    );
}

export const LookingForPiratesHumboldt = () => {
    return(
        <>
        <LookingForPirateContainer/>
        </>
    );
}