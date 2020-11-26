import React from 'react';
import {withFirebase} from '../Firebase';
import {
    Container,
    Row,
    Col,
    Card,
    Button,
    Spinner
} from 'react-bootstrap';
import { FooterHumboldt } from '../Footer';


class CategoriesHome extends React.Component {
    constructor(props) {
        super(props);
        this.CategoriesList = [];
        this.ParsedList = [];
        this.state = {
            categoriesParsed : []
        }

    }

    componentWillMount() {
        this.props.firebase.db.ref("Categories/").once("value").then(
            (snapshot) => {
                if(snapshot) {
                    snapshot.forEach(category => {
                        if(category) {
                            this.CategoriesList.push(category.val());
                        }
                    })
                }
                var trigger = true;
                while(trigger) {
                    let tmpListTags = [];
                    for(var j = 0; j < this.CategoriesList.length; ++j ) {
                        tmpListTags = this.CategoriesList.splice(0,3);
                        this.ParsedList.push(tmpListTags);
                        tmpListTags = [];
                        if(this.CategoriesList.length === 0) {
                            trigger = false;
                            break;
                        }
                    }
                }
                this.setState({
                    categoriesParsed : this.ParsedList
                })
            }
        )
    }

    render() {
        if(this.state.categoriesParsed.length !== 0) {
            return(
                <>
                <Container fluid className="tag-menu-container-fluid">
                    <h2>
                        Les Catégories
                    </h2>
                    {
                        this.state.categoriesParsed.map(categories => {
                            return(
                                <Container className="tag-menu-container">
                                    <Row>
                                    {categories.map(category => {
                                        return(
                                            <Col className="tag-menu-col">
                                                <Card  className="tag-menu-card">
                                                <Card.Body>
                                                    <Card.Title className="tag-menu-card-title">{category.libelle}</Card.Title>
                                                    <Card.Text className="tag-menu-card-content">
                                                    {category.content}
                                                    </Card.Text>
                                                    <a href={category.url_link}>
                                                        <Button className="tag-menu-card-button" variant="warning">Vas-y!</Button>
                                                    </a>
                                                </Card.Body>
                                                <Card.Header style={{backgroundColor : category.color_ref}}as="h5"></Card.Header>
                                                </Card>
                                            </Col>
                                        );
                                    })}
                                    </Row>
                                </Container>
                            );
                        })
                    }
                </Container>
                <FooterHumboldt/>
                </>
            )
        } else {
            return(
                <>
                <Container fluid className="tag-menu-spinner-container-fluid">
                    <h2>
                        Les Catégories
                    </h2>
                    <Spinner className="spinner-tag-menu" animation="border" variant="warning" role="status">
                        <span className="sr-only">Loading...</span>
                    </Spinner>
                </Container>
                </>
            )
        }
    }
}


const CategoriesHomeHumboldt = withFirebase(CategoriesHome);

export default CategoriesHomeHumboldt;