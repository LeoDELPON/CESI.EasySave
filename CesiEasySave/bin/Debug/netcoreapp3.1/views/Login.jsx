import React, { Component } from 'react';
import { SignUpLink } from './SignUp';
import { withRouter } from 'react-router-dom';
import { compose } from 'recompose';
import * as ROUTES from '../../constant/route';
import { withFirebase } from '../Firebase';
import { PasswordForgetLink } from '../ForgetAndChangePwd';
import { FooterHumboldt } from '../Footer';
import { Container, Row, Button } from 'react-bootstrap';

const LoginHumboldt = () => {
    return (
        <>
            <Container fluid className="login-bg">
                <SignInComponentHumbodlt />
                <FooterHumboldt />
            </Container>
        </>
    );
};

const INITIAL_LOG_IN_STATE = {
    email: '',
    password: '',
    error: null,
};

class LogInFormComponent extends Component {
    constructor(props) {
        super(props);
        this.state = { ...INITIAL_LOG_IN_STATE };
    }

    onSubmit = (event) => {
        const { email, password } = this.state;

        this.props.firebase
            .doSignInWithEmailAndPassword(email, password)
            .then(() => {
                this.setState({ ...INITIAL_LOG_IN_STATE });
                this.props.history.push(ROUTES.HOME);
            })
            .catch((error) => {
                this.setState({ error });
            });

        event.preventDefault();
    };

    onChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    };

    render() {
        const { email, password, error } = this.state;
        const isInvalid = email === '' || password === '';
        return (
            <>
                <Container fluid className="sign-in-section-container-fluid">
                    <Container className="sign-in-section-box">
                        <h2>Tu nous avais manqué</h2>
                        <Row>
                            <form
                                onSubmit={this.onSubmit}
                                className="sign-in-section-form"
                            >
                                <input
                                    name="email"
                                    value={email}
                                    onChange={this.onChange}
                                    type="text"
                                    placeholder="Adresse mail"
                                />
                                <input
                                    name="password"
                                    value={password}
                                    onChange={this.onChange}
                                    type="password"
                                    placeholder="Mot de passe"
                                />
                                <Button
                                    variant="warning"
                                    className={isInvalid ? undefined : 'active'}
                                    disabled={isInvalid}
                                    type="submit"
                                >
                                    Sign In
                                </Button>
                                {error && (
                                    <span className="error-message">
                                        {' '}
                                        Désolé mais l'email ou le mot de passe
                                        est incorrect
                                    </span>
                                )}
                            </form>
                            <PasswordForgetLink />
                            <SignUpLink />
                        </Row>
                    </Container>
                </Container>
            </>
        );
    }
}

const SignInComponentHumbodlt = compose(
    withRouter,
    withFirebase
)(LogInFormComponent);

export default LoginHumboldt;
