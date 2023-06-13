import React, { useContext } from 'react';
import {NamedMessage} from '../../../models/Message';
import { Typography, Grid } from '@mui/material';
import { StoreContext } from '../../../contexts/StoreContext'
import {observer} from "mobx-react-lite";

const MessageItem = observer((msg: NamedMessage) => {

    const store = useContext(StoreContext);
    const sent: boolean = store?.mobxStore?.user?.id == msg.userId

    const classes = `msg ${sent ? 'sent' : 'received'}`

    return (
        <Grid container direction="column" sx={{ alignItems: sent ? 'flex-end' : 'flex-start' }}>
            <Typography className={classes}>
                <Typography component={'span'} color={sent ? '#cedcff' : '#3759d5'}
                            sx={{display: "block"}} variant='body2'>{msg.userName}</Typography>
                {msg.text}
            </Typography>
        </Grid>
    )
})

export default MessageItem