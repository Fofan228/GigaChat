import React, {createContext, useEffect, useContext} from "react";
import { Room } from "../models/_index";
import {ConnectContext, NotificationContext, StoreContext} from "../contexts/_index";
import {Outlet} from "react-router-dom";
import {observer} from "mobx-react-lite";

export const RoomsLayer = observer(() => {
    const connectionCtx = useContext(ConnectContext)
    const notificationCtx = useContext(NotificationContext)

    const store = useContext(StoreContext)

    useEffect(() => {
        if (connectionCtx?.connectionStarted && connectionCtx.connection) {
            const connect = connectionCtx.connection

            connect.on('SendError', (err: string) => {
                notificationCtx?.showMessage({
                    message: err,
                    duration: 4000,
                    status: "error"
                })
            })

            connect.on('SendUserChatRooms', (rooms: {rooms: Room[]}) => {
                store?.mobxStore.setChats(rooms.rooms)
            })

            connect.on('SendInviteToChatRoom', (chatRoom: {chatRoom: Room}) => {
                notificationCtx?.showMessage({
                    message: `Вас добавили в чат '${chatRoom.chatRoom.title}'`,
                    duration: 4000,
                    status: "info"
                })
                store?.mobxStore.setChats([chatRoom.chatRoom, ...store?.mobxStore.myChats])
            })

            connect.on('SendCloseChatRoom', (kickRoom: Room) => {
                notificationCtx?.showMessage({
                    message: `Вас кикнули из чата '${kickRoom.title}'`,
                    duration: 4000,
                    status: "error"
                })
                store?.mobxStore.setChats(store?.mobxStore.myChats.filter(r => r.id !== kickRoom.id))
            })
        }
    }, [connectionCtx?.connection])
    return (
        <Outlet />
    );
})