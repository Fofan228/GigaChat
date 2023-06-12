import { createContext, ReactNode, useState } from "react"
import {Alert, AlertColor, Snackbar} from "@mui/material";

interface IMessage {
    message: string,
    status: AlertColor,
    duration: number
}
interface INotificationContext {
    showMessage: (message: IMessage) => void
}

export const NotificationContext = createContext<INotificationContext | null>(null)

export const NotificationProvider = ({ children }: { children: ReactNode }) => {
    const [openNotice, setOpenNotice] = useState(false)
    const [notice, setNotice] = useState<IMessage>({
        status: "success",
        duration: 1,
        message: ""
    })

    function showMessage(message: IMessage) {
        setNotice(message)
        setOpenNotice(true)
    }

    return (
        <>
            <NotificationContext.Provider value={{showMessage}}>
                <Snackbar open={openNotice}
                          autoHideDuration={notice.duration}
                          onClose={() => setOpenNotice(false)}
                >
                    <Alert severity={notice.status}>
                        {notice.message}
                    </Alert>
                </Snackbar>
                {children}
            </NotificationContext.Provider>
        </>
    )

}