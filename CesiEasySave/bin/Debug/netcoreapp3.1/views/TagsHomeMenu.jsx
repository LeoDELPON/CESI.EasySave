import React, {Component} from 'react';
import {tags} from '../../dataMock/tagsMock';
import {
    Container,
    Row,
    Col,
    Card,
    Button,
    Spinner
} from 'react-bootstrap';
import {withFirebase} from '../Firebase';
import {FooterHumboldt} from '../Footer';

    var INIT_STATE_TAGS = {
        color_ref : "",
        libelle : ""      
    }

class TagMenu extends Component {
    constructor(props) {
        super(props);
        this.tagContent = {...INIT_STATE_TAGS}
        this.Tags = [];
        this.ParsedListTags = [];
        this.State = {
            parsed : []
        }
    }

    componentWillMount() {
        // A RETENIR TA MERE LET A UNE PORTEE DE BLOC ET VAR A UNE PORTEE DE FONCTION
        this.props.firebase.db.ref("Tags/").once("value", (snapshot) => {
        }).then(
            (tags) => {
                tags.forEach(data => {
                    if(data) {
                        this.Tags.push(data.val())
                    }
                })
                var test = true;
                while(test) {
                    let tmpListTags = [];
                    for(var j = 0; j < this.Tags.length; ++j ) {
                        tmpListTags = this.Tags.splice(0,3);
                        this.ParsedListTags.push(tmpListTags);
                        tmpListTags = [];
                        if(this.Tags.length === 0) {
                            test = false;
                            break;
                        }
                    }
                }
                this.setState({
                    parsed : this.ParsedListTags
                })
            }
        )
    }


    render() {
        if(this.state) {
            return(
                <>
                <Container fluid className="tag-menu-container-fluid">
                    <h2 className="">
                        Les Tags
                    </h2>
                    {
                        this.state.parsed.map(tags => {
                            return(
                                <Container className="tag-menu-container">
                                    <Row>
                                    {tags.map(tagComponent => {
                                        return(
                                            <Col className="tag-menu-col">
                                                <Card  className="tag-menu-card">
                                                <Card.Body>
                                                    <Card.Title className="tag-menu-card-title">{tagComponent.libelle}</Card.Title>
                                                    <Card.Text className="tag-menu-card-content">
                                                    {tagComponent.content}
                                                    </Card.Text>
                                                    <a href="/">
                                                        <Button className="tag-menu-card-button" variant="warning">Vas-y!</Button>
                                                    </a>
                                                </Card.Body>
                                                <Card.Header style={{backgroundColor : tagComponent.color_ref}}as="h5"></Card.Header>
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
            );
        } else {
            return(
                <>
                <Container fluid className="tag-menu-spinner-container-fluid">
                    <h2 className="">
                        Les Tags
                    </h2>
                    <Spinner className="spinner-tag-menu" animation="border" variant="warning" role="status">
                        <span className="sr-only">Loading...</span>
                    </Spinner>
                </Container>
                </>
            );
        }
    }
}

const TagMenuHumboldt = withFirebase(TagMenu);


export default TagMenuHumboldt;