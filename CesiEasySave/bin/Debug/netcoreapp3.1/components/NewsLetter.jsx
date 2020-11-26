import React, {useState} from 'react'
import {
    Container,
    Button
} from 'react-bootstrap';
import {withFirebase} from './Firebase'

const ContainerNewsLetter = () => {
    return (
        <>
        <Container className="newsletter-container">
            <Container>
            <h2 className="newsletter-title">
                Tenez-vous au courant !
            </h2>
            <ButtonNewsletterComponentFirebase/>
            </Container>
        </Container>
        </>
    );
}



class ButtonNewsletterComponent extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            isChanged : false,
            email : ''
        }
        this.libele = ["Clique moi", "Merci !"]
    }
    toggle = () =>  this.setState(state => ({isChanged: true    }))

    onSubmit = event => {
        const emailNewsletter = this.state.email;
        this.props.firebase.doPostNewsletterForm(emailNewsletter)
        .then(() => {
            alert("Success");
            this.setState({
                isChanged : false,
                email : ''
            });
        })
        .catch(error => {
            console.log(error)
        }) 
        event.preventDefault();
    }

    onChange = event => {
        this.setState(
            {
                [event.target.name] : [event.target.value]
            }
        )
    };

    render() {
        return(
            <>
            <div className="div-button" 
            onClick={() => this.toggle()}>
                <div 
                    variant="secondary" 
                    id="nl-button"
                    className={this.state.isChanged ? "newsletter-button active" : "newsletter-button"}>
                        {
                            this.state.isChanged ? <FormNewsLetter stateForm={this.state.isChanged} email={this.state.email} onChange={this.onChange} onSubmit={this.onSubmit}/> : "Clique moi"
                        }
                </div>
            </div>
            </>
        );
    }
}

const ButtonNewsletterComponentFirebase = withFirebase(ButtonNewsletterComponent)

const FormNewsLetter = ({stateForm, email, onChange, onSubmit}) => {
    const [isUp, setIsUp] = useState(false)
    if(stateForm) {
        setInterval(() => {
            setIsUp(true)
        }, 310)
    }
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    const isInvalid = !regex.test(email)
    return (
        <>
        <div className="form-container">
            <form
            onSubmit={onSubmit}
            className="newsletter-form"
            autoComplete="off">
                <div 
                className="newsletter-form-table-cell">
                    <input 
                    type="email" 
                    value={email}
                    name="email" 
                    placeholder="Ton adresse courrielle" 
                    spellCheck="false"
                    onChange={onChange}
                    className="input-email" 
                    required/>
                </div>
                <div
                className="newsletter-form-table-cell">
                    <Button  
                    type="submit"
                    variant="warning"
                    className={isUp ? "newsletter-submit-button active" : "newsletter-submit-button"}
                    disabled={isInvalid}>
                        Envoyer
                    </Button>
                </div>
            </form>
        </div>
        </>
    );
}

export const NewsLetterHumboldt = () => {
    return (
        <>

        <Container fluid className="newsletter-container-fluid big-pattern-anim">
            <div className="newsletter-background-color"></div>
            <ContainerNewsLetter/>
        </Container>
        </>
    );
}