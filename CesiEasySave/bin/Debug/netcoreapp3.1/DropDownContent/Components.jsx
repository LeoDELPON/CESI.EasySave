import styled from "styled-components"

export const Icon = styled.div`
    width: 13px;
    height: 13px;
    margin-right: 13px;
    background-color: var(--blue);
    opacity: 0.8;
    display: inline-block;
`;

export const ProductsDropdownEl = styled.div`
    width: 29rem;
`;

export const Logo = styled.div`
    width: 38px;
    height: 38px;
    margin-right: 16px;
    border-radius: 100%;
    opacity: 0.6;
    background-color: ${({ color }) => `var(--${color})`};
`;

export const SubProductsList = styled.ul`
    li {
        display: flex;
        margin-bottom: 1rem;
    }
    h3 {
        margin-right: 1rem;
        width: 3.2rem;
        display: block;
    }
    a {
        color: var(--dark-grey);
    }
`;

export const ProductsSection = styled.ul`
    li {
        display: flex;
    }
`;

export const WorksWithStripe = styled.div`
    border-top: 2px solid #fff;
    display:flex;
    justify-content: center;
    align-items: center;
    margin-top: var(--spacer);
    padding-top: var(--spacer);
    }
    h3 {
    margin-bottom: 0;
}
`;

