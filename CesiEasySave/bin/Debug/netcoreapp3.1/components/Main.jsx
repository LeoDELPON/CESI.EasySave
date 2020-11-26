import React from 'react'
import {
    Switch,
    Route,
    BrowserRouter as Router
} from 'react-router-dom';
import * as ROUTE from '../constant/route';
import Home from './views/Home';
import ArticlesHomeHumboldt from './views/ArticlesHome';
import AboutUsHumboldt from './views/AboutUs';
import ContactUsHumboldt from './views/ContactUs';
import ProfilHumboldt from './views/Profils';
import FAQHumboldt from './views/FAQ';
import LoginHumboldt from './views/Login';
import SignUpHumboldt from './views/SignUp';
import PwForgetHumboldt from './views/PwForget';
import AdminHumboldt from './views/Admin';
import AccountHumboldt from './views/Account';
import ArticleMakerHumboldt from './views/ArticleMaker';
import TagMenuHumboldt from './views/TagsHomeMenu';
import CategoriesHomeHumboldt from './views/CategoriesHome';
import {NavBarChoiceHumboldt} from '../components/NavBar';
import Page404Humboldt from './PageErrorHTML/404Page';
import TinderSwapUI from '../tinder_swap/ui';

export const MainHumboldt = props => {
    return (
        <>
        <Router>
            <NavBarChoiceHumboldt/>
            <Switch>
                <Route path={ROUTE.HOME_ARTICLE} component={ArticlesHomeHumboldt}/>
                <Route path={ROUTE.ABOUT_US} component={AboutUsHumboldt}/>
                <Route path={ROUTE.CONTACT} component={ContactUsHumboldt}/>
                <Route path={ROUTE.PROFIL} component={ProfilHumboldt}/>
                <Route exact path={ROUTE.HOME} component={Home}/>
                <Route path={ROUTE.FAQ} component={FAQHumboldt}/>
                <Route path={ROUTE.LOGIN} component={LoginHumboldt}/>
                <Route path={ROUTE.SIGN_UP} component={SignUpHumboldt}/>
                <Route path={ROUTE.PASSWORD_FORGET} component={PwForgetHumboldt}/>
                <Route path={ROUTE.ADMIN} component={AdminHumboldt}/>
                <Route path={ROUTE.ACCOUNT} component={AccountHumboldt}/>
                <Route path={ROUTE.ARTICLE_MAKER} component={ArticleMakerHumboldt}/>
                <Route path={ROUTE.TAGS_MENU} component={TagMenuHumboldt}/>
                <Route path={ROUTE.CATEGORIES} component={CategoriesHomeHumboldt}/>
                <Route path={ROUTE.PAGE404} component={Page404Humboldt}/>
                <Route path={ROUTE.TINDER_SWAP} component={TinderSwapUI}/>
            </Switch>
        </Router>
        </>
    );
}