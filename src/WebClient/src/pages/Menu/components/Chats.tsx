import React, {useContext} from 'react';
import {Avatar, Box, Grid, List, ListItem, ListItemIcon, ListItemText, Paper, Typography} from "@mui/material";
import usernameToAvatar from "../../../utils/usernameToAvatar";
import {StoreContext} from "../../../contexts/_index";
import Groups2Icon from '@mui/icons-material/Groups2';
import ChatDivider from "../../../components/ChatDivider";
import {useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";

const Chats = observer(() => {
    const store = useContext(StoreContext)
    const nav = useNavigate()
    return store?.mobxStore?.myChats?.length === 0 ? (
        <Paper sx={{
            p: 3,
            boxShadow: 'none',
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            overflowY: "scroll",
            height: "100%"
        }}>
            <Avatar sx={{m: 1, bgcolor: 'white'}}>
                <Groups2Icon color={'warning'} sx={{fontSize: '45px'}}/>
            </Avatar>

            <ChatDivider width="62%"/>

            <Typography variant={'h5'} color={'#3759d5'}>
                Нет чатов
            </Typography>
            <Typography sx={{pt: 2}} variant={'subtitle1'} color={'#3759d5'} textAlign={'center'}>
                Создайте чат со своими друзьями
            </Typography>
        </Paper>
    ) : (
        <Box sx={{
            height: "100%",
            overflowY: "auto"
        }}>
            <List className={"message-container"}>
                {store?.mobxStore?.myChats?.map((chat, index) => (
                    <ListItem key={`user_${index}`} sx={{cursor: "pointer"}} onClick={() => {
                        nav('/chat', {state: JSON.stringify(chat)})
                    }}>
                        <ListItemIcon>
                            <Avatar {...usernameToAvatar(chat.title)} />
                        </ListItemIcon>
                        <ListItemText primary={chat.title}>{chat.title}</ListItemText>
                    </ListItem>
                ))}
            </List>
        </Box>
    )
})

export default Chats;