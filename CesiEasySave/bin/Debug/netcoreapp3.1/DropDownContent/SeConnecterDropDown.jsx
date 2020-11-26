import React from 'react';
import {
    ProductsDropdownEl,
    ProductsSection,
    Logo,
    SubProductsList,
    WorksWithStripe,
} from './Components';
import SignOutHumboldt from '../SignOutComponent';
import {withFirebase} from '../Firebase'

class SeConnecterDropdown extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            username : "",
            rang : "",
            statut : "",
            url : ""
        }
    }

    componentDidMount() {
        const uid = this.props.firebase.getUid()
        this.props.firebase.db.ref('users/' + uid + '/username/').once("value", snapshot => {
            this.setState({
                username : snapshot.val()
            })
        });
        this.props.firebase.db.ref('users/' + uid + '/rang/').once("value", snapshot => {
            this.setState({
                rang: snapshot.val()
            })
        })
        this.props.firebase.db.ref('users/' + uid + '/pictureUrl/').once("value", snapshot => {
            this.setState({
                url: snapshot.val()
            })
        })
        this.props.firebase.db.ref('users/' + uid + '/statut/').once("value", snapshot => {
            if(snapshot.val() === 0) {
                this.setState({
                    statut: "Membre novice"
                })
            } else if(snapshot.val() === 1) {
                this.setState({
                    statut: "Maître du site"
                })
            }
        })

    }
    render() {
        return (
            <ProductsDropdownEl>
            <div className="blog-dropdown-section">
                <ProductsSection>
                <li>
                    <div>
                    <Logo color="blue" />
                    </div>
                    <div>
                    <a href="/fiche">
                        <h3 className="blog-dropdown-title" color="blue">MA FICHE</h3>
                    </a>
                    <p className="blog-dropdown-subtitle">Accéder à ma fiche caractéristique</p>
                    </div>
                </li>
                <li>
                    <div>
                    <Logo color="green" />
                    </div>
                    <div>
                    <a href="/article-maker">
                        <h3 className="blog-dropdown-title" color="green">ECRIRE</h3>
                    </a>
                    <p className="blog-dropdown-subtitle">Au cas où de l'inspiration serait présente</p>
                    </div>
                </li>
                <li>
                    <div className={this.state.url ? "redimensionn-img-container" : undefined}>
                    {
                        this.state.url ? <img className="redimensionn-img" src={this.state.url} /> : <Logo color="red" />
                    }
                    </div>
                    <div>
                    <a href="/account">
                        <h3 className="blog-dropdown-title" color="teal">GESTION DE COMPTE</h3>
                    </a>
                    <p id="pseudo" className="blog-dropdown-subtitle" style={{ marginBottom: 0 }}>
                        Heureux de vous revoir parmis nous ! 
                    </p>
                    </div>
                </li>
                </ProductsSection>
            </div>
            <div className="blog-dropdown-section">
                <SubProductsList>
                <li>
                    <h3 className="blog-dropdown-title-section">STATUT</h3>
                    <div className="blog-dropdown-title-content-section">
                        {this.state.statut}
                    </div>
                </li>
                <li>
                    <h3 className="blog-dropdown-title-section">RANG</h3>
                    <div className="blog-dropdown-title-content-section">
                        {this.state.rang}
                    </div>
                </li>
                <li>
                    <h3 className="blog-dropdown-title-section">NOM</h3>
                    <div className="blog-dropdown-title-content-section">
                        {this.state.username}
                    </div>
                </li>
                </SubProductsList>
                <WorksWithStripe>
                <h3 className="se-connecter-dropdown-title">
                <SignOutHumboldt />
                </h3>
                </WorksWithStripe>
            </div>
            </ProductsDropdownEl>
        );
    }
};

export default withFirebase(SeConnecterDropdown)