import React from 'react';
import { 
    Jumbotron,
    Container,
    Row ,
    Col
} from 'react-bootstrap';
import {fiches} from '../dataMock/ficheMock';
import * as ROUTE from '../constant/route';


const FooterCopyRight = () => {
    return(
        <>
        <div>
            <img 
            src=""
            alt=""/>
            <span>

            </span>
        </div>
        </>
    );
}


const ListFooterCard = () => {
    const cards = [
        ["facebook.png", "145k", "Followers"],
        ["twitter.png", "20K", "Followers"],
        ["pirate.png", "10k", "Membres"],
        ["discord.png", "1k", "Membres"],
        ["youtube.png", "645", "Abonnés"],
    ]
    return(
        <>
        <Container fluid className="list-platform-container-fluid">
            <Container>
                <Row>
                    {
                        cards.map((card, index) => {
                            return(
                                <Col className="list-platform-col" key={index}>
                                    <FooterCardNumberFollowerOrMember card={card}/>
                                </Col>
                            );
                        })
                    }
                </Row>
            </Container>
        </Container>
        </>
    );
}

const ListCategoryFooterComponent = () => {
    const cat = [
        ["Cybersecurite", "/cyber"],
        ["Developpement", "dev"],
        ["Réseau", "/reseau"],
        ["Système", "/sys"],
        ["Electronique embarqué", "/electro-emb"]
    ]
    return(
        <>
            {
                cat.map( (category, index) => {
                    return(
                        <a href={category[1]} key={index}>
                            <span>
                                {
                                    category[0]
                                }
                            </span>
                        </a>
                    );
                })
            }
        </>
    );
}

const ListHumboldtFooterComponent = () => {
    const contents = [
        ["A propos", ROUTE.ABOUT_US ],
        ["Contact", ROUTE.CONTACT],
        ["FAQ", ROUTE.FAQ],
        ["CGU", ROUTE.FAQ],
        ["Mentions Légales", ROUTE.POLITIQUE_CONF],
        ["Se connecter", ROUTE.LOGIN]
    ]
    return(
        <>
            {
                contents.map( (content, index) => {
                    return(
                        <a href={content[1]} key={index}>
                            <span>
                                {content[0]}
                            </span>
                        </a>
                    );
                })
            }
        </>
    );
}

const ListProfilFooterComponent = () => {
    return(
        <>
            {
                fiches.map((fiche, index) => {
                    return(
                        <a href={"/" + fiche.profil} key={index}>
                            <span>
                                {fiche.profil}
                            </span>
                        </a>
                    );
                })
            }
        </>
    );
}

const FooterArchitecture = () => {
    const Brand = [
        ["CATEGORIES", <ListCategoryFooterComponent/>],
        ["HUMBOLDT", <ListHumboldtFooterComponent/> ],
        ["PROFILS", <ListProfilFooterComponent/>]
    ]
    return(
        <Container fluid className="footer-menu-container-fluid">
            <Container className="footer-menu-container">
                <footer>
                    <Row>
                        {Brand.map((brand, index) => {
                            return(
                                <Col className="footer-menu-col" key={index}>
                                    <h3>
                                        {brand[0]}
                                    </h3>
                                    <div className="footer-menu-div-content">
                                        {brand[1]}
                                    </div>
                                </Col>
                            );
                        })}
                    </Row>
                </footer>
            </Container>
        </Container>
    );
}

const IconesFooterComponent = () => {
    const icons = [
        "facebook.png",
        "twitter.png",
        "discord.png"
    ]
    return(
        <>
        <Container fluid className="icons-container-fluid">
            <Container className="icons-container">
                {
                    icons.map((icon, index) => {
                        return(
                            <a href="/" key={index}>
                                <img 
                                src={process.env.PUBLIC_URL + "img/Icones/" + icon}
                                alt=""
                                className="icons-img"
                                />
                            </a>
                        );
                    })
                }
            </Container>
        </Container>
        </>
    );
}

const FooterCardNumberFollowerOrMember = ({card}) => {
    return(
        <Jumbotron className="list-platform-jumbotron">
            <img
            src={process.env.PUBLIC_URL + "/img/ImageSite/" + card[0]}
            alt={card[0]}/>
            <span className="list-platform-content-number">
                {card[1]}
            </span>
            <span className="list-platform-content-libelle-number">
                {card[2]}
            </span>
        </Jumbotron>
    );
}

export const FooterHumboldt = () => {
    return (
        <>
        <Container fluid className="footer-background">
            <ListFooterCard/>
            <FooterArchitecture/>
            <IconesFooterComponent/>
            <FooterCopyRight/>
        </Container>
        </>
    );
}

