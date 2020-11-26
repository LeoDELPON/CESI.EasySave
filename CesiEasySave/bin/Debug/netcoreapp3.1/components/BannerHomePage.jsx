import React from 'react';
import {
    Container,
    Button,
    Modal,
    Form,
    Alert,
} from 'react-bootstrap';
import { withFirebase } from './Firebase';
import Typical from 'react-typical';


const INIT_MODAL_POPUP = {
    nom_modal: "",
    email_modal: "",
    content_modal: "",
    error: null
}

class ModalPopUpContactUs extends React.Component {
    constructor(props) {
        super(props);
        this.isSuccess = false;
        this.state = {...INIT_MODAL_POPUP};
    }

    onSubmit = event => {
        const {nom_modal, email_modal, content_modal} = this.state;
        this.props.firebase
        .doPostContactForm(nom_modal, email_modal, content_modal)
        .then(() => {
            console.log('Message succeeded');
            this.isSuccess = true;
            this.setState({ ...INIT_MODAL_POPUP});
        })
        .catch(error => {
            this.setState({ error });
            console.log(error);
        })
        event.preventDefault();
    }

    onChange = event => {
        this.setState({
            [event.target.name] : [event.target.value]
        })
    }
    
    render() {
        const {nom_modal, email_modal, content_modal, error_modal} = this.state;
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        const isInvalid = !regex.test(email_modal) || email_modal === "" || nom_modal === "" || content_modal.length === 0
        return (
            <>
            <Modal
            {...this.props}
            size="md"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            >
            <Modal.Body>
                <h4 className="modal-body-title">Ayo Ayo Moussaillon</h4>
                <Form onSubmit={this.onSubmit}>
                    <Form.Group controlId="exampleForm.ControlInput1">
                    <Form.Label className="form-label">Nom*</Form.Label>
                    <Form.Control 
                    type="name" 
                    value={nom_modal}
                    name="nom_modal"
                    placeholder="Your Name 2016"
                    onChange={this.onChange}/>
                </Form.Group>
                <Form.Group controlId="exampleForm.ControlInput2">
                    <Form.Label className="form-label">Adresse Mail*</Form.Label>
                    <Form.Control 
                    type="email" 
                    placeholder="rorona.zoro@email.com"
                    name="email_modal"
                    value={email_modal}
                    required
                    onChange={this.onChange}/>
                </Form.Group>
                <Form.Group controlId="exampleForm.ControlTextarea1">
                    <Form.Label 
                    className="form-label">Le petit message</Form.Label>
                    <Form.Control 
                    as="textarea" 
                    rows="3"
                    name="content_modal"
                    value={content_modal}
                    onChange={this.onChange}
                    placeholder="69 6E 64 69 63 65 20 31 3A 20 63 68 65 72 63 68 65 20 6C 65 20 6C 61 70 69 6E"/>
                </Form.Group>
                <Button 
                disabled={isInvalid}
                className={isInvalid ? "button-contact-modal" : "button-contact-modal-active" }
                variant="warning" 
                type="submit">
                    Envoyer
                </Button>
                {error_modal !== null ? <p >{error_modal}</p> : undefined}
                </Form>
                <Alert className={this.isSuccess ? "alert-notif-success-shown" : "alert-notif-success"} variant="success">
                    {this.isSuccess ? "Votre message a bien été envoyé par pigeon voyageur ! Merci bien :)" : undefined}
                </Alert>
            </Modal.Body>
            </Modal>
            </>
        );
    }
}

const ModalPopUpContactUsHumboldt = withFirebase(ModalPopUpContactUs)

const ContentBanner = () => {
    const [modalShow, setModalShow] = React.useState(false);
    return (
        <>
        <ModalPopUpContactUsHumboldt
        show={modalShow}
        onHide={() => setModalShow(false)}
        />
        <Container fluid className="content-banner-container-fluid">
            <Container className="content-banner-container">
                <h1 className="content-banner-title">
                    Hello World !
                </h1>
                <h5 className="content-banner-subtitle">
                    Nous sommes{' '}
                    <strong><Typical
                        loop={Infinity}
                        wrapper='b'
                        steps={[
                            'Etudiants💻',
                            2000,
                            'Passionés🪐',
                            2000,
                            'Curieux💥',
                            2000,
                            'Humboldt🍖',
                            5000,
                        ]}
                    /></strong>
                </h5>
                <Button
                variant="warning"
                className="content-banner-button"
                onClick={() => setModalShow(true)}>
                    Contactez-nous !
                </Button>
            </Container>
        </Container>
        </>
    );
}

const ImgBanner = () => {
    return (
        <Container fluid className="image-banner-container-fluid">
            <img
            src={process.env.PUBLIC_URL + "/img/ImageSite/home2.jpg"}
            alt="background room"
            className="image-banner-resize"/>
        </Container>
    );
}

const BannerContainer = () => {
    return (
        <Container fluid>
            <ContentBanner/>
            <ImgBanner/>
        </Container>
    );
}

export const BannerHumboldt = () => {
    return (
        <BannerContainer/>
    );
}