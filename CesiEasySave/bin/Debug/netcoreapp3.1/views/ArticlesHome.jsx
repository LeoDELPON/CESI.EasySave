import React from 'react';
import { Container } from 'react-bootstrap';
import { NavBarArticleHumboldt } from '../NavBarArticle';
import { ArticleHeaderHumboldt } from '../ArticlesHomeComponents';
import { ArticleHomeLeftHumboldt } from '../ArticleHomeLeftSide';
import { ArticleHomeRigthHumboldt } from '../ArticleHomeRightSide';
// Importation du mock des articles
import { articles } from '../../dataMock/articleMock';

const ArticlesHomeHumboldt = () => {
    return (
        <>
            <Container fluid className="container-fluid-article-home-humboldt">
                <div className="row">
                    <div className="col-sm-3">
                        <ArticleHomeLeftHumboldt />
                    </div>
                    <div className="col-sm-6">
                        <ArticleHeaderHumboldt articleList={articles} />
                    </div>
                    <div className="col-sm-3">
                        <ArticleHomeRigthHumboldt />
                    </div>
                </div>
            </Container>
        </>
    );
};

export default ArticlesHomeHumboldt;
