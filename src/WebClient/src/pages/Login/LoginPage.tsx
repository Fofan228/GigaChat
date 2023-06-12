import React, {useState} from 'react';
import {Avatar, Backdrop, CircularProgress, Paper, Typography} from "@mui/material";
import SendIcon from "@mui/icons-material/Send";
import Login from "./Login";
import Register from "./Register";

const LoginPage = () => {

    const [loginSwitcher, setLoginSwitcher] = useState(true)
    const [spinner, setSpinner] = useState(false)

    const switchLoginForm = () => setLoginSwitcher(!loginSwitcher)

    return (
        <>
            <Paper sx={{
                p: 3, boxShadow: 2, display: 'flex', flexDirection: 'column',
                width: { xs: '100%', sm: '60%', md: '430px' }, alignItems: 'center',
                borderRadius: '1.7rem'
            }}>
                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                    <SendIcon />
                </Avatar>
                {
                    loginSwitcher ? (
                        <Login spinnerState={setSpinner} />
                    ) : (
                        <Register spinnerState={setSpinner} />
                    )
                }
                <Typography variant={'subtitle1'}
                            color={'gray'}
                            sx={{textDecoration: 'underline', cursor: 'pointer'}}
                            onClick={switchLoginForm}>
                    {
                        loginSwitcher ? "Нет аккаунта? Нажмите и зарегистрируйтесь" : "Нажмите, чтобы войти в аккаунт"
                    }
                </Typography>
            </Paper>

            <Backdrop
                sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                open={spinner}
            >
                <CircularProgress color="inherit" />
            </Backdrop>
        </>
    )
};

export default LoginPage;