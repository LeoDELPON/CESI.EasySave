import React from 'react';
import {
    Container, 
    Jumbotron,
    Button
} from 'react-bootstrap';

const AccessProfilContent = () => {
    return(
        <Jumbotron className="acces-profil-jumbotron">
            <h3>
                Envie de découvrir l'équipage Humboldt ?
            </h3>
            <span className="access-profil-content">
                Cette équipe s'est formée au cours des trois dernières années dans un but bien précis, partager nos connaissances aux jeunes mais également au plus vieux.
                Curieux et entreprenant de nature, nous esperons ,par notre apprentissage à l'école mais également grâce à nos experiences personnelles, vous faire découvrir 
                au maximum l'univers complexe et fantasmagorique de l'informatique ! 
            </span>
            <a href="/profil">
                <Button variant="warning">
                    Accès à la caravelle
                </Button>
            </a>
            <span className="acces-profil-sous-content">
                Ce contenu est sponsorisé par nous-même, bien cordialement, <strong> la direction Humboldt </strong>
            </span>
        </Jumbotron>
    );
}

const AccessProfilArchitecture = () => {
    return(
        <>
        <Container fluid className="acces-profil-container-fluid">
            <Container className="acces-profil-container">
                <AccessProfilContent/>
            </Container>
        </Container>
        </>
    );
}


export const AccessProfilHomeHumboldt = () => {
    return(
        <>
        <AccessProfilArchitecture/>
        </>
    );
}