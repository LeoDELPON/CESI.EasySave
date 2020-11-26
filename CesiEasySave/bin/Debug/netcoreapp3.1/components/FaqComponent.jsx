import React from 'react';
import {faqs} from '../dataMock/faqContentMock';
import {
    Card,
    Button,
    Accordion,
    Container
} from 'react-bootstrap';
import {FooterHumboldt} from './Footer';
 
const faq = faqs;

const AccordionList = () => {
    console.log(faq);
    return(
        <Accordion defaultActiveKey="0" className="faq-accordion">
            {
                faq.map( (f, index) => 
                    <AccordionComponent title={f.question} content={f.content} index={index + 1} key={index}/>
                )
            }
        </Accordion>
    );
}

const AccordionComponent = ({title, content, index}) => {
    return (
    <Card className="card-accordion-faq">
        <Card.Header className="card-header-accordion-faq">
        <Accordion.Toggle 
        as={Button} 
        variant="link" 
        eventKey={index}
        className="card-title-accordion-faq">
            {title}
        </Accordion.Toggle>
        </Card.Header>
        <Accordion.Collapse eventKey={index}>
        <Card.Body className="card-content-accordion-faq">
            {content}
        </Card.Body>
        </Accordion.Collapse>
    </Card>
    );
}

const FaqTitle = () => {
    return (
        <Container fluid className="faq-title-container-fluid">
            <img src={process.env.PUBLIC_URL + "/img/ImageSite/faq.png"} alt="faq logo"/>
            <h2>
                Foire à Question 
            </h2>
        </Container>
    );
}



const ArchiFAQHumboldt = () => {
    return(
        <>
        <Container fluid className="faq-bg">
            <Container fluid className="faq-container-fluid">
                <Container>
                    <FaqTitle/>
                    <AccordionList/>
                </Container>
            </Container>
        </Container>
        <FooterHumboldt/>
        </>
    );
}


export const AccordionHumboldt = () => {
    return (
        <>
            <ArchiFAQHumboldt/>
        </>
    );
}