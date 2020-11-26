import React, {Component} from 'react';
import {
    Navbar,
    Nav,
    Container,
    Col
} from 'react-bootstrap';
import { useSpring, animated } from 'react-spring';
import { AuthUserContext } from './Session';
import {Flipper} from 'react-flip-toolkit';
import SeConnecterDropdown from './DropDownContent/SeConnecterDropDown';
import BlogDropdown from './DropDownContent/BlogDropDown';
import DropdownContainer from './DropDownContainer';
import NavbarItem from './Nav/NavBarItem';
import {withRouter, useLocation} from 'react-router-dom';
import {NavBarArticleHumboldt} from '../components/NavBarArticle';
import * as ROUTE from '../constant/route';



const navbarConfigBlog = [
    { title: "BLOG", dropdown: BlogDropdown }
];

class BlogSectionNavBar extends Component {
    state = {
        activeIndices: []
    };

    onMouseEnter = i => {
        if (this.state.activeIndices[this.state.activeIndices.length - 1] === i)
        return
        this.setState(prevState => ({
        activeIndices: prevState.activeIndices.concat(i)
        }));
    };

    onMouseLeave = () => {
        this.setState({
        activeIndices: []
        });
    };

    render() {
        let CurrentDropdown;

        const currentIndex = this.state.activeIndices[
        this.state.activeIndices.length - 1
        ];

        if (typeof currentIndex === "number")
        CurrentDropdown = navbarConfig[currentIndex].dropdown;

        return (
        <nav onMouseLeave={this.onMouseLeave}>
            {navbarConfigBlog.map((n, index) => {
            return (
                <NavbarItem
                title={n.title}
                index={index}
                onMouseEnter={this.onMouseEnter}
                >
                {currentIndex === index && (
                    <DropdownContainer>
                    <CurrentDropdown />
                    </DropdownContainer>
                )}
                </NavbarItem>
            );
            })}
        </nav>
        );
    }
}

const navbarConfig = [
    { title: "BLOG", dropdown: BlogDropdown },
    { title: "MON COMPTE", dropdown: SeConnecterDropdown}
]

class MonCompteSectionNavBar extends Component {
    constructor(props) {
        super(props);
        this.state = {
            activeIndices: [],
        }
    }

    resetDropdownState = i => {
        this.setState({
        activeIndices: typeof i === "number" ? [i] : [],
        animatingOut: false
        })
        delete this.animatingOutTimeout
    }

    onMouseEnter = i => {
        if (this.animatingOutTimeout) {
        clearTimeout(this.animatingOutTimeout)
        this.resetDropdownState(i)
        return
        }
        if (this.state.activeIndices[this.state.activeIndices.length - 1] === i)
        return

        this.setState(prevState => ({
        activeIndices: prevState.activeIndices.concat(i),
        animatingOut: false
        }))
    }

    onMouseLeave = () => {
        this.setState({
        animatingOut: true
        })
        this.animatingOutTimeout = setTimeout(
        this.resetDropdownState,
        this.props.duration
        )
    }

    render() {
        const { duration } = this.props
        let CurrentDropdown
        let PrevDropdown
        let direction

        const currentIndex = this.state.activeIndices[
        this.state.activeIndices.length - 1
        ]
        const prevIndex =
        this.state.activeIndices.length > 1 &&
        this.state.activeIndices[this.state.activeIndices.length - 2]

        if (typeof currentIndex === "number")
        CurrentDropdown = navbarConfig[currentIndex].dropdown
        if (typeof prevIndex === "number") {
        PrevDropdown = navbarConfig[prevIndex].dropdown
        direction = currentIndex > prevIndex ? "right" : "left"
        }

        return (
        <Flipper
            flipKey={currentIndex}
            spring={duration === 300 ? "noWobble" : { stiffness: 10, damping: 10 }}
        >
            <Nav onMouseLeave={this.onMouseLeave}>
            {navbarConfig.map((n, index) => {
                return (
                <NavbarItem
                    key={n.title}
                    title={n.title}
                    index={index}
                    onMouseEnter={this.onMouseEnter}
                >
                    {currentIndex === index && (
                    <DropdownContainer
                        direction={direction}
                        animatingOut={this.state.animatingOut}
                        duration={duration}
                    >
                        <CurrentDropdown />
                        {PrevDropdown && <PrevDropdown />}
                    </DropdownContainer>
                    )}
                </NavbarItem>
                )
            })}
            </Nav>
        </Flipper>
        )
    }
}

const NavBarHumboldt = () => {
    return (
        <>
            <AuthUserContext.Consumer>
                {(authUser) =>
                    authUser ? <AuthNavBarHumboldt/> : <NonAuthNavBarHumboldt />
                }
            </AuthUserContext.Consumer>
        </>
    );
}

export class AuthNavBarHumboldt extends React.Component {
    render () {
        return (
            <div>
                <Container fluid className="navbar-home-container-fluid">
                    <Container className="navbar-home-container">
                        <Navbar
                            collapseOnSelect
                            expand="lg"
                            className="navbar-home">
                            <Col className="navbar-home-col">
                                <Navbar.Brand
                                    href="/"
                                    className="navbar-home-brandname">
                                    Humboldt-Life
                        </Navbar.Brand>
                            </Col>
                            <Col>
                                <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                                <Navbar.Collapse id="responsive-navbar-nav">
                                    <Nav className="mr-auto">
                                    </Nav>                              
                                        <Nav.Link className="nav-bar-menu-link" href="/contact">Contact</Nav.Link>
                                        <Nav.Link className="nav-bar-menu-link" href="/profil">Nos Fiches</Nav.Link>
                                        <Nav.Link className="nav-bar-menu-link" href="/aboutus">A Propos</Nav.Link>
                                        <MonCompteSectionNavBar/>
                                </Navbar.Collapse>
                            </Col>
                        </Navbar>
                    </Container>
                </Container>
            </div>
        );
    }
}



const NonAuthNavBarHumboldt = () => {
    const fade = useSpring({
        from: {
            opacity: 0
        },
        to: {
            opacity: 1
        }
    })
    return (
        <animated.div style={fade}>
            <Container fluid className="navbar-home-container-fluid">
                <Container className="navbar-home-container">
                    <Navbar
                        collapseOnSelect
                        expand="lg"
                        className="navbar-home">
                        <Col className="navbar-home-col">
                            <Navbar.Brand
                                href="/"
                                className="navbar-home-brandname">
                                Humboldt-Life
                    </Navbar.Brand>
                        </Col>
                        <Col>
                            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                            <Navbar.Collapse id="responsive-navbar-nav">
                                <Nav className="mr-auto">
                                </Nav>
                                <Nav>
                                    <Nav.Link className="nav-bar-menu-link" href="/contact">Contact</Nav.Link>
                                    <Nav.Link className="nav-bar-menu-link" href="/profil">Nos Fiches</Nav.Link>
                                    <Nav.Link className="nav-bar-menu-link" href="/aboutus">A Propos</Nav.Link>
                                    <BlogSectionNavBar/>
                                    <Nav.Link className="nav-bar-menu-link" href="/login">Se connecter</Nav.Link>
                                </Nav>
                            </Navbar.Collapse>
                        </Col>
                    </Navbar>
                </Container>
            </Container>
        </animated.div>
    );
}

class NavBarChoice extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            navBar : <NavBarHumboldt/>
        }
    }

    changeNavBar = (newNavBar) => {
        this.setState({
            navBar : newNavBar
        })
    }

    //if path is blog display navBar for blog else, display the basic navBar
    componentWillMount() {
        console.log(window.location.pathname)
        if(window.location.pathname === ROUTE.HOME_ARTICLE) {
            this.changeNavBar(<NavBarArticleHumboldt/>)
        }
    }

    render() {
        return(
            <>
            {
                this.state.navBar
            }
            </>
        );
    }
}

export const NavBarChoiceHumboldt = withRouter(NavBarChoice)
