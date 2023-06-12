import React from 'react';
import {Avatar, Paper, Typography} from "@mui/material";
import LockIcon from '@mui/icons-material/Lock';
import {useNavigate} from "react-router-dom";
import ChatDivider from "../../components/ChatDivider";

const NotAccess = () => {
    const navigate = useNavigate()
    return (
        <Paper sx={{
            p: 3, boxShadow: 2, display: 'flex', flexDirection: 'column',
            width: {xs: '100%', sm: '60%', md: '430px'}, alignItems: 'center',
            borderRadius: '1.7rem', cursor: 'pointer'
        }} onClick={() => {
            navigate('/')
        }}>
            <Avatar sx={{m: 1, bgcolor: 'white'}}>
                <LockIcon color={'warning'} sx={{fontSize: '45px'}}/>
            </Avatar>

            <ChatDivider width="62%"/>

            <Typography variant={'h5'}
                        color={'red'}>
                Ошибка 403
            </Typography>

            <Typography variant={'subtitle1'}
                        color={'red'}>
                У вас нет доступа к этой странице
            </Typography>
        </Paper>
    );
};

export default NotAccess;