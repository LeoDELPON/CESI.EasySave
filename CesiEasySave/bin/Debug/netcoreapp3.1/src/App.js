import React, {Component} from 'react';
import './style/App.scss';
import {MainHumboldt} from './components/Main';
import {withAuthentication} from './components/Session';

class App extends Component {
  constructor(props) {
    super(props)
    this.state = {
      authUser : null
    }
  }
  render() {
      return (
        <>
          <MainHumboldt/>
        </>
      );
  }
}

export default withAuthentication(App);
