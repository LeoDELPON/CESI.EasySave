import React, {useState} from 'react';
import {
    Container,
    Row,
    Col
} from 'react-bootstrap';
import {useTransition, animated, } from 'react-spring';



const ImgRightSideBanner = props => {
    const {img} = props
    const transitions = useTransition(img, item => item.id, {
        from: { opacity: 0 },
        enter: { opacity: 1 },
        reset : true,
        config: { tension: 220, friction: 120 }
    })
    return (
        transitions.map(({item, props, key}) => 
        <animated.div style={{...props}} key={key} className="animated-div">
        <img 
        src={
            process.env.PUBLIC_URL + "img/ImageSite/" + item
        }
        key={key}
        className="banner-aboutus-img"
        alt={item}/>
        </animated.div>
        )
    );
}


class RightSideBannerImg extends React.Component{
    constructor(props) {
        super(props);
        this.switchImage = this.switchImage.bind(this);
        this.state = {
            differentImg: false,
            currentImage: 0,
            images : [
                "dev.png",
                "electronic.png",
                "secu.png"
            ]
        }
    }

    switchImage() {
    if (this.state.currentImage < this.state.images.length - 1) {
        this.setState({
            currentImage: this.state.currentImage + 1
        });
        this.props.setStateImgBannerRS(this.state.currentImage)
        } else {
        this.setState({
            currentImage: 0
        });
        }
        return this.currentImage;
    }
    
    componentDidMount() { 
        setInterval(
            this.switchImage, 3000, []
        );
    }

    render() {
        console.log(this.state.images[this.state.currentImage])
        return (
          <ImgRightSideBanner img={this.state.images[this.state.currentImage]}/>
        );
    }
}


class LeftSideBannerText extends React.Component {
    constructor(props) {
        super(props)
        this.switchContent = this.switchContent.bind(this);
        this.state = {
            currentContent: 0
        }
    }

    switchContent() {
        this.setState({
            currentContent : this.props.stateImgBannerRS
        })
    }

    componentDidMount() {
        setInterval(
            this.switchContent, 0
        );
    }

    render() {
        const titleContent = [
            "LE POINT DEV",
            "PROJETS MAISONS",
            "LA CYBERSECURITE"
        ]
        const content = [
            "Passionnés par l'univers de la programmation, notre équipage sera ravi de vous montrer nos plus beaux projets! ",
            "Vous avez toujours eu envie de faire votre propre drône maison ? Et bien nous aussi !",
            "Vm9pY2kgbGEgcHJlbWnDqHJlIGNsw6kgcXVpIHBlcm1ldCBkJ2FjdGl"
        ]
        return (
            <Container className="banner-about-leftside-container">
                    <h3>
                        {titleContent[this.state.currentContent]}
                    </h3>
                    <span>
                        {content[this.state.currentContent]}
                    </span>
            </Container>
    );
    }
}


export const BannerAboutUsHumboldt = () => {
    const [stateImgBannerRS, setStateImgBannerRS] = useState(0)
    return (
        <>
        <Container fluid className="banner-aboutus-bg">
            <div className="banner-aboutus-title">
                <h2>
                    A PROPOS
                </h2>
                <h4>
                    Ce que nous faisons de nos journées et de nos nuits
                </h4>
            </div>
            <Row>
                <Col xs={5} className="banner-aboutus-col">
                    <LeftSideBannerText stateImgBannerRS={stateImgBannerRS}/>
                </Col>
                <Col xs={7} className="banner-aboutus-col">
                    <RightSideBannerImg 
                    setStateImgBannerRS={setStateImgBannerRS}/>
                </Col>
            </Row>
        </Container>
        </>
    );
}