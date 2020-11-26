import React from 'react';
import { BannerAboutUsHumboldt } from '../BannerAboutUs';
import { PurposeAboutUsHumboldt } from '../PurposeAboutUs';
import { TechnoComponentHumboldt } from '../TechnoUsed';
import { WhoWeAreHumboldt } from '../WhoWeAre';
import { FooterHumboldt } from '../Footer';
import { withAuthorization } from '../Session';



const AboutUsHumboldt = () => {
    return (
        <>  
            <BannerAboutUsHumboldt/>
            <PurposeAboutUsHumboldt />
            <WhoWeAreHumboldt />
            <TechnoComponentHumboldt/>
            <FooterHumboldt />
        </>
    );
}
const condition = authUser => !!authUser;

export default withAuthorization(condition)(AboutUsHumboldt);
