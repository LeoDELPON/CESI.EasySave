import React from 'react';
import {
    Container,
    Row
} from 'react-bootstrap';
import {technos} from '../dataMock/architectureContentMock';
import {useSpring, animated} from 'react-spring';

class ArchitectureTechnoStack extends React.Component {
    constructor(props) {
        super(props)
        this.state = {...props};
    }

    render() {
        return(
            <>
            <Container fluid className="archi-techno-container-fluid">
                <Row>
                    {
                        this.state.technos.map( (techno, index) => {
                            return (
                                <div className="col archi-techno-col" key={index}>
                                    <ComponentTechnoStack tech = {techno} index = {index} key={index}/>
                                </div>
                            );
                        })
                    }
                </Row>
            </Container>
            </>
        );
    }
}

const ContentComponentTechnoStack = props => {
    
    return (
        props.content.map( (content, index) => {
            return (
                <li key={index}>
                    {content}
                </li>
            );
        })
    );
}

const ComponentTechnoStack = ({tech, index}) => {
    const interp = i => r => `translate3d(0, ${15 * Math.sin(r + (i * 2 * Math.PI) / 1.6)}px, 0)`;
    const { radians } = useSpring({
        to: async next => {
            while (1) await next({ radians: 2 * Math.PI })
        },
        from: { radians: 0 },
        config: { duration: 3500 },
        reset: true,
        })
    return (
        <>
        <animated.div 
        className="archi-techno-content-box"
        style={{ transform: radians.interpolate(interp(index)) }}>
            <h2>
                {tech.libelle}
            </h2>
            <h3>
                {tech.tech}
            </h3>
            <div className="archi-techno-content">
                <ContentComponentTechnoStack content = {tech.content} index={index}/>
            </div>
        </animated.div>
        </>
    );
}


const TitleTechnoStack = () => {
    return (
        <div className="techno-component-container-title">
            <h2>
                LA "TECHNOLOGY STACK"
            </h2>
            <h3>
                Voici en quelques points notre architecture web
            </h3>
        </div>
    );
}



export const TechnoComponentHumboldt = () => {
    return (
        <>
            <Container fluid className="techno-component-container-fluid">
                <TitleTechnoStack/>
                <ArchitectureTechnoStack technos = {technos}/>
            </Container>
        </>
    );
}