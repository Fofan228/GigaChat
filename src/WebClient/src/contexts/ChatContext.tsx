import React, {createContext, useContext, useEffect, useState} from "react";
import {Message, NamedMessage, Room, User} from "../models/_index";
import {Outlet, useLocation, useNavigate} from "react-router-dom";
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
    const nav = useNavigate()

    const location = useLocation();
    const chatInfo: Room | undefined = JSON.parse(location?.state)

    const [messages, setMessages] = useState<NamedMessage[]>([]);
    const [connectedUsers, setConnectedUsers] = useState<User[]>([]);

    useEffect(() => {
        async function fetchChatData() {
            const u = await axios.get<{users: User[]}>(constants.API_URL + "/users/" + chatInfo?.id, {
                headers: {
                    Authorization: `Bearer ${store?.mobxStore.token}`
                }
            }).then(r => r.data.users)
                .catch(() => notification?.showMessage({
                    message: "Не удалось загрузить список пользователей",
                    status: "error",
                    duration: 3000
                }))
            if (u) {
                setConnectedUsers(u)
                const m = await axios.get<{ messages: NamedMessage[], logins: string[] }>(
                    constants.API_URL + "/messages?ChatRoomId=" + chatInfo?.id, {
                        headers: {
                            Authorization: `Bearer ${store?.mobxStore.token}`
                        }
                    })
                    .then(r => {
                        console.log(r.data, 'ВСЕ СООБЩЕНИЯ')
                        r.data.messages.forEach(msg => msg.userName = u.find(e => e.id === msg.userId)?.login || "Ошибка")
                        return r.data.messages
                    })
                    .catch(() => notification?.showMessage({
                        message: "Не удалось загрузить сообщения",
                        status: "error",
                        duration: 3000
                    }))
                if (m)
                    setMessages(m)
            }
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
        setConnectedUsers(connectedUsers.filter(u => u.id !== user.userId))
        notification?.showMessage({
            message: `${userDel?.name ?? "АНОНИМУС"} вышел из чата`,
            status: "info",
            duration: 3000
        })
    })

    connect?.connection?.on("SendExitFromChatRoom", (room: { chatRoomId: number }) => {
        store?.mobxStore.setChats(store?.mobxStore.myChats.filter(c => c.id !== room.chatRoomId))
        notification?.showMessage({
            message: `Вы успешно покинули чат`,
            status: "info",
            duration: 3000
        })
        nav('/')
    })
    connect?.connection?.on("SendTextMessage", (msg: { textMessage: NamedMessage, username: string }) => {
        msg.textMessage.userName = msg.username
        setMessages([...messages, msg.textMessage])
    })
    connect?.connection?.on("SendEditTextMessage", (msg: {textMessage: Message}) => {
        setMessages(messages.map(m => {
            if (m.id === msg.textMessage.id) {
                console.log(m, msg.textMessage)
                m.text = msg.textMessage.text
            }
            return m
        }))
    })
    connect?.connection?.on("SendDeleteMessage", (msg: {message: Message}) => {
        setMessages(messages.filter(m => m.id !== msg.message.id))
    })

    return (
        <ChatContext.Provider value={{messages, connectedUsers, sendMessage}}>
            <Outlet/>
        </ChatContext.Provider>
    );
})