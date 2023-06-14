import { HubConnection } from "@microsoft/signalr"
import React, {createContext, ReactNode, useState, useEffect, useContext} from "react"
import { buildConnection, startConnection } from '../utils/hubUtils'
import {NotificationContext, StoreContext} from "./_index";
import {Room} from "../models/Room";
import {observer} from "mobx-react-lite";
import {useNavigate} from "react-router-dom";
import {Outlet} from "react-router-dom";

interface IConnectContext {
    connection?: HubConnection
    connectionStarted: boolean
}

export const ConnectContext = createContext<IConnectContext | null>(null)

export const ConnectContextProvider = observer(() => {

    const [connection, setConnection] = useState<HubConnection>()
    const [connectionStarted, setConnectionStarted] = useState(false)

    const store = useContext(StoreContext)
    const notification = useContext(NotificationContext)
    const nav = useNavigate()

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
                    connection.on("SendUserChatRooms", (rooms: {chatRooms: Room[]}) => {
                        store?.mobxStore.setChats(rooms.chatRooms)
                    })
                    connection.on("SendOpenChatRoom", (room: {chatRoom: Room}) => {
                        store?.mobxStore.setChats([...store?.mobxStore.myChats, room.chatRoom])
                        notification?.showMessage({
                            message: `Вы успешно создали комнату ${room.chatRoom.title}`,
                            status: "success",
                            duration: 5000
                        })
                        nav('/chat', {
                            state: JSON.stringify(room.chatRoom)
                        })
                    })
                    connection.on("SendInviteToChatRoom", (room: {chatRoom: Room}) => {
                        store?.mobxStore.setChats([...store?.mobxStore.myChats, room.chatRoom])
                        notification?.showMessage({
                            message: `Вас добавили в комнату ${room.chatRoom.title}`,
                            status: "success",
                            duration: 5000
                        })
                    })
                })
        }
    }, [connection])

    return (
        <ConnectContext.Provider value={{connection, connectionStarted}}>
            <Outlet />
        </ConnectContext.Provider>
    )
})