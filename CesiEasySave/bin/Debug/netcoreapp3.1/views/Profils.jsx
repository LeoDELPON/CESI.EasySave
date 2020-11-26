import React from 'react'
import {
    ListeEquipageHumboldt,
    ArchiProfilButtonHumboldt,
    ArchiFormationCentreInteretHumboldt,
    ArchiCompetenceEtAbiliteHumboldt,
    ExperienceProHumboldt,
    ContactFicheHumboldt
} from '../FichesComponents';
import {Container} from 'react-bootstrap';
import {FooterHumboldt} from '../Footer';
import {LookingForPiratesHumboldt} from '../LookingForPirates';


const ProfilHumboldt = () => {
    return (
        <>
        <Container fluid className="bg-profil">
            <ListeEquipageHumboldt/>
            <ArchiProfilButtonHumboldt/>
            <ArchiFormationCentreInteretHumboldt/>
            <ArchiCompetenceEtAbiliteHumboldt/>
            <ExperienceProHumboldt/>
            <LookingForPiratesHumboldt/>
            <FooterHumboldt/>
        </Container>
        </>
    );
}

export default ProfilHumboldt;