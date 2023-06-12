import React, {useContext} from 'react';
import {Avatar, Container, Paper, Typography} from "@mui/material";
import LogoutIcon from '@mui/icons-material/Logout';
import {logout} from "../../utils/authUtils";
import {UserContext} from "../../contexts/_index";

const Menu = () => {
    const userCtx = useContext(UserContext)
    return (
        <Paper sx={{
            p: 3, boxShadow: 2, display: 'flex', flexDirection: 'column',
            width: { xs: '100%', sm: '60%', md: '430px' }, alignItems: 'center',
            borderRadius: '1.7rem'
        }}>
            <Container sx={{cursor: 'pointer', display: 'flex', flexDirection: 'column', alignItems: 'center'}} onClick={() => {
                logout()
                userCtx?.setUser(null)
            }}>
                <Avatar sx={{ m: 1, bgcolor: 'white' }}>
                    <LogoutIcon color={'warning'} />
                </Avatar>

                <Typography variant={'subtitle1'}
                            color={'red'}
                            sx={{textDecoration: 'underline'}}>
                    Выйти из аккаунта
                </Typography>
            </Container>
        </Paper>
    );
};

export default Menu;