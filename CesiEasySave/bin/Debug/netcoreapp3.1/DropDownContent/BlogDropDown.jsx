import React from 'react';
import {
    Icon
} from './Components';

const BlogDropdown = () => {
    return (
        <div className="se-connecter-dropdown-container">
        <div className="se-connecter-dropdown-section">
            <div>
                <a href="/home">
                    <h3 className="se-connecter-dropdown-title">Humboldt Blog</h3>
                </a>
            <div className="se-connecter-dropdown-content-container">
                <div>
                <h4 className="se-connecter-dropdown-content-title">CATEGORIES</h4>
                <ul>
                    <li className="se-connecter-dropdown-content-li">
                    <a href="/">Cybersécurité</a>
                    </li>
                    <li className="se-connecter-dropdown-content-li">
                    <a href="/">Développement</a>
                    </li>
                    <li className="se-connecter-dropdown-content-li">
                    <a href="/">Système</a>
                    </li>
                    <li className="se-connecter-dropdown-content-li">
                    <a href="/">Réseau</a>
                    </li>
                    <li className="se-connecter-dropdown-content-li">
                    <a href="/">Electronique embarqué</a>
                    </li>
                </ul>
                </div>
                <div>
                <h4 className="se-connecter-dropdown-content-title">TOP TIERS </h4>
                <ul>
                    <li className="se-connecter-dropdown-content-li">
                    <a href="/">Apple Pay</a>
                    </li>
                    <li className="se-connecter-dropdown-content-li">
                    <a href="/">Testing</a>
                    </li>
                    <li className="se-connecter-dropdown-content-li">
                    <a href="/">Launch Checklist</a>
                    </li>
                </ul>
                </div>
            </div>
            </div>
        </div>
        <div className="se-connecter-dropdown-section">
            <ul>
            <li className="se-connecter-dropdwon-icons">
            <a href="/tags-menu">
                <Icon /> TAGS
                </a>
            </li>
            <li className="se-connecter-dropdwon-icons">
                <a href="/">
                <Icon /> FORUM
                </a>
            </li>
            <li className="se-connecter-dropdwon-icons">
                <a href="/">
                <Icon /> LE LAPIN
                </a>
            </li>
            </ul>
        </div>
        </div>
    );
};

export default BlogDropdown;