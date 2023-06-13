import React, {useEffect, useRef, useContext, useState} from 'react'
import {Paper, Typography, Grid, Box, Divider, IconButton, Avatar} from '@mui/material';
import MessageItem from './components/MessageItem';
import SendMessage from './components/SendMessage';
import Users from './components/Users';
import {ArrowBackIos} from '@mui/icons-material';
import {ChatContext, ConnectContext, NotificationContext, StoreContext} from '../../contexts/_index';
import {useLocation, useNavigate} from "react-router-dom";
import LogoutIcon from "@mui/icons-material/Logout";
import PersonAddAlt1Icon from '@mui/icons-material/PersonAddAlt1';
import {Room} from "../../models/Room";
import ChooseUsersModal from "../../components/ChooseUsersModal";
import Groups2Icon from "@mui/icons-material/Groups2";
import ChatDivider from "../../components/ChatDivider";

const ChatRoom = () => {

    const chatCtx = useContext(ChatContext);
    const connect = useContext(ConnectContext)
    const notification = useContext(NotificationContext)
    const [openUserAdd, setOpenUserAdd] = useState(false)

    const nav = useNavigate()

    const location = useLocation();
    const chatInfo: Room | undefined = JSON.parse(location?.state)

    const messageRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        if (messageRef && messageRef.current) {
            const {scrollHeight, clientHeight} = messageRef.current;
            messageRef.current.scrollTo({left: 0, top: scrollHeight - clientHeight});
        }
    }, [chatCtx?.messages]);


    return (
        <Grid container component={Paper} direction="row" sx={{
            p: 3, boxShadow: 2,
            width: "100%",
            borderRadius: '1.3rem', height: {xs: '90vh', md: '80vh'}
        }} spacing={2}>
            {openUserAdd && <ChooseUsersModal open={openUserAdd}
                                              setOpen={setOpenUserAdd}
                                              title={false}
                                              filterByUserId={chatCtx!!.connectedUsers.map(us => us.id)}
                                              submit={(title, userIds) => {
                                                  userIds.forEach(newUser => {
                                                      connect?.connection?.invoke("InviteToChatRoom", {
                                                          userId: newUser,
                                                          chatRoomId: chatInfo?.id
                                                      }).catch((e) => {
                                                          console.log(e)
                                                          notification?.showMessage({
                                                              message: "Не удалось добавить пользователя с id = " + newUser,
                                                              status: "error",
                                                              duration: 1500
                                                          })
                                                      })
                                                  })
                                              }} />}
            <Grid item xs={12}
                  sx={{
                      display: 'flex', alignItems: 'center',
                      justifyContent: 'space-between', mb: 2, paddingLeft: '0px !important'
                  }}
            >
                <IconButton color='error' onClick={() => nav('/')}>
                    <ArrowBackIos/>
                </IconButton>

                <Box display={'flex'} alignItems={'center'}>
                    <Typography variant="h5" color="primary.main">
                        {chatInfo?.title ?? "Комната"}
                    </Typography>

                    <PersonAddAlt1Icon color={'info'} sx={{cursor: 'pointer', ml: 2}} onClick={() => {
                        setOpenUserAdd(true)
                    }}/>
                    <LogoutIcon color={'warning'} sx={{cursor: 'pointer', ml: 2}} onClick={() => {
                        connect?.connection?.invoke("ExitFromChatRoom", chatInfo?.id)
                    }}/>
                </Box>


            </Grid>

            <Users/>

            <Grid item xs={12} md={9}
                  sx={{
                      border: '1px solid rgba(0, 0, 0, 0.12)',
                      paddingTop: '0px !important',
                      borderRadius: '10px',
                      height: '90%'
                  }}>

                <Box
                    ref={messageRef}
                    className="message-container"
                    sx={{
                        height: '83%'
                    }}>

                    {
                        chatCtx?.messages.length === 0 ? (
                            <Paper sx={{
                                p: 3, boxShadow: 'none', display: 'flex', flexDirection: 'column', alignItems: 'center'
                            }}>
                                <Avatar sx={{m: 1, bgcolor: 'white'}}>
                                    <Groups2Icon color={'info'} sx={{fontSize: '45px'}} />
                                </Avatar>

                                <ChatDivider width="62%"/>

                                <Typography variant={'h5'} color={'#3759d5'}>
                                    Нет сообщений
                                </Typography>
                                <Typography sx={{pt: 2}} variant={'subtitle1'} color={'#3759d5'} textAlign={'center'}>
                                    Начните обсуждение прямо сейчас!
                                </Typography>
                            </Paper>
                        ) : (
                            chatCtx?.messages.map((message, idx) => (
                                <MessageItem key={idx} {...message} />
                            ))
                        )
                    }
                </Box>

                <Divider sx={{marginRight: '16px'}}/>

                <SendMessage onSendMessage={chatCtx?.sendMessage ??
                    ((message: string) => {
                    })}/>

            </Grid>

        </Grid>
    )
}

export default ChatRoom