import React, { useState } from 'react'
import { Grid, TextField, Button } from '@mui/material';
import SendIcon from '@mui/icons-material/Send';

interface SendMessageProps {
    onSendMessage: (message: string) => void
}

const SendMessage = ({onSendMessage}: SendMessageProps) => {

    const [message, setMessage] = useState("")

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        onSendMessage(message);
        setMessage("")
    }

    return (
        <Grid container
            sx={{ padding: '20px' }}
            component="form"
            onSubmit={handleSubmit}
        >
            <Grid item xs={10}>
                <TextField
                    multiline maxRows={4} required
                    label="Сообщение" fullWidth
                    onChange={e => setMessage(e.target.value)}
                    value={message} />
            </Grid>
            <Grid item xs={2} sx={{ display: 'flex', justifyContent: "center", alignItems: 'center' }}>
                <Button variant="contained" endIcon={<SendIcon />} type='submit'>
                    Go
                </Button>
            </Grid>
        </Grid>
    )
}

export default SendMessage