import React from 'react';
import * as ROUTES from '../constant/route';
import { withFirebase } from './Firebase';


const INIT_STATE_CHANGE_PSEUDO = {
    pseudo : "",
    error : null
}

class PseudoChangeAccount extends React.Component {
    constructor(props) {
        super(props)
        this.state = {...INIT_STATE_CHANGE_PSEUDO}
    }

    onSubmit = event => {
        const { pseudo } = this.state;
        this.props.firebase.doPseudoUpdateDb(pseudo)
        .then(() => {
            this.setState({ ...INIT_STATE_CHANGE_PSEUDO });
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
        const {pseudo, error} = this.state;
        const isInvalid = pseudo === ""
        return(
            <>
                <form 
                onSubmit={this.onSubmit}>
                <input
                name="pseudo"
                value={pseudo}
                onChange={this.onChange}
                type="text"
                placeholder="Ton pseudo"
                />
                <button disabled={isInvalid} type="submit">
                Reset my Pseudo
                </button>
                {error && <p>{error.message}</p>}
                </form>
            </>
        );
    }
}

export const PseudoChangeAccountHumboldt = withFirebase(PseudoChangeAccount)

const INIT_STATE_CHANGE_EMAIL = {
    email : "",
    password : "",
    error : null
}

class EmailAccount extends React.Component {
    constructor(props) {
        super(props)
        this.state = {...INIT_STATE_CHANGE_EMAIL}
    }

    onSubmit = event => {
        const { email, password } = this.state;
        
        this.props.firebase
        .doEmailUpdate(email, password)
        .then(() => {
            this.setState({ ...INIT_STATE_CHANGE_EMAIL });
            return this.props.firebase
            .doEmailUpdateDb(email)
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
        this.props.firebase.db.ref('users/' + this.props.firebase.getUid() + '/email/').once("value", snapshot => {
            console.log(snapshot.val())
        })
    }

    render() {
        const {email, password, error} = this.state;
        const isInvalid = email === "";
        return(
            <>  
                <form
                onSubmit={this.onSubmit}>
                <input
                name="password"
                value={password}
                onChange={this.onChange}
                type="password"
                placeholder="Tape ton mot de passe !"
                />
                <input
                name="email"
                value={email}
                onChange={this.onChange}
                type="text"
                placeholder="Ton Email"
                />
                <button disabled={isInvalid} type="submit">
                Changer
                </button>
                {error && <p>{error.message}</p>}
                </form>   
            </>
        );
    }
}

export const EmailAccountHumboldt = withFirebase(EmailAccount);

class PictureAccount extends React.Component {
    constructor(props){
        super(props)
        this.state = {
            file : ""
        }
    }

    onChange = event => {
        if(event.target.files[0]) {
            this.setState({
                file: event.target.files[0]
            })
        }
    }

    onClick = () => {
        const uploadTask = this.props.firebase.storage.ref('images/'+ this.props.firebase.getUid() + '/'+ this.state.file.name).put(this.state.file);
        uploadTask.on(
            "state_changed",
            snapshot => {},
            error => {
                console.log(error);
            },
            () => {
                this.props.firebase.storage
                .ref("images/" + this.props.firebase.getUid())
                .child(this.state.file.name)
                .getDownloadURL()
                .then(url => {
                    this.props.firebase.doPictureUrlDb(url)
                    this.setState({
                        file : ""
                    })
                    console.log("fait Luffy")
                })
            }
        )
    };

    render() {
        return(
            <>
                <input type="file" onChange={this.onChange}/>
                <button onClick={this.onClick}> Upload </button>
            </>
        )
    }
}

export const PictureAccountHumboldt = withFirebase(PictureAccount)


class DeleteAccount extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            user : this.props.firebase.auth.currentUser,
            uid : this.props.firebase.getUid(),
            password : '',
            error : null
        }
    }

    onClick = event => {
        this.props.firebase.DoReAuthenticate(this.state.user.email, String(this.state.password));
        this.state.user.delete()
        .then(uid => {
            this.props.firebase.doAccountDeleteDb(this.state.uid);
            this.props.history.push(ROUTES.HOME);
        })
        .catch(function(error) {
            console.log(error)
        })
    }

    onChange = event => {
        this.setState({
            [event.target.name] : [event.target.value]
        })
    }

    render() {
        const {password, error} = this.state;
        return(
            <>
            <input
            name="password"
            value={password}
            onChange={this.onChange}
            type="password"
            placeholder="Ton mot de passe"
            />
            <button onClick={this.onClick}>
                Supprimer mon compte
            </button>
            {error && <p>{error.message}</p>}
            </>
        );
    }
}

export const DeleteAccountHumboldt = withFirebase(DeleteAccount);

export const MyProfilCVAccount = () => {
    // const statut = [
    //     "Voir mes caractéristiques",
    //     "Créer ma fiche caractéristique",
    //     "Malheureusement, tu n'en as pas le pouvoir"
    // ]
    return(
        <>

        </>
    );
}

export const DashBoardAccount = () => {
    return(
        <>
        </>
    );
}



export const FeatureUnlock = () => {
    return(
        <>
        </>
    );
}