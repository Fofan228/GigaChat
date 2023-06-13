import React, {useContext} from 'react';
import {Avatar, Grid, List, ListItem, ListItemIcon, ListItemText, Paper, Typography} from "@mui/material";
import usernameToAvatar from "../../../utils/usernameToAvatar";
import {StoreContext} from "../../../contexts/_index";
import Groups2Icon from '@mui/icons-material/Groups2';
import ChatDivider from "../../../components/ChatDivider";

const Chats = () => {
    const store = useContext(StoreContext)
    return (
        <Grid item md={3} sx={{
            overflowY: 'auto',
            display: { xs: "none", sm: "block" }
        }}>
            {
                store?.mobxStore.myChats.length === 0 &&
                <Paper sx={{
                    p: 3, boxShadow: 'none', display: 'flex', flexDirection: 'column', alignItems: 'center'
                }}>
                    <Avatar sx={{m: 1, bgcolor: 'white'}}>
                        <Groups2Icon color={'warning'} sx={{fontSize: '45px'}} />
                    </Avatar>

                    <ChatDivider width="62%"/>

                    <Typography variant={'h5'} color={'#3759d5'}>
                        Нет чатов
                    </Typography>
                    <Typography sx={{pt: 2}} variant={'subtitle1'} color={'#3759d5'} textAlign={'center'}>
                        Создайте чат со своими друзьями
                    </Typography>
                </Paper>
            }
            <List>

                {store?.mobxStore.myChats.map((chat, index) => (
                    <ListItem key={`user_${index}`}>
                        <ListItemIcon>
                            <Avatar {...usernameToAvatar(chat.title)} />
                        </ListItemIcon>
                        <ListItemText primary={chat.title}>{chat.title}</ListItemText>
                    </ListItem>
                ))}
            </List>
        </Grid>
    );
};

export default Chats;