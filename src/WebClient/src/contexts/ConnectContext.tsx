import { HubConnection } from "@microsoft/signalr"
import {createContext, ReactNode, useState, useEffect, useContext} from "react"
import { Message, User } from "../models/_index"
import { buildConnection, startConnection } from '../utils/hubUtils'
import {UserContext} from "./_index";

interface IConnectContext {
    connection?: HubConnection
    connectionStarted: boolean
}

export const ConnectContext = createContext<IConnectContext | null>(null)

export const ConnectContextProvider = ({ children }: { children: ReactNode }) => {

    const [connection, setConnection] = useState<HubConnection>()
    const [connectionStarted, setConnectionStarted] = useState(false)

    const userCtx = useContext(UserContext)

    const startNewConnection = () => {
        const newConnection = buildConnection(userCtx?.token || "")
        setConnection(newConnection)
    }

    useEffect(() => {
        startNewConnection();
    }, [])

    useEffect(() => {
        if (connection) {
            startConnection(connection)
                .then(() => {
                    setConnectionStarted(true)
                })
        }
    }, [connection])

    return (
        <ConnectContext.Provider value={{connection, connectionStarted}}>
            {children}
        </ConnectContext.Provider>
    )
}