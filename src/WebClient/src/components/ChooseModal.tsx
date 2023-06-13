import React, {useContext, useEffect, useState} from 'react';
import {
    Avatar,
    Box, Button,
    Checkbox, CircularProgress, Input,
    List,
    ListItem,
    ListItemAvatar,
    ListItemButton,
    ListItemText,
    Modal, Typography
} from "@mui/material";
import usernameToAvatar from "../utils/usernameToAvatar";
import {User, UserId} from "../models/User";
import axios from "axios";
import constants from "../constants";
import {ConnectContext, NotificationContext, StoreContext} from "../contexts/_index";
import SendIcon from "@mui/icons-material/Send";

interface IChooseUsersModal {
    open: boolean,
    setOpen: (open: boolean) => void
}

const ChooseModal = ({open, setOpen}: IChooseUsersModal) => {

    const style = {
        position: 'absolute' as 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: 400,
        bgcolor: 'background.paper',
        boxShadow: 24,
        borderRadius: 3,
        border: 'none',
        p: 4,
    };

    const [checked, setChecked] = React.useState([] as string[]);
    const [title, setTitle] = useState("")
    const [allUsers, setAllUsers] = useState<null | UserId[]>(null)
    const store = useContext(StoreContext)
    const connect = useContext(ConnectContext)
    const notification = useContext(NotificationContext)

    useEffect(() => {
        async function fetchUsers() {
            setAllUsers(await axios.get<UserId[]>(constants.API_URL + "/users", {
                headers: {
                    Authorization: `Bearer ${store?.mobxStore.token}`
                }
            }).then(r => r.data).catch(() => [] as UserId[]))
        }

        fetchUsers().then()
    }, [])

    const handleToggle = (userId: string) => () => {
        const currentIndex = checked.indexOf(userId);
        const newChecked = [...checked];

        if (currentIndex === -1) {
            newChecked.push(userId);
        } else {
            newChecked.splice(currentIndex, 1);
        }

        setChecked(newChecked);
    };

    return (
        <Modal
            open={open}
            onClose={() => setOpen(false)}
        >
            <Box sx={{...style, width: 400}}>
                {
                    allUsers == null ? (
                        <CircularProgress/>
                    ) : allUsers.length === 0 ? (
                        <Typography>Список пользователей пуст</Typography>
                    ) : (
                        <>
                            <List dense sx={{width: '100%'}}>
                                {allUsers.map((u, idx) => {
                                    const labelId = `checkbox-list-secondary-label-${u.id}`;
                                    return (
                                        <ListItem
                                            key={idx}
                                            secondaryAction={
                                                <Checkbox
                                                    edge="end"
                                                    onChange={handleToggle(u.id)}
                                                    checked={checked.indexOf(u.id) !== -1}
                                                    inputProps={{'aria-labelledby': labelId}}
                                                />
                                            }
                                            disablePadding
                                        >
                                            <ListItemButton>
                                                <ListItemAvatar>
                                                    <Avatar {...usernameToAvatar(u.name)}/>
                                                </ListItemAvatar>
                                                <ListItemText id={labelId} color={'black'}>
                                                    {u.name}
                                                </ListItemText>
                                            </ListItemButton>
                                        </ListItem>
                                    );
                                })}
                            </List>
                            <Box sx={{display: 'flex', flexDirection: "column", gap: "6px"}}>
                                <Input value={title} placeholder={'Название чата'}
                                       onChange={e => setTitle(e.target.value)}/>

                                <Button variant="contained" endIcon={<SendIcon/>} onClick={() => {
                                    connect?.connection?.invoke("OpenChatRoom", {
                                        title,
                                        userIds: checked
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

                                    setChecked([])
                                    setTitle("")
                                    setOpen(false)
                                }}>
                                    Создать
                                </Button>
                            </Box>

                        </>
                    )
                }
            </Box>
        </Modal>
    );
};

export default ChooseModal;