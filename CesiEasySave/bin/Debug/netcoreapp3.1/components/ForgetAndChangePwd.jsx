import React, {Component} from 'react';
import {withFirebase} from '../components/Firebase';
import * as ROUTE from '../constant/route';
import {
    Container
} from 'react-bootstrap';

export const PasswordForgetHumboldt = () => {
    return(
        <>
        <h2>
            Mot de passe oublié 
        </h2>
        <PasswordForgetComponentHumboldt/>
        </>
    );
}

const INIT_STATE = {
    email : '',
    error : null
}

export class PasswordForgetFormComponent extends Component {
    constructor(props) {
        super(props)
        this.state = {...INIT_STATE} 
    }
    
    onSubmit = event => {
        const { email } = this.state;

        this.props.firebase
        .doPasswordReset(email)
        .then(() => {
            this.setState({ ...INIT_STATE });
        })
        .catch(error => {
            this.setState({ error });
        });
    
        event.preventDefault();
    }

    onChange = event => {
        this.setState({
            [event.target.name] : [event.target.value]
        })
    }

    render() {
        const {
            email,
            error
        } = this.state
        const isInvalid = email === '';
        return(
            <form onSubmit={this.onSubmit}>
                <input
                name="email"
                value={this.state.email}
                onChange={this.onChange}
                type="text"
                placeholder="Email Address"
                />
                <button disabled={isInvalid} type="submit">
                Reset My Password
                </button>
                {error && <p>{error.message}</p>}
            </form>
        );
    }
}

export const PasswordForgetLink = () => {
    return(
        <Container fluid className="forgot-link">
            <p>
                <a href={ROUTE.PASSWORD_FORGET}>Forgot Password?</a>
            </p>
        </Container>
    );
}

const PasswordForgetComponentHumboldt = withFirebase(PasswordForgetFormComponent);

const INIT_STATE_CHANGE_PASSWORD = {
    passwordOne : '',
    passwordTwo : '',
    error : null
};

class ChangePasswordFormComponent extends Component {
    constructor(props) {
        super(props)
        this.state = {...INIT_STATE_CHANGE_PASSWORD} 
    }

    onSubmit = event => {
        const { passwordOne } = this.state;
    
        this.props.firebase
        .doPasswordUpdate(passwordOne)
        .then(() => {
            this.setState({ ...INIT_STATE_CHANGE_PASSWORD });
        })
        .catch(error => {
            this.setState({ error });
        });
    
        event.preventDefault();
    }

    onChange = event => {
        this.setState({
            [event.target.name] : [event.target.value]
        })
    }

    render() {
        const {passwordOne, passwordTwo, error} = this.state;
        const isInvalid = passwordOne !== passwordTwo || passwordOne === '';
        return(
            <form onSubmit={this.onSubmit}>
                <input
                name="passwordOne"
                value={passwordOne}
                onChange={this.onChange}
                type="password"
                placeholder="New Password"
                />
                <input
                name="passwordTwo"
                value={passwordTwo}
                onChange={this.onChange}
                type="password"
                placeholder="Confirm New Password"
                />
                <button disabled={isInvalid} type="submit">
                Changer
                </button>
        
                {error && <p>{error.message}</p>}
            </form>
        );
    }
}

export const ChangePasswordFormComponentHumboldt = withFirebase(ChangePasswordFormComponent)

