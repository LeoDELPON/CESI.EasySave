import React from 'react';
import {
    Container, 
    Jumbotron,
    Button
} from 'react-bootstrap';
import {tags} from '../dataMock/tagsMock';
import * as ROUTE from '../constant/route';
import {withFirebase} from '../components/Firebase';

var INIT_STATE_TAGS_HOME_ARTICLE = {
    color_ref : "",
    libelle : ""      
}

class TagLeftSideNav extends React.Component {
    constructor(props) {
        super(props);
        this.tagContent = {...INIT_STATE_TAGS_HOME_ARTICLE}
        this.Tags = [];
        this.State = {
            parsed : []
        }
    }

    componentWillMount() {
        this.props.firebase.db.ref("Tags/").once("value", (snapshot) => {
        }).then(
            (tags) => {
                tags.forEach(data => {
                    if(data) {
                        this.Tags.push(data.val())
                    }
                })
                this.setState({
                    parsed : this.Tags
                })
            }
        )
    }

    render() {
        return (
            <Container className="article-side-nav-tags-container">
                <Jumbotron className="article-side-nav-tags-jumbotron">
                    {
                        tags.map(tag =>
                            <a 
                            href="/tag1"
                            className="article-side-nav-tags-position">
                            <Button
                            variant={tag.color_ref}
                            className="article-side-nav-tags-content">{tag.libelle}</Button>
                            </a>
                            )
                    }
                </Jumbotron>
            </Container>
        );
    }
}

const TagLeftSideNavHumboldt = withFirebase(TagLeftSideNav)

const NetworksLinksLeftSideNavHumboldt = () => {
    const icons = [
        "facebook",
        "github",
        "instagram",
        "linkedin",
        "twitter"
    ]
    return (
        <Container className="article-side-nav-icons-container">
            {
                icons.map(icon => {
                    return(
                        <a 
                        href="/"
                        className="article-side-nav-icons-position">
                            <img 
                            src={process.env.PUBLIC_URL + "/img/Icones/blackIcons/" + icon + ".png"}
                            className="article-side-nav-icons-image"
                            alt={icon + "logo"}/>
                        </a>
                    );
                })
            }
        </Container>
    );
}



export const ArticleHomeLeftHumboldt = () => {
    const sideNavBar = [
        ["https://img.icons8.com/fluent/96/000000/login-rounded-right.png","Inscription-Connexion", ROUTE.LOGIN],
        ["https://img.icons8.com/officel/80/000000/price-tag.png" , "Tags", ROUTE.TAGS_MENU],
        ["https://img.icons8.com/fluent/96/000000/faq.png", "FAQ", ROUTE.FAQ],
        ["https://img.icons8.com/cute-clipart/64/000000/shop.png", "Shop Humboldt", ROUTE.HOME],
        ["https://img.icons8.com/fluent/96/000000/business-contact.png", "Contact", ROUTE.CONTACT],
        ["https://img.icons8.com/officel/80/000000/set-as-resume.png", "Profil", ROUTE.PROFIL]
    ]
    return (
        <>
        <Container className="article-side-nav-container">
            <Jumbotron className="article-side-nav-jumbotron">
            <div className="article-side-nav-li-container">
                {
                sideNavBar.map( (element, index) => {
                    return (
                        <li className="article-side-nav-li">
                            <img 
                            src={element[0]} 
                            alt={element[1]}
                            className="article-side-li-img"/>
                            <span className="article-side-nav-text">
                                <a href={element[2]}>{element[1]}</a>
                            </span>
                        </li>
                    );
                })
                }
            </div>
            </Jumbotron>
        </Container>
        <NetworksLinksLeftSideNavHumboldt/>
        <TagLeftSideNavHumboldt/>
        </>
    );
}