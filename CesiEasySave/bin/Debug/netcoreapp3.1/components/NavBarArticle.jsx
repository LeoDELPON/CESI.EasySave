import React from 'react';
import {
    Navbar,
    Nav,
    Form,
    FormControl,
    Button,
    Col,
    Row
} from 'react-bootstrap';

export const NavBarArticleHumboldt = () => {
    return (
        <Navbar className="navbar-article-container" bg="dark" variant="dark">
            <Row>
                <Col>
                    <Navbar.Brand className="navbar-brand" href="/">Humboldt-Life</Navbar.Brand>
                </Col>
                <Col className="navbar-article-col">
                    <Nav className="mr-auto navbar-article-content">
                        <Nav.Link href="/contact">CONTACT</Nav.Link>
                        <Nav.Link href="/aboutus">A PROPOS</Nav.Link>
                        <Nav.Link href="/tags-menu">TAGS</Nav.Link>
                        <Nav.Link href="/categories">CATEGORIES</Nav.Link>
                    </Nav>
                </Col>
                <Col className="navbar-article-search-col">
                    <Form inline>
                        <FormControl 
                        type="text" 
                        placeholder="Search" 
                        className="mr-sm-2 navbar-article-searchbar" />
                        <Button variant="outline-warning">Search</Button>
                    </Form>
                </Col>
            </Row>
        </Navbar>
    );
}