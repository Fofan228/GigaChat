import { Grid, List, ListItem, ListItemIcon, ListItemText, Avatar } from '@mui/material';
import usernameToAvatar from '../../../utils/usernameToAvatar';
import { ChatContext } from '../../../contexts/_index';
import React, { useContext } from 'react';

const Users = () => {

    const chatCtx = useContext(ChatContext);

    return (
        <Grid item md={3} sx={{
            overflowY: 'auto',
            display: { xs: "none", sm: "block" }
        }}>
            <List>

                {chatCtx?.connectedUsers.map((user, index) => (
                    <ListItem key={`user_${index}`}>
                        <ListItemIcon>
                            <Avatar {...usernameToAvatar(user.name)} />
                        </ListItemIcon>
                        <ListItemText primary={user.name}>{user.name}</ListItemText>
                    </ListItem>
                ))}
            </List>
        </Grid>
    )
}

export default Users