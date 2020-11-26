import React from 'react';
import {
    PasswordForgetFormComponent,
    ChangePasswordFormComponentHumboldt
} from '../ForgetAndChangePwd';
import {
    Container,
    Row,
    Card,
    Col
} from 'react-bootstrap';
import { withAuthorization } from '../Session';
import {FooterHumboldt} from '../Footer';
import {
    PseudoChangeAccountHumboldt,
    EmailAccountHumboldt,
    PictureAccountHumboldt,
    MyProfilCVAccount,
    DeleteAccountHumboldt,
    DashBoardAccount
} from '../DataAccount';


const AccountContent = () => {
    return(
        <Row>
            <Col className="account-content-col-left">
                <Card>
                    <PictureAccountHumboldt/>
                    <PseudoChangeAccountHumboldt/>
                    {/* <EmailAccountHumboldt/> */}
                    <ChangePasswordFormComponentHumboldt/>
                </Card>
                <Card>
                    <DeleteAccountHumboldt/>
                </Card>
            </Col>
            <Col className="account-content-col-right">
                <Card>
                    
                </Card>
                <Card>

                </Card>
                <Card>

                </Card>
            </Col>
        </Row>
    );
}

const ArchitectureAccount = () => {
    return(
        <>
        <Container fluid className="architecture-account-container-fluid">
            <Container className="architecture-account-container-header">
                <img 
                src={process.env.PUBLIC_URL + "/img/ImageSite/userProfilSettings.png"}
                className="architecture-account-img-header"
                alt="logo profil"/>
                <h2>
                    PARAMETRE
                </h2>
            </Container>
        </Container>
        <Container fluid className="architecture-account-sub-header-container-fluid ">
            <div className="architecture-account-separation">
            </div>
            <Container className="architecture-account-container-sub-header">
                <img
                src={process.env.PUBLIC_URL + "/img/ImageSite/user.png"}
                className="architecture-account-sub-header-img"/>
                <h3>
                    PROFIL
                </h3>
            </Container>
        </Container>
        <Container fluid className="architecture-account-content-container-fluid">
            <div className="architecture-account-content-container">
                <AccountContent/>
            </div>
        </Container>
        <FooterHumboldt/>
        </>
    );
}


const AccountHumboldt = () => {
    return (
        <>
        <Container fluid className="account-bg">
        <ArchitectureAccount/>
        </Container>
        </>
    );
}
const condition = authUser => !!authUser;

export default withAuthorization(condition)(AccountHumboldt);