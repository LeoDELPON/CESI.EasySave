import React from 'react';
import { withFirebase } from './Firebase';
import {
        Button
} from 'react-bootstrap';

const SignOutComponent = ({firebase}) => {
        return(
        <Button className="sign-out-button" variant="warning" type="button" onClick={firebase.doSignOut}>
        Sign Out
        </Button>
        );
}

export default withFirebase(SignOutComponent);