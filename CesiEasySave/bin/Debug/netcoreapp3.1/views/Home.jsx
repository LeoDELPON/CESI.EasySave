import React from 'react';
import { FooterHumboldt } from '../Footer';
import { BannerHumboldt} from '../BannerHomePage';
import { TopArticleHomeHumboldt } from '../TopArticleHomePage';
import { OurSchoolHumboldt } from '../OurSchool';
import { NewsLetterHumboldt } from '../NewsLetter';
import {OurTeamHomeHumboldt} from '../OurTeamComponentHome';
import {CategoriesHomeHumboldt} from '../CategoriesComponentHome';
import {LookingForPiratesHumboldt} from '../LookingForPirates';
import {AccessProfilHomeHumboldt} from '../AccessToProfilHome';


const HomeHumboldt = () => {
    return (
        <>
            <BannerHumboldt/>
            <TopArticleHomeHumboldt/>
            <OurSchoolHumboldt/>
            <OurTeamHomeHumboldt/>
            <AccessProfilHomeHumboldt/>
            <CategoriesHomeHumboldt/>
            <LookingForPiratesHumboldt/>
            <NewsLetterHumboldt/>
            <FooterHumboldt />
        </>
    );
}

export default HomeHumboldt;