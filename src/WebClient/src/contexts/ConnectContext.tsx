import { HubConnection } from "@microsoft/signalr"
import React, {createContext, ReactNode, useState, useEffect, useContext} from "react"
import { buildConnection, startConnection } from '../utils/hubUtils'
import {StoreContext} from "./_index";

interface IConnectContext {
    connection?: HubConnection
    connectionStarted: boolean
}

export const ConnectContext = createContext<IConnectContext | null>(null)

export const ConnectContextProvider = ({ children }: { children: ReactNode }) => {

    const [connection, setConnection] = useState<HubConnection>()
    const [connectionStarted, setConnectionStarted] = useState(false)

    const store = useContext(StoreContext)

    const startNewConnection = () => {
        const newConnection = buildConnection(store?.mobxStore.token || "")
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