import React from 'react';
import {
    Jumbotron,
    Nav,
    Button
} from 'react-bootstrap';


const ArticleHomeMenu = () => {
    return (
        <div className="article-home-menu">
            <h2 className="article-home-menu-title">
                POSTS
            </h2>
            <div className=" col-sm-11 ">
                <Nav className="article-home-menu-nav">
                <Nav.Item>
                    <Nav.Link href="/home">
                    <Button 
                    variant="secondary"
                    className="article-home-menu-nav-button">Feed</Button>
                    </Nav.Link>
                </Nav.Item>
                <Nav.Item>
                    <Nav.Link eventKey="link-2">
                    <Button 
                    variant="secondary"
                    className="article-home-menu-nav-button">Récent</Button>
                    </Nav.Link>
                </Nav.Item>
                <Nav.Item>
                    <Nav.Link eventKey="link-1">
                    <Button 
                    variant="secondary"
                    className="article-home-menu-nav-button">Semaine</Button>
                    </Nav.Link>
                </Nav.Item>
                <Nav.Item>
                    <Nav.Link eventKey="link-2">
                    <Button 
                    variant="secondary"
                    className="article-home-menu-nav-button">Mois</Button>
                    </Nav.Link>
                </Nav.Item>
                <Nav.Item>
                    <Nav.Link eventKey="link-2">
                    <Button 
                    variant="secondary"
                    className="article-home-menu-nav-button">Année</Button>
                    </Nav.Link>
                </Nav.Item>
                </Nav>
            </div>
        </div>
    );
}

const ArticleHeaderImg = ({artImg, refImg}) => {
    return(
        <div className="article-header-image-container">
            <a href={refImg}>
                <img 
                src={process.env.PUBLIC_URL + "/img/ImageArticle/" + artImg} 
                alt="description modulable"
                className="article-header-img"/>
            </a>
        </div>
    );
}

const ArticleHeaderAuthor = ({artAut, artDate, artImgAut, artRef}) => {
    return (
        <div className="article-header-author-container">
            <img 
            src={process.env.PUBLIC_URL + "/img/ImageProfil/" + artImgAut} 
            alt="description modulable"
            className="article-header-author-image"/>
            <div className="article-header-author-name-container col-sm-2">
                <a 
                href="author"
                className="article-header-author-name"> 
                    {artAut}
                </a>
                <a 
                href={artRef}
                className="article-header-author-name"> {artDate} </a>
            </div>
        </div>
    );
}

const ArticleHeaderTitleContent = ({artTitle, artRef}) => {
    return (
        <h2>
            <a 
            href={artRef}
            className="article-header-content-title">
            {artTitle}
            </a>
        </h2>
    );
}

const ArticleHeaderTagsContent = ({artTag}) => {
    return (
        <div className="article-header-position-container">
            {
                artTag.map(tag =>
                        <a 
                        href="tag1"
                        className="article-header-content-position-tag">
                        <Button
                        variant="primary"
                        className="article-header-content-tag">{tag}</Button>
                        </a>
                    )
            }
        </div>
    );
}

const ArticleHeaderReactionsContent = ({artLike, artComm}) => {
    return (
        <div className="article-header-content-position-like-and-comment">
            <div className="article-header-content-like-comment">
                {artLike} <img src={process.env.PUBLIC_URL + "/img/ImageSite/heart.png"} alt="icone d'un like"/>
            </div>
            <div className="article-header-content-like-comment">
                {artComm} <img src={process.env.PUBLIC_URL + "/img/ImageSite/comment.png"} alt="icone d'un commentaire"/>
            </div>
        </div>
    );
}

const ArticleHeaderContent = ({artTitle, artRef, artTag, artLike, artComm}) => {
    return (
        <div className="article-header-content-body">
            <ArticleHeaderTitleContent artTitle={artTitle} artRef={artRef}/>
            <ArticleHeaderTagsContent artTag={artTag}/>
            <ArticleHeaderReactionsContent artLike={artLike} artComm={artComm}/>
        </div>
    );
}

export const ArticleHeaderHumboldt = props => {
    const {articleList} = props;
    return (
        <>
        <ArticleHomeMenu/>
        {
            articleList.map((art, index) => {
                if(index === 0) {
                    return (
                        <Jumbotron className="article-header">
                        <ArticleHeaderImg 
                        artImg={art.image} 
                        refImg={art.reference} 
                        key={index}/>
                        <div className="article-header-content">
                            <ArticleHeaderAuthor 
                            artAut={art.auteur} 
                            artDate={art.date} 
                            artImgAut={art.imageAuteur}
                            artRef={art.reference} 
                            key={index + 10}/>
                            <ArticleHeaderContent 
                            key={index + 20}
                            artTitle={art.titre}
                            artRef={art.reference}
                            artTag={art.tags}
                            artLike={art.like}
                            artComm={art.comment}/>
                        </div>
                        </Jumbotron>
                    );
                } else {
                    return (
                        <Jumbotron className="article-header">
                        <div className="article-header-content">
                            <ArticleHeaderAuthor 
                            artAut={art.auteur} 
                            artDate={art.date} 
                            artImgAut={art.imageAuteur}
                            artRef={art.reference} 
                            key={index + 10}/>
                            <ArticleHeaderContent 
                            key={index + 20}
                            artTitle={art.titre}
                            artRef={art.reference}
                            artTag={art.tags}
                            artLike={art.like}
                            artComm={art.comment}/>
                        </div>
                        </Jumbotron>
                    );
                }
            })
        }
        </>
    );
}


