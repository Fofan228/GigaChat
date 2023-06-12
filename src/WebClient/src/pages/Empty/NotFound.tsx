import React from 'react';
import {Avatar, Paper, Typography} from "@mui/material";
import SearchIcon from '@mui/icons-material/Search';
import {useNavigate} from "react-router-dom";
import ChatDivider from "../../components/ChatDivider";

const NotFound = () => {
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
                <SearchIcon color={'primary'} sx={{fontSize: '45px'}}/>
            </Avatar>

            <ChatDivider width="62%"/>

            <Typography variant={'h5'}
                        color={'red'}>
                Ошибка 404
            </Typography>

            <Typography variant={'subtitle1'}
                        color={'red'}>
                Страница не найдена
            </Typography>
        </Paper>
    );
};

export default NotFound;