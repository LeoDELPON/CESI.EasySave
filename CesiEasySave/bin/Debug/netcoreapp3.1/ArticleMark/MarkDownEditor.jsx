import React, {useRef, useEffect} from 'react'
import {
    Container,
    Row,
    Button,
    Col
} from 'react-bootstrap'


export const MarkDownEditorHumboldt = ({ content, changeContent }) => {
    const editorRef = useRef(null)

    useEffect(() => {
        localStorage.setItem("markdown", content)
    }, [content])

    // const onChangeImg = () => {

    // }

    // const onClickImg = () => {

    // }

    const handleClearClick = () => {
        changeContent("")
        editorRef.current.focus()
    }

    const handleEditorChange = (event) => {
        event.preventDefault()
        changeContent(event.target.value)
    }

    return(
        <>
            <Container fluid className="mardown-editor-container-fluid scroll">
                <Row className="markdown-editor-row-section">
                    <Col xs={10} className="markdown-editor-col-section-title">
                        <h3>
                            MarkDown Editeur
                        </h3>
                    </Col>
                    <Col xs={2} className="markdown-editor-col-section-button">
                        <Button 
                        className="markdown-editor-button-clear"
                        variant="warning" 
                        onClick={handleClearClick}>
                            Effacer
                        </Button>
                    </Col>
                </Row>
                {/* <input type="file" onChange={onChangeImg}/>
                <button onClick={onClickImg}> Upload </button>
                <textarea>
                    TITRE
                </textarea>
                <input/> */}
                <textarea
                    className="markdown-editor"
                    value={content}
                    onChange={handleEditorChange}
                    id="editor"
                    ref={editorRef}>
                </textarea>
            </Container>
        </>
    )
}