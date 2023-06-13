import React, { useContext } from 'react';
import { Message } from '../../../models/Message';
import { Typography, Grid } from '@mui/material';
import { StoreContext } from '../../../contexts/StoreContext'

const MessageItem = (msg: Message) => {

    const store = useContext(StoreContext);
    const sent: boolean = store?.mobxStore?.user?.sub == msg.userId

    const classes = `msg ${sent ? 'sent' : 'received'}`

    return (
        <Grid container direction="column" sx={{ alignItems: sent ? 'flex-end' : 'flex-start' }}>
            <Typography className={classes}>
                <Typography color={sent ? '#cedcff' : '#3759d5'} variant='body2'>{msg.name}</Typography>
                {msg.text}
            </Typography>
        </Grid>
    )
}

export default MessageItem