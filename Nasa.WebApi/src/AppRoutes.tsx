import RoverContainer from './components/rover/RoverContainer';
import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

export default () => {

    return (
        <BrowserRouter/*  history={browserHistory} */>
            <Switch>
                <Route path='/' exact component={RoverContainer} />
            </Switch>
        </BrowserRouter>
    )
}