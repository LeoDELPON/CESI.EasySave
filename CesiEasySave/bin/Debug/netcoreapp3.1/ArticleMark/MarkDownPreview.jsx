import React, { useState, useEffect } from "react";
import axios from 'axios';
import * as marked from "marked";
import {
    Container,
    Col,
    Row,
    Button
} from 'react-bootstrap'
import { FullScreen, useFullScreenHandle } from "react-full-screen";
import Firebase from '../Firebase/firebase';
import { withFirebase } from "../Firebase";

const MarkDownPreview = ({ content , edit, firebase}) => {
    const [html, setHtml] = useState(getHtml(content));
    const handle = useFullScreenHandle();
    const editFromPreliminaire = edit;

    useEffect(() => {
        setHtml(getHtml(content));
    }, [content]);

    const handleFullScreen = () =>
    handle.active ? handle.exit() : handle.enter();

    const handleSaveClick = event => {
        if(editFromPreliminaire != undefined){
            save(editFromPreliminaire, html);
        }
    };
    const save = (data, contentArticle) => {
        console.log(data);
        firebase.doArticlePostInWaiting(
            data.url_img, 
            data.title_article, 
            data.tags_tagged, 
            data.categories_in,
            contentArticle);        
    }
    return(
        <>
            <Container fluid className="markdown-renderer-container-fluid">
                <Row className="markdown-renderer-row-section">
                    <Col className="markdown-renderer-col-section-title">
                        <h3>
                            Aperçu
                        </h3>
                    </Col>
                    <Col className="markdown-renderer-col-section-button">
                        <Button 
                        variant="warning"
                        className="markdown-renderer-button-publish"
                        onClick={handleSaveClick}>
                            Publier !
                        </Button>
                        <Button 
                        variant="warning"
                        className="markdown-renderer-button-full-screen"
                        onClick={handleFullScreen}>
                            Full Screen
                        </Button>
                    </Col>
                </Row>
                <Row className="markdown-renderer-row-content">
                    <FullScreen handle={handle}>
                        <div
                        id="preview"
                        className={`html-div ${handle.active ? "preview-fullscreen" : ""}`}
                        dangerouslySetInnerHTML={{ __html: html }}></div>
                    </FullScreen>
                </Row>
            </Container>
        </>
    );

}

export const MarkDownPreviewHumboldt = withFirebase(MarkDownPreview);

const getHtml = (markdown) => {
    return marked(markdown);
};

const DATA_ARTICLE_SENDING = {
    title : "",
    author: "",
    tags: "",
    category: "",
    content: "",
    img : null
}