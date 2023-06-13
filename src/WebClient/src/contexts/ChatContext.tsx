import React, {createContext, useContext, useEffect, useState} from "react";
import {Message, NamedMessage, Room, User} from "../models/_index";
import {Outlet, useLocation} from "react-router-dom";
import {ConnectContext, NotificationContext, StoreContext} from "./_index";
import {observer} from "mobx-react-lite";
import axios from "axios";
import constants from "../constants";

interface IChatContext {
    messages: NamedMessage[]
    connectedUsers: User[]

    sendMessage: (message: string) => void
}

export const ChatContext = createContext<IChatContext | null>(null)

export const ChatContextProvider = observer(() => {
    const connect = useContext(ConnectContext)
    const notification = useContext(NotificationContext)
    const store = useContext(StoreContext)

    const location = useLocation();
    const chatInfo: Room | undefined = JSON.parse(location?.state)

    const [messages, setMessages] = useState<NamedMessage[]>([]);
    const [connectedUsers, setConnectedUsers] = useState<User[]>([]);

    useEffect(() => {
        async function fetchChatData() {
            const m = await axios.get<{ messages: NamedMessage[], logins: string[] }>(
                constants.API_URL + "/messages?ChatRoomId=" + chatInfo?.id, {
                    headers: {
                        Authorization: `Bearer ${store?.mobxStore.token}`
                    }
                })
                .then(r => {
                    r.data.messages.forEach((msg, idx) => msg.userName = r.data.logins[idx])
                    return r.data.messages
                })
                .catch(() => notification?.showMessage({
                    message: "Не удалось загрузить сообщения",
                    status: "error",
                    duration: 3000
                }))
            if (m)
                setMessages(m)
            console.log(m, 'все сообщения')
            const u = await axios.get<User[]>(constants.API_URL + "/users?chatRoomId=" + chatInfo?.id, {
                headers: {
                    Authorization: `Bearer ${store?.mobxStore.token}`
                }
            }).then(r => r.data)
                .catch(() => notification?.showMessage({
                    message: "Не удалось загрузить список пользователей",
                    status: "error",
                    duration: 3000
                }))
            if (u)
                setConnectedUsers(u)
        }

        fetchChatData().then()
    }, [])

    function sendMessage(message: string) {
        connect?.connection?.invoke("SendTextMessage", {
            chatRoomId: chatInfo?.id,
            text: message
        })
            .catch((e) => {
                console.log(e)
                notification?.showMessage({
                    message: "Не удалось отправить сообщение",
                    duration: 3000,
                    status: "error"
                })
            })
    }

    connect?.connection?.on("SendJoinedUserToChatRoom", (user: { user: User }) => {
        setConnectedUsers([...connectedUsers, user.user])
        notification?.showMessage({
            message: `${user.user.name} вошел в чат`,
            status: "info",
            duration: 3000
        })
    })
    connect?.connection?.on("SendExitedUserFromChatRoom", (user: { userId: string }) => {
        const userDel = connectedUsers.find(u => u.id == user.userId)
        setConnectedUsers(connectedUsers.filter(u => u.id === user.userId))
        notification?.showMessage({
            message: `${userDel?.name ?? "АНОНИМУС"} вышел из чата`,
            status: "info",
            duration: 3000
        })
    })

    connect?.connection?.on("SendExitFromChatRoom", (room: { chatRoomId: number }) => {
        const title = store?.mobxStore.myChats.find(c => c.id == room.chatRoomId)
        store?.mobxStore.setChats(store?.mobxStore.myChats.filter(c => c.id == room.chatRoomId))
        notification?.showMessage({
            message: `Вы вышли из чата ${title}`,
            status: "info",
            duration: 3000
        })
    })
    connect?.connection?.on("SendTextMessage", (msg: { textMessage: NamedMessage, userName: string }) => {
        msg.textMessage.userName = msg.userName
        setMessages([...messages, msg.textMessage])
    })

    return (
        <ChatContext.Provider value={{messages, connectedUsers, sendMessage}}>
            <Outlet/>
        </ChatContext.Provider>
    );
})