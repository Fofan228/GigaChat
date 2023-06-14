import React, {useContext, useState} from 'react';
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
import {ConnectContext, NotificationContext, StoreContext} from "../../contexts/_index";
import Chats from "./components/Chats";
import AddIcon from '@mui/icons-material/Add';
import {useNavigate} from "react-router-dom";
import ChooseUsersModal from "../../components/ChooseUsersModal";


const Menu = () => {
    const store = useContext(StoreContext)
    const nav = useNavigate()
    const connect = useContext(ConnectContext)
    const notification = useContext(NotificationContext)
    const [usersModal, setUsersModal] = useState(false)

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
                {usersModal && <ChooseUsersModal open={usersModal}
                                                 setOpen={setUsersModal}
                                                 needsTitle={true}
                                                 filterByUserId={[store!!.mobxStore!!.user!!.id]}
                                                 submit={(title, usersIds) => {
                                                     console.log({
                                                         title,
                                                         usersIds
                                                     })
                                                     connect?.connection?.invoke("OpenChatRoom", {
                                                         title,
                                                         userIds: usersIds
                                                     }).then(r => {
                                                         console.log(r, 'успешно')
                                                     }).catch(e => {
                                                         console.log(e)
                                                         notification?.showMessage({
                                                             status: "error",
                                                             duration: 2000,
                                                             message: "Не удалось создать чат"
                                                         })
                                                     })
                                                 }}/>}
                <IconButton color='error' onClick={() => {
                    logout()
                    store?.mobxStore.refreshUser()
                    store?.mobxStore.refreshToken()
                    nav('/auth')
                }}>
                    <LogoutIcon color={'warning'}/>
                </IconButton>

                <Typography variant="h5" color="primary.main">
                    Чаты
                </Typography>
            </Grid>

            <Grid item md={3}
                  sx={{
                      height: '90%'
                  }}>
                <Chats/>
            </Grid>

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
                    <ListItem sx={{cursor: "pointer"}} onClick={() => {
                        setUsersModal(true)
                    }}>
                        <ListItemIcon>
                            <AddIcon color={'info'}/>
                        </ListItemIcon>
                        <ListItemText>Создать чат</ListItemText>
                    </ListItem>
                </List>
            </Grid>
        </Grid>
    );
};

export default Menu;