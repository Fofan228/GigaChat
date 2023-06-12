import React from 'react';

const Users = () => {
    return (
        <div>
            
        </div>
    );
};

export default Users;

// import { Grid, List, ListItem, ListItemIcon, ListItemText, Avatar } from '@mui/material';
// import usernameToAvatar from '../../../utils/usernameToAvatar';
// import { ChatContext } from '../../../contexts/_index';
// import { useContext } from 'react';
//
// const Users = () => {
//
//     const hubCtx = useContext(ChatContext);
//
//     return (
//         <Grid item md={3} sx={{
//             overflowY: 'auto',
//             display: { xs: "none", sm: "block" }
//         }}>
//             <List>
//
//                 {hubCtx?.connectedUsers.map((item, index) => (
//                     <ListItem key={`user_${index}`}>
//                         <ListItemIcon>
//                             <Avatar {...usernameToAvatar(item)} />
//                         </ListItemIcon>
//                         <ListItemText primary={item}>{item}</ListItemText>
//                     </ListItem>
//                 ))}
//
//             </List>
//         </Grid>
//     )
// }
//
// export default Users