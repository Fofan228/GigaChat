import React, {useContext, useState} from 'react';
import {NamedMessage} from '../../../models/Message';
import {
    Typography,
    Grid,
    Tooltip,
    Box,
    Paper,
    ListItem,
    ListItemIcon,
    Avatar,
    ListItemText,
    List,
    Input, Button
} from '@mui/material';
import {StoreContext} from '../../../contexts/StoreContext'
import {observer} from "mobx-react-lite";
import EditIcon from '@mui/icons-material/Edit';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import {ConnectContext} from "../../../contexts/ConnectContext";
import {NotificationContext} from "../../../contexts/NotificationContext";
import SendIcon from "@mui/icons-material/Send";

const MessageItem = observer((msg: NamedMessage) => {

    const store = useContext(StoreContext);
    const sent: boolean = store?.mobxStore?.user?.id == msg.userId
    const connect = useContext(ConnectContext)
    const notification = useContext(NotificationContext)
    const [edit, setEdit] = useState(false)
    const [editText, setEditText] = useState("")

    const classes = `msg ${sent ? 'sent' : 'received'}`

    function editHandler(newText: string) {
        console.log('я тута')
        connect?.connection?.invoke("EditTextMessage", {
            textMessageId: msg.id,
            text: newText
        })
            .then(r => {
                console.log(r)
            })
            .catch(e => {
            console.log(e)
            notification?.showMessage({
                message: "Не удалось изменить сообщение",
                duration: 4000,
                status: "error"
            })
        })
    }

    function deleteHandler() {
        connect?.connection?.invoke("DeleteMessage", {
            messageId: msg.id,
        }).catch(e => {
            console.log(e)
            notification?.showMessage({
                message: "Не удалось удалить сообщение",
                duration: 4000,
                status: "error"
            })
        })
    }

    return (
        <Grid container direction="column" sx={{alignItems: sent ? 'flex-end' : 'flex-start'}}>
            <Tooltip disableFocusListener
                     disableTouchListener
                     title={
                sent && (
                    <Paper>
                        <List>
                            <ListItem sx={{cursor: "pointer"}} onClick={() => {
                                setEdit(true)
                                setEditText(msg.text)
                            }}>
                                {
                                    edit ? (
                                        <Paper>
                                            <Input value={editText} onChange={e => setEditText(e.target.value)}/>
                                            <Button variant="contained" onClick={() => {
                                                editHandler(editText)
                                                setEdit(false)
                                                setEditText("")
                                            }}>
                                                <SendIcon/>
                                            </Button>

                                        </Paper>
                                    ) : (
                                        <>
                                            <ListItemIcon>
                                                <EditIcon/>
                                            </ListItemIcon>
                                            <ListItemText primary={'Изменить сообщение'}/>
                                        </>
                                    )
                                }
                            </ListItem>
                            <ListItem sx={{cursor: "pointer"}} onClick={() => {
                                setEdit(false)
                                deleteHandler()
                            }}>
                                <ListItemIcon>
                                    <DeleteForeverIcon/>
                                </ListItemIcon>
                                <ListItemText primary={'Удалить сообщение'}/>
                            </ListItem>
                        </List>
                    </Paper>
                )
            } onClose={() => {
                setEdit(false)
                setEditText("")
            }}>
                <Typography className={classes}>
                    <Typography component={'span'} color={sent ? '#cedcff' : '#3759d5'}
                                sx={{display: "block"}} variant='body2'>{msg.userName}</Typography>
                    {msg.text}
                </Typography>
            </Tooltip>
        </Grid>
    )
})

export default MessageItem