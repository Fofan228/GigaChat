import React, {useContext} from 'react';
import {
    Grid,
    IconButton,
    List,
    ListItem,
    ListItemIcon, ListItemText,
    Paper,
    Typography
} from "@mui/material";
import LogoutIcon from '@mui/icons-material/Logout';
import {logout} from "../../utils/authUtils";
import {StoreContext} from "../../contexts/_index";
import Chats from "./components/Chats";
import AddIcon from '@mui/icons-material/Add';
import {useNavigate} from "react-router-dom";


const Menu = () => {
    const store = useContext(StoreContext)
    const nav = useNavigate()
    return (
        <Grid container component={Paper} direction="row" sx={{
            p: 3, boxShadow: 2,
            width: "100%",
            borderRadius: '1.3rem', height: {xs: '90vh', md: '80vh'}
        }} spacing={2}>

            <Grid item xs={12}
                  sx={{
                      display: 'flex', alignItems: 'center',
                      justifyContent: 'space-between', mb: 2, paddingLeft: '0px !important'
                  }}
            >
                <IconButton color='error' onClick={() => {
                    logout()
                    store?.mobxStore.refreshUser()
                    store?.mobxStore.refreshToken()
                    nav('/auth')
                }}>
                    <LogoutIcon color={'warning'} />
                </IconButton>

                <Typography variant="h5" color="primary.main">
                    Чаты
                </Typography>

            </Grid>

            <Chats/>

            <Grid item xs={12} md={9}
                  sx={{
                      border: '1px solid rgba(0, 0, 0, 0.12)',
                      paddingTop: '0px !important',
                      borderRadius: '10px',
                      height: '90%'
                  }}>
                <Typography variant={'h6'} color={'#3759d5'}>
                    Управление чатами
                </Typography>
                <List>
                  <ListItem sx={{cursor: "pointer"}} onClick={() => {}}>
                      <ListItemIcon>
                          <AddIcon color={'info'} />
                      </ListItemIcon>
                      <ListItemText>Создать чат</ListItemText>
                  </ListItem>
                </List>
            </Grid>
        </Grid>
    );
};

export default Menu;