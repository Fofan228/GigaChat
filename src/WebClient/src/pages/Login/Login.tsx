import { Box, Button, TextField, Typography } from '@mui/material';
import React, { useState, useContext } from 'react';
import ChatDivider from '../../components/ChatDivider';
import {NotificationContext, StoreContext} from '../../contexts/_index';
import {login} from "../../utils/authUtils";
import {useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";

interface LoginProps {
    spinnerState: (success: boolean) => void
}

const Login = observer(({spinnerState}: LoginProps) => {
    const nav = useNavigate()

    const store = useContext(StoreContext);
    const noticeCtx = useContext(NotificationContext)

    const [userLogin, setUserLogin] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        spinnerState(true)
        const user = await login(userLogin, password)
        if (user) {
            store?.mobxStore.refreshUser()
            store?.mobxStore.refreshToken()
            nav('/')
            noticeCtx?.showMessage({
                status: "success",
                duration: 1200,
                message: `Добро пожаловать, ${user.name}`
            })
        }
        else {
            noticeCtx?.showMessage({
                status: "info",
                duration: 3000,
                message: "Не получилось войти!"
            })
        }
        spinnerState(false)
    }

    return (
        <>
            <Typography component="h1" variant="h5" color="primary.main">
                Вход
            </Typography>

            <ChatDivider width="62%" />

            <Box
                component="form"
                onSubmit={handleSubmit}
            >
                <Box sx={{ pr: 3, pl: 3 }}>
                    <TextField
                        label="Логин" fullWidth
                        type="text" required margin="dense"
                        onChange={e => setUserLogin(e.target.value)} />

                    <TextField
                        label="Пароль" fullWidth
                        type="text" required margin="dense"
                        onChange={e => setPassword(e.target.value)} />
                </Box>

                <Button variant="contained" type="submit" fullWidth sx={{ mt: 3, mb: 2 }}>
                    Продолжить
                </Button>
            </Box>
        </>
    )
})

export default Login