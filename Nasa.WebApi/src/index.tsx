import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import AppRoutes from './AppRoutes';
import { store } from './reduxStore/reduxStore';

const mainApp = document.querySelector('#root') as HTMLElement;

if (mainApp)
    ReactDOM.render(
        <Provider store={store}>
            <AppRoutes />
        </Provider>, mainApp
    );