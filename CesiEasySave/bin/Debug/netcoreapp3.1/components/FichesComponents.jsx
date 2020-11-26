import React from 'react';
import {fiches} from '../dataMock/ficheMock'
import {
    Container,
    Button,
    Jumbotron,
    Row,
    Col,
    Card,
    ProgressBar
} from 'react-bootstrap';
import { Radar, Polar } from 'react-chartjs-2';

const fichesHumboldt = fiches;

const InfoPerso = () => {
    return (
        <Jumbotron className="info-perso-jumbotron">
            <Row>
                <Col xs={6}>
                    <div className="info-perso-div-img-content">
                        <img 
                        src={process.env.PUBLIC_URL + "/img/ImageProfil/" + fichesHumboldt[0].content.picture} 
                        alt="profil utilisateur"
                        className="info-perso-img"/>
                        <span className="info-perso-main-content">
                            <span className="info-perso-main-content-span">
                                {fichesHumboldt[0].content.prenom + " " + fichesHumboldt[0].content.nom }
                            </span>
                            <span className="info-perso-main-content-span">
                                {fichesHumboldt[0].content.rang}
                            </span>
                        </span>
                    </div>
                    <div className="info-perso-div-caracteristiques">
                        <h2>
                            Caracteristiques
                        </h2>
                        <span className="info-perso-libelle-caracteristiques">
                            Haki de l'Observation
                        </span>
                        <span>
                            <ProgressBar animated={true} now={70} variant="danger"/>
                        </span>
                        <span  className="info-perso-libelle-caracteristiques">
                            Haki de l'Armement
                        </span>
                        <span>
                        <ProgressBar animated={true} now={30} variant="warning"/>
                        </span>
                        <span  className="info-perso-libelle-caracteristiques">
                            Haki des Rois
                        </span>
                        <span>
                        <ProgressBar animated={true} now={100} variant="success"/>
                        </span>
                    </div>
                </Col>
                <Col xs={6} className="info-perso-col-dialecte">
                    <h2>
                        Dialecte
                    </h2>
                    {
                        fichesHumboldt.map(fiche =>
                                fiche.content.langues.map( langue => {
                                    return(
                                        <>
                                        <span className="info-perso-dialecte-libelle">
                                            {langue[0]}
                                        </span>
                                        <span>
                                        <ProgressBar animated={true} now={langue[1]} variant="warning"/>
                                        </span>
                                        </>
                                    );
                                }
                                    
                                )
                            )
                    }
                </Col>
            </Row>
        </Jumbotron>
    );
}

const ButtonMesProjets = () => {
    return (
        <span>
            <a 
            href={"profil/" + fiches.profil + "/projets"}
            className="mes-projets-link">
                <Button variant="warning" className="mes-projets-button">  
                    Mes Projets plus ou moins originaux
                </Button>
            </a>
        </span>
    );
}

const CentreInteretHumboldt = () => {
    //10 centre d'interet max 
    var moitieUn = []
    var moitieDeux = []
    for(var i =0; i < Math.floor(fichesHumboldt[0].centreInteret.length / 2); ++i) {
        moitieUn.push(fichesHumboldt[0].centreInteret[i]);
    }
    for(var j = Math.floor(fichesHumboldt[0].centreInteret.length / 2); j < fichesHumboldt[0].centreInteret.length; ++j ) {
        moitieDeux.push(fichesHumboldt[0].centreInteret[j]);
    }
    return (
        <>
        <Jumbotron className="centre-interet-jumbotron">
            <h2>
                Compétences spéciales
            </h2>
            <Row>
                <Col className="centre-interet-col">
                    {
                        moitieUn.map( i => {
                            return(
                                <span>
                                    {i}
                                </span>
                            );
                        })
                    }
                </Col>
                <Col className="centre-interet-col">
                    {
                        moitieDeux.map( i => {
                            return(
                                <span>
                                    {i}
                                </span>
                            );
                        })
                    }
                </Col>
            </Row>
        </Jumbotron>
        </>
    );
}

const FormationHumboldt = () => {
    return (
        <>
        <Jumbotron className="formation-jumbotron">
            <h2>
                Les îles parcourues
            </h2>
            <Row>
                {
                    fichesHumboldt.map( fiche => 
                        fiche.formation.map(f => {
                            return(
                                <Col className="formation-col">
                                    <Card className="formation-card">
                                        <span>
                                            {f[0]}
                                        </span>
                                        <span>
                                            {f[1]}
                                        </span>
                                    </Card>
                                </Col>
                            );
                        })
                        )
                }
            </Row>
        </Jumbotron>
        </>
    );
}

const CompetencesHumboldt = () => {
    var competencesLabel = [];
    var DataCount = [];
    fichesHumboldt[0].competences.forEach(
        (competenceLib) => {
            competencesLabel.push(competenceLib[0])
        }
    )
    fichesHumboldt[0].competences.forEach(
        (competenceGrade) => {
            DataCount.push(competenceGrade[1]);
        }
    )
    const data = {
        labels: competencesLabel,
        datasets: [
            {
            label: 'Caractéristiques du Pirate',
            backgroundColor: 'rgba(255,99,132,0.2)',
            borderColor: 'rgba(255,99,132,1)',
            pointBackgroundColor: 'rgba(255,99,132,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(255,99,132,1)',
            data: DataCount
            }
        ]
        };
    return (
        <>
        <Radar data={data}/>
        </>
    );
}

const CompetencesTransHumboldt = () => {
    return (
        <>
        <Jumbotron className="competence-trans-jumbotron">
            <h2>
                Abilités
            </h2>
            <Card className="competence-trans-div">
                {
                    fichesHumboldt[0].competenceTrans.map(c => {
                        return(
                            <>
                            <span className="competence-trans-span-title">
                                {c[0]}
                            </span>
                            <span>
                            <ProgressBar animated={true} now={c[1]} variant="success"/>
                            </span>
                            </>
                        );
                    })
                }
            </Card>
        </Jumbotron>
        </>
    );
}

export const ArchiCompetenceEtAbiliteHumboldt = () => {
    return (
        <>
        <Container fluid className="archi-competence-et-trans-container-fluid">
            <Row>
                <Col xs={6}>
                    <CompetencesHumboldt/>
                </Col>
                <Col xs={6}>
                    <CompetencesTransHumboldt/>
                </Col>
            </Row>
        </Container>
        </>
    );
}

export const ArchiFormationCentreInteretHumboldt = () => {
    return(
        <>
        <Container fluid className="archi-formation-centre-interet-container-fluid">
            <Row>
                <Col xs={5}>
                    <CentreInteretHumboldt/>
                </Col>
                <Col xs={7}>
                    <FormationHumboldt/>
                </Col>
            </Row>
        </Container>
        </>
    );
}

export const ListeEquipageHumboldt = () => {
    return (
        <>
            <Container fluid className="team-list-container-fluid">
                <h2 className="team-list-title">
                    Membres de l'équipage 
                </h2>
                <div className="team-list-container">
                    {
                        fichesHumboldt.map( fiche => 
                            <a 
                            href={"/profil/" + fiche.profil}
                            className="team-list-link">
                                <Button variant="warning" className="team-list-button">
                                    {fiche.profil}
                                </Button>
                            </a>
                            )
                    }
                </div>
            </Container>
        </>
    );
}


export const ArchiProfilButtonHumboldt = () => {
    return (
        <>
        <Container fluid>
            <Row>
                <Col xs={7}>
                    <InfoPerso/>
                </Col>
                <Col xs={5} className="archi-profil-button">
                    <ButtonMesProjets/>
                </Col>
            </Row>
        </Container>
        </>
    );
}

export const ExperienceProHumboldt = () => {
    return (
        <>
        <Container fluid className="archi-experience-container-fluid">
            <Jumbotron className="archi-experience-jumbotron">
                <h2>
                    Experiences en piraterie
                </h2>
                <Row>
                    {
                        fichesHumboldt[0].experience.map( e=> {
                            return(
                                <Col className="archi-experience-col">
                                    <Card>
                                        <span className="archi-experience-titre">
                                            {e[0]}
                                        </span>
                                        <span className="archi-experience-date">
                                            {e[1]}
                                        </span>
                                        <span className="archi-experience-content">
                                            {e[2]}
                                        </span>
                                    </Card>
                                </Col>
                            );
                        })
                    }
                </Row>
            </Jumbotron>
        </Container>
        </>
    );
}