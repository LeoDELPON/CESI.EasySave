import React, {Component} from 'react';
import { withFirebase } from '../Firebase';
import * as ROUTES from '../../constant/route';
import {withRouter } from 'react-router-dom';
import {compose} from 'recompose';
import {
    Container,
    Button,
    Row
} from 'react-bootstrap';
import {FooterHumboldt} from '../Footer';

const SignUpHumboldt = () => {
    return (
        <>
        <Container fluid className="signup-bg">
            <SignUpComponentHumbodlt/>
            <FooterHumboldt/>
        </Container>
        </>
    );
}

const INITIAL_STATE = {
    username : '',
    email : '',
    pictureUrl : '',
    school : '',
    passwordOne : '',
    passwordTwo : '',
    rang : "marin d'eau douce",
    statut : 0,
    error : null
};

class SignUpFormComponent extends Component {
    constructor(props) {
        super(props)
        this.state = {...INITIAL_STATE}
    }

    onSubmit = event => {
        const { username, email, passwordOne, pictureUrl, school, rang, statut } = this.state;

        this.props.firebase
        .doCreateUserWithEmailAndPassword(email, passwordOne)
        .then(authUser => {
            // Create a user in your Firebase realtime database
            return this.props.firebase
            .user(authUser.user.uid)
            .set({
                username,
                email,
                pictureUrl,
                school,
                rang, 
                statut
            });
        })
        .then(authUser => {
            this.setState({ ...INITIAL_STATE });
            this.props.history.push(ROUTES.HOME);
        })
        .catch(error => {
            this.setState({ error });
        });
    
        event.preventDefault();
    };

    onChange = event => {
        this.setState(
            {
                [event.target.name]: event.target.value
            }
        )
    };

    render() {
        const {
            username,
            email,
            passwordOne,
            passwordTwo,
            error,
        } = this.state;

        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        
        const isInvalidEmail = !regex.test(email)
        const isInvalid = passwordOne !== passwordTwo || email === '' || username === '' || isInvalidEmail
        return(
            <Container fluid className="sign-up-section-container-fluid">
                <Container className="sign-up-section-box">
                    <h2>
                        Rejoins nous jeune pirate !
                    </h2>
                    <Row>
                        <form 
                        onSubmit={this.onSubmit}
                        className="sign-up-section-form">
                            <input
                            name="username"
                            value={username}
                            onChange={this.onChange}
                            type="text"
                            placeholder="Pseudo"
                            />
                            <input
                            name="email"
                            value={email}
                            onChange={this.onChange}
                            type="text"
                            placeholder="Adresse Mail"
                            />
                            <input
                            name="passwordOne"
                            value={passwordOne}
                            onChange={this.onChange}
                            type="password"
                            placeholder="Mot de passe"
                            />
                            <input
                            name="passwordTwo"
                            value={passwordTwo}
                            onChange={this.onChange}
                            type="password"
                            placeholder="Mot de passe confirmé"
                            />
                            <Button 
                            variant="warning"
                            disabled={isInvalid} 
                            className={isInvalid ? undefined : "active"}
                            type="submit">
                                Sign Up
                            </Button>
                    
                            {error !== null ? <p >{error.message}</p> : undefined}
                        </form>
                    </Row>
                </Container>
            </Container>
        );
    }
}

const SignUpComponentHumbodlt = compose(withRouter(withFirebase(SignUpFormComponent)));

export const SignUpLink = () => (
    <>
    <Container fluid className="sign-up-link">
        <p>
        Don't have an account? <a href={ROUTES.SIGN_UP}>Sign Up</a>
        </p>
    </Container>
    </>
  );

export default SignUpHumboldt;