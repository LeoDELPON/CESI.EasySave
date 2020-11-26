import React, {useState, useEffect, useReducer} from 'react';
import {MarkDownEditorHumboldt} from '../ArticleMark/MarkDownEditor';
import {MarkDownPreviewHumboldt} from '../ArticleMark/MarkDownPreview';
import {FooterHumboldt} from '../Footer';
import { Multiselect } from 'multiselect-react-dropdown';
import Split from 'react-split';
import {withFirebase} from '../Firebase';
import {
    Container,
    Row,
    Col,
    Button
} from 'react-bootstrap';


const ArticleMakerHumboldt = () => {

    var markdown = '';
    const [markDown, setMarkDown] = useState(markdown);
    const [orientation, setOrientation] = useState("horizontal");
    const [edit, setEdit] = useState();

    const getEdit = (edit) => {
        setEdit(edit)
    }

    useEffect(() => {
        let changeOrientation = () => {
        setOrientation(window.innerWidth < 600 ? "vertical" : "horizontal");
        };
        changeOrientation();
        window.onresize = changeOrientation;
    }, []);
    
    return (
        <>
        <Container fluid className="article-home-page-maker-bg">
        <Container fluid className="article-home-page-maker-title-container-fluid">
            <Container className="article-home-page-maker-title">
                <h2>
                    ECRIS NOUS UN JOLI ARTICLE 
                </h2>
                <h3>
                    Merci à l'avance d'avoir contribué à la pérénnité de Humboldt-Life,
                    Cela nous fait énormément plaisir !
                </h3>
            </Container>
        </Container>
        <EditPreliHumboldt getEdit={getEdit}/>
        <Container fluid className="article-home-page-maker-container-fluid">
            <Split
                className="wrapper-card"
                sizes={[50, 50]}
                minSize={orientation === "horizontal" ? 300 : 100}
                expandToMin={true}
                gutterAlign="center"
                direction={orientation}
            >
                <MarkDownEditorHumboldt content={markDown} changeContent={setMarkDown}/>
                <MarkDownPreviewHumboldt content={markDown} edit={edit} />
            </Split>
        </Container>
        <FooterHumboldt/>
        </Container>
        </>
    );
}

const INIT_EDIT_PRELIMINAIRE = {
    coverImgArticleFile : "",
    titleArticle : "",
    tags : [],
    tagsChoices : [],
    categories : [],
    categoriesChoices : [],
    error: null
}

class EditPreli extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            ...INIT_EDIT_PRELIMINAIRE
        };
        this.Tags = [];
        this.finalData = {
            url_img : "",
            title_article : "",
            tags_tagged : [],
            categories_in : []
        }
        this.Categories = [];
    }

    componentWillMount() {
        this.props.firebase.db.ref("Tags/").once("value").then(
            (snapshot) => {
                snapshot.forEach(tag => {
                    if(tag) {
                        this.Tags.push(tag.val().libelle);
                    }
                })
                this.setState({
                    tagsChoices : this.Tags
                })
            }
        )
        this.props.firebase.db.ref("Categories/").once("value").then(
            (snap) => {
                snap.forEach(category => {
                    if(category) {
                        this.Categories.push(category.val().libelle);
                    }
                })
                this.setState({
                    categoriesChoices : this.Categories
                })
            }
        )
    }

    onChange = (event) => {
        this.setState(
            {
                [event.target.name]: event.target.value
            }
        )
    }

    onChangePicture = (event) => {
        if(event.target.files[0]) {
            this.setState({
                coverImgArticleFile: event.target.files[0]
            })
        } else {
            return null;
        }
    }

    onClick = (event) => {
        const { coverImgArticleFile, titleArticle, tags, categories} = this.state;
        this.finalData = {
            url_img : this.state.coverImgArticleFile,
            title_article : this.state.titleArticle,
            tags_tagged : this.state.tags,
            categories_in : this.state.categories
        }
        this.props.getEdit(this.finalData)
    }
    
    onSelectTags = (selectedList) => {
        this.setState({
            tags : selectedList
        })
    }

    onSelectCategories = (event) => {
        this.setState({
            categories : event
        })
    }

    onRemoveTags =(selectedList) => {
        this.setState({
            tags : selectedList
        })
    }

    onRemoveCategories = (selectedList) => {
        this.setState({
            categories : selectedList
        })
    }

    render() {
        const {coverImgArticleFile, titleArticle, tags, categories} = this.state;
        const isInvalidButton = (coverImgArticleFile.length !== 0) && (titleArticle.length !== 0) && (categories.length !== 0)
        return(
            <>
            <Container fluid className="article-home-page-maker-datas-container-fluid">
                <Container fluid className="article-home-page-maker-datas-wrapper">
                    <Row className="article-home-page-maker-data-row"> 
                            <Col>
                                <h3>
                                    Edit préliminaire
                                </h3>   
                            </Col>
                            <Col className="article-home-page-maker-datas-button-col">
                                <Button 
                                onClick={this.onClick}
                                variant="warning" 
                                disabled={!isInvalidButton} > Sauvegarder </Button>
                            </Col>
                        </Row>
                        <Row className="article-home-page-maker-data-content-row">
                            <Col>
                                <h3>
                                    Choix de l'image de couverture
                                </h3>
                                <input 
                                required
                                className="article-home-page-maker-data-input"
                                type="file" 
                                onChange={this.onChangePicture}/>
                            </Col>
                            <Col>
                                <h3>
                                    Choix du titre
                                </h3>
                                <input
                                required 
                                className="article-home-page-maker-data-input"
                                type="text"
                                name="titleArticle"
                                value={titleArticle}
                                onChange={this.onChange}
                                placeholder="Titre"
                                />
                            </Col>
                            <Col>
                                <h3>
                                    Choix des tags (4max)
                                </h3>
                                <Multiselect
                                required 
                                options={this.state.tagsChoices}
                                onSelect={this.onSelectTags}
                                onRemove={this.onRemoveTags}
                                selectionLimit="4"
                                name="tags"
                                isObject={false}
                                />
                            </Col>
                            <Col>
                                <h3>
                                    Choix de la catégorie
                                </h3>
                                <Multiselect
                                options={this.state.categoriesChoices}
                                onSelect={this.onSelectCategories}
                                onRemove={this.onRemove}
                                selectionLimit="1"
                                isObject={false}
                                />
                            </Col>
                        </Row>
                </Container>
            </Container>
            </>
        );
    }
}

const EditPreliHumboldt = withFirebase(EditPreli)

export default ArticleMakerHumboldt