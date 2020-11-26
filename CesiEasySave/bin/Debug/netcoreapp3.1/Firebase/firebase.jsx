import app from 'firebase/app';
import 'firebase/auth';
import 'firebase/database';
import 'firebase/storage';

const firebaseConfig = {
    apiKey: process.env.REACT_APP_API_KEY,
    authDomain: process.env.REACT_APP_AUTHDOMAIN,
    databaseURL: process.env.REACT_APP_BASEURL,
    projectId: process.env.REACT_APP_PROJECT_ID,
    storageBucket: process.env.REACT_APP_STORAGEBUCKET,
    messagingSenderId: process.env.REACT_APP_MESSAGING_SENDER_ID,
    appId: process.env.REACT_APP_APP_ID,
    measurementId: process.env.REACT_APP_MEASUREMENT_ID
};

class Firebase {
    constructor() {
        app.initializeApp(firebaseConfig);
        this.auth = app.auth();
        this.db = app.database();
        this.storage = app.storage();
        this.credential = app.auth.EmailAuthProvider;

    }

    DoReAuthenticate = (email, password) => {
        const credentials = this.credential.credential(email, password)
        this.auth.currentUser.reauthenticateWithCredential(credentials)
    }

    doCreateUserWithEmailAndPassword = (email, password) =>
        this.auth.createUserWithEmailAndPassword(email, password);

    doSignInWithEmailAndPassword = (email, password) =>
        this.auth.signInWithEmailAndPassword(email, password);

    doSignOut = () => this.auth.signOut();

    doPasswordReset = email => this.auth.sendPasswordResetEmail(email);

    doPasswordUpdate = password =>
        this.auth.currentUser.updatePassword(password);

    doEmailUpdate = (email, password) => {
        this.DoReAuthenticate(email, password)
        this.auth.currentUser.updateEmail(email)
    }

    //User's API
    user = uid => this.db.ref(`users/${uid}`);
    users = () => this.db.ref('users');

    //SettingInfo
    doPseudoUpdateDb = (pseudo) => this.db.ref('users/' + this.auth.currentUser.uid  ).update({
        username : pseudo
    })
    doEmailUpdateDb = (email) => this.db.ref('users/' + this.getUid()).update({
        email : email
    })
    doPictureUrlDb = (url) => this.db.ref('users/' + this.getUid()).update({
        pictureUrl : url
    })
    doAccountDeleteDb = (uid) => this.db.ref('users/' + uid ).remove()
    getUid = () => this.auth.currentUser.uid;

    //PostContactForm
    doPostContactForm = (nom, email, content) => this.db.ref('ContactForm/')
    .push()
    .set({
        nom,
        email,
        content
    })
    .toString()

    //PostEmailNewsLetter
    doPostNewsletterForm = (email) => 
    this.db.ref('Email/')
    .push()
    .set({
        email
    })
    .toString()


    doArticlePostInWaiting = (img, title, tags, category, content) =>  {
        var authName = "";
        this.db.ref('users/' + this.getUid() + '/username/').once("value", snapshot => {
            authName = snapshot.val();
        });
        try {
            this.storage.ref('ArticleImgStandBy/'+ this.getUid() + '/' + title).put(img);
            this.db.ref('ArticleStandBy/').push().set({
                title,
                authName,
                tags,
                category,
                content
            })
        } catch(e) {
            console.log(e);
        }
    }
    
}

export default Firebase;