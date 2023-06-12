import CssBaseline from '@mui/material/CssBaseline';
import React from 'react';
import ReactDOM from 'react-dom/client';
import { ThemeProvider } from '@mui/material/styles';
import App from './App';
import lightTheme from './themes/lightTheme';
import './index.css'
import {BrowserRouter} from 'react-router-dom'
import {UserContextProvider, NotificationProvider} from "./contexts/_index";

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);
root.render(
    <React.StrictMode>
        <BrowserRouter>
            <ThemeProvider theme={lightTheme} >
                <CssBaseline />
                <NotificationProvider>
                    <UserContextProvider>
                        <App />
                    </UserContextProvider>
                </NotificationProvider>
            </ThemeProvider>
        </BrowserRouter>
    </React.StrictMode>
);