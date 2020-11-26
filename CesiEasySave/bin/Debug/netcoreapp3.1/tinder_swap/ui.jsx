import React, {useRef, useEffect, Component} from 'react';
import {Card, Button} from 'react-bootstrap';
import Request_API from './request_api';
class TinderSwapUI extends Component {
    render(){
    const API_URL = "http://127.0.0.1:8080";    
    var request = new Request_API(API_URL);
    function acceptCard(){
        alert('Accepter ! ');
    }
    function declineCard(){
        alert('Decliner ! ');
    }
    const createCard = (title, text, img_src, id) => {
        return <Card style={{ width: '18rem' }}>
        <Card.Img variant='top' src={process.env.PUBLIC_URL + img_src} />
        <Card.Body>
          <Card.Title>{title}</Card.Title>
          <Card.Text>
            {text}
          </Card.Text>
          <Button variant='primary' style={{backgroundColor:"green"}} onClick={acceptCard}>Accept</Button>
          <Button variant='primary' style={{backgroundColor:"red"}} onClick={declineCard}>Decline</Button>
        </Card.Body>
      </Card>
    };

       return (<>
            {createCard("title", "ceci est un text", "path/to/img", "1")}
        </>);
    }
}
export default TinderSwapUI;