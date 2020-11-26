import React, {Component} from 'react';
import {
    Container,
    Form,
    Button,
    Jumbotron,
    Row,
    Col,
    Alert
} from 'react-bootstrap';
import ParticlesBg from 'particles-bg' 
import {withFirebase} from './Firebase'

const TitleContactForm = () => {
    return (
        <>
        </>
    );
}

const INIT_CONTACT = {
    nom : "",
    email: "",
    content: "",
    error : null
}

class ContactForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {...INIT_CONTACT};
        this.isSuccess = false;
    }

    onSubmit = event => {
        const {nom, email, content} = this.state;
        this.props.firebase
        .doPostContactForm(nom, email, content)
        .then(() => {
            console.log('Synchronization succeeded');
            this.isSuccess = true;
            this.setState({ ...INIT_CONTACT});
        })
        .catch(error => {
            console.log(error)
            this.setState({ error })
        });
        event.preventDefault();
    }

    onChange = event => {
        this.setState({
            [event.target.name] : [event.target.value]
        })
    }

    render() {
        const {nom, email, content, error} = this.state
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        const isInvalid = !regex.test(email) || email === "" || nom === "" || content.length === 0
        return (
            <>
            <Jumbotron className="jumbotron-contact-us">
                <Container fluid>
                <Row>
                    <Col>
                        <img 
                        className="img-contact-us"
                        src={process.env.PUBLIC_URL + "/img/ImageSite/contact.png"}
                        alt="pizza boii"/>
                    </Col>
                    <Col className="col-contact-us">
                        <div className="title-contact-us">
                            <h3>
                                Envoyez nous un message pirate !
                            </h3>
                        </div>
                        <Form className="form-contact-us-container" onSubmit={this.onSubmit}>
                            <Form.Group controlId="exampleForm.ControlInput2" >
                                <Form.Label className="form-contact-us-label">Nom*</Form.Label>
                                <Form.Control 
                                className="form-contact-us-control" 
                                type="text" 
                                placeholder="Ton Nom" 
                                name="nom"
                                value={nom}
                                required 
                                onChange={this.onChange}/>
                            </Form.Group>
                            <Form.Group controlId="exampleForm.ControlInput2">
                                <Form.Label className="form-contact-us-label">Adresse Mail*</Form.Label>
                                <Form.Control 
                                className="form-contact-us-control" 
                                type="email" 
                                placeholder="dio.brando@jojo.com" 
                                name="email"
                                value={email} 
                                onChange={this.onChange} 
                                required/>
                            </Form.Group>
                            <Form.Group controlId="exampleForm.ControlTextarea1">
                                <Form.Label className="form-contact-us-label">Le petit message</Form.Label>
                                <Form.Control 
                                className="form-contact-us-control" 
                                as="textarea" 
                                rows="3" 
                                name="content"
                                value={content} 
                                required
                                onChange={this.onChange}/>
                            </Form.Group>
                            <Button 
                            disabled={isInvalid}
                            variant="warning" 
                            type="submit"
                            className={isInvalid ? "button-contact-us" : "button-contact-us-active" }>
                                Envoyer
                            </Button>
                            {error !== null ? <p >{error.message}</p> : undefined}
                        </Form>
                    </Col>
                </Row>
                </Container>
            </Jumbotron>
            <Alert className={this.isSuccess ? "alert-message-success" : "alert-message" } variant="success">
                        Votre message a bien été envoyé merci beaucoup :) 
            </Alert>
            </>
        );
    }
}

const ContactFormFirebase = withFirebase(ContactForm)


const ArchitectureContactForm = () => {
    return (
        <>
        <Container fluid
        className="architecture-contact-us">
                <Container className="architecture-contact-us-col">
                    <h2>
                    Contact
                    </h2>   
                    <TitleContactForm/>
                    <Container fluid className="container-fluid-contact-form">
                        <ContactFormFirebase/>
                    </Container>
                </Container> 
        </Container>
        </>
    );
}

export class ContactFormHumboldt extends Component {
    render() {
        return(
            <div className="div-bg-particle">
                <ParticlesBg className="thick-stuff" type="thick" bg={true}/> 
                <ArchitectureContactForm/>
            </div>
        );
    }
}