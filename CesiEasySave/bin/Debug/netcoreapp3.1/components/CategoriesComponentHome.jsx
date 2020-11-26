import React from 'react';
import {
    Container, 
    Row,
    Col,
    Card,
    Spinner 
} from 'react-bootstrap';
import {withFirebase} from './Firebase';


class CategoriesHome extends React.Component {
    constructor(props) {
        super(props)
        this.categoriesList = []
        this.Categories = []
        this.state = {
            categoriesList : []
        }
    }

    componentWillMount() {
        this.props.firebase.db.ref("Categories/").once("value", snap => {
        }).then(
            (snapshot) => {
                snapshot.forEach( category => {
                    if(category) {
                        this.Categories.push(category.val())
                    }
                })
                var test = true
                while(test) {
                    var tmpList = []
                    for(var i = 0; i <= this.Categories.length; ++i) {
                        tmpList = this.Categories.splice(0, 3)
                        this.categoriesList.push(tmpList)
                        tmpList = []
                        if(this.Categories.length === 0) {
                            test = false;
                            break;
                        }
                    }
                }
                this.setState({
                    categoriesList : this.categoriesList
                })
                console.log(this.state.categoriesList)
            }
        )
    }

    render() {
        if(this.state.categoriesList.length === 0) {
            return(
                <Container fluid className="category-home-container-fluid">
                <h2>
                    LES CATEGORIES
                </h2>
                <Container fluid className="category-home-container-fluid-spinner">
                    <Spinner animation="border" variant="warning" role="status">
                                <span className="sr-only">Loading...</span>
                    </Spinner>
                </Container>
                </Container>
            );
        } else {
            return(
                <>
                <Container fluid className="category-home-container-fluid">
                    <h2>
                        LES CATEGORIES
                    </h2>
                    {
                        this.state.categoriesList.map(categoryCompo => {
                            return(
                                <Row className="category-home-row">
                                    {
                                        categoryCompo.map( c => {
                                            return(
                                                <Col className="category-home-card">
                                                    <a href={c.url_link}>
                                                        <Card style={{backgroundColor: c.color_ref}}>
                                                        <Card.Body>
                                                            <Card.Title className="category-home-title-card">{c.libelle}</Card.Title>
                                                            <Card.Body className="category-home-content-card">
                                                                {c.content}
                                                            </Card.Body>
                                                        </Card.Body>
                                                        </Card>
                                                    </a>
                                                </Col>
                                            );
                                        })
                                    }
                                </Row>
                            );
                        })
                    }
                </Container>
                </>
            );
        }
    }
}

export const CategoriesHomeHumboldt = withFirebase(CategoriesHome);