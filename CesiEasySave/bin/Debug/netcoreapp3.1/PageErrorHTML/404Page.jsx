import React from 'react';
import {
    Container,
    Row,
    Col,
    Button
} from 'react-bootstrap';
import * as ROUTE from '../../constant/route';

class Page404Humboldt extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return(
            <>
            <Component404/>
            </>
        );
    }
}


const Component404 = () => {
    return(
        <>
        <Container fluid className="page-404-container-fluid">
            <Container className="page-404-container">
                <Row className="page-404-row">
                    <Col className="page-404-content-col">
                        <Row>
                            <Row className="page-404-content-div">
                                <h3>
                                    4
                                </h3>
                                <h3>
                                    0
                                </h3>
                                <h3>
                                    4
                                </h3>
                            </Row>
                            <span className="page-404-content-text">
                                OOPS quelque chose ne va pas, nous sommes désolé mais cette page est en cours de construction par nos chers développeurs ! 
                                Sincèrement nous, Humboldt-Life :) 
                            </span>
                            <Row className="page-404-content-button-row">
                                <a
                                href={ROUTE.HOME_ARTICLE}>
                                <Button variant="warning">
                                    Blog
                                </Button>
                                </a>
                                <a
                                href={ROUTE.HOME}>
                                <Button variant="warning">
                                    Accueil
                                </Button>
                                </a>
                            </Row>
                        </Row>
                    </Col>
                    <Col className="page-404-content-image-jojo-col">
                        <img
                        className="page-404-content-image-jojo"
                        src={process.env.PUBLIC_URL + "/img/ImageSite/jojo-bizarre-aventure.jpeg"}
                        alt="image représentant un meme sympa de Jojo sur la page 404"/>
                    </Col>
                </Row>
            </Container>
        </Container>
        </>
    );
}


export default Page404Humboldt;