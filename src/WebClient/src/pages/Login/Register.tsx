import React, {useContext, useState} from 'react';
import {UserContext} from "../../contexts/UserContext";
import {ChatContext} from "../../contexts/ChatContext";
import {Box, Button, TextField, Typography} from "@mui/material";
import ChatDivider from "../../components/ChatDivider";
import {NotificationContext} from "../../contexts/NotificationContext";
import {login, registration} from "../../utils/authUtils";

interface RegistrationProps {
    spinnerState: (success: boolean) => void
}

const Register = ({spinnerState}: RegistrationProps) => {

    const userCtx = useContext(UserContext);
    const noticeCtx = useContext(NotificationContext)

    const [userLogin, setUserLogin] = useState("");
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        spinnerState(true)
        const user = await registration(userName, userLogin, password)
        if (user) {
            userCtx?.setUser(user)
            noticeCtx?.showMessage({
                status: "success",
                duration: 3000,
                message: `Вы успешно зарегистрировались, ${user.name}`
            })
        }
        else {
            noticeCtx?.showMessage({
                status: "info",
                duration: 3000,
                message: "Не получилось зарегистрироваться!"
            })
        }
        spinnerState(false)
    }

    return (
        <>
            <Typography component="h1" variant="h5" color="primary.main">
                Создать
            </Typography>

            <ChatDivider width="62%" />

            <Box
                component="form"
                onSubmit={handleSubmit}
            >
                <Box sx={{ pr: 3, pl: 3 }}>
                    <TextField
                        label="Придумайте логин" fullWidth
                        type="text" required margin="dense"
                        onChange={e => setUserLogin(e.target.value)} />

                    <TextField
                        label="Ваше имя" fullWidth
                        type="text" required margin="dense"
                        onChange={e => setUserName(e.target.value)} />

                    <TextField
                        label="Пароль" fullWidth
                        type="text" required margin="dense"
                        onChange={e => setPassword(e.target.value)} />
                </Box>

                <Button variant="contained" type="submit" fullWidth sx={{ mt: 3, mb: 2 }}>
                    Зарегистрироваться
                </Button>
            </Box>
        </>
    );
};

export default Register;