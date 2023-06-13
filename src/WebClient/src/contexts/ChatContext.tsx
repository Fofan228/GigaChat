import React, {createContext, useState} from "react";
import { Message, User } from "../models/_index";
import {Outlet} from "react-router-dom";
import {useNavigate} from "react-router-dom";

interface IChatContext {
    messages: Message[]
    connectedUsers: User[]

    sendMessage: (message: string) => void
}


export const ChatContext = createContext<IChatContext | null>(null)

export const ChatContextProvider = () => {
    const [messages, setMessages] = useState<Message[]>([
        {
            id: 0,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "sa",
            chatRoomId: "s",
            name: "Гигачад Мудрый"
        },
        {
            id: 2,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "sa",
            chatRoomId: "s",
            name: "Гигачад Мудрый 2"
        },
        {
            id: 1,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "d2d39e87-7ef2-4d45-a895-09398fc02182",
            chatRoomId: "s",
            name: "Гигачад Мудрый 1"
        },
        {
            id: 0,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "sa",
            chatRoomId: "s",
            name: "Гигачад Мудрый"
        },
        {
            id: 2,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "sa",
            chatRoomId: "s",
            name: "Гигачад Мудрый 2"
        },
        {
            id: 1,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "d2d39e87-7ef2-4d45-a895-09398fc02182",
            chatRoomId: "s",
            name: "Гигачад Мудрый 1"
        },
        {
            id: 0,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "sa",
            chatRoomId: "s",
            name: "Гигачад Мудрый"
        },
        {
            id: 2,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "sa",
            chatRoomId: "s",
            name: "Гигачад Мудрый 2"
        },
        {
            id: 1,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "d2d39e87-7ef2-4d45-a895-09398fc02182",
            chatRoomId: "s",
            name: "Гигачад Мудрый 1"
        },
        {
            id: 0,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "sa",
            chatRoomId: "s",
            name: "Гигачад Мудрый"
        },
        {
            id: 2,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "sa",
            chatRoomId: "s",
            name: "Гигачад Мудрый 2"
        },
        {
            id: 1,
            text: "asdn LNASDLnasldn lkasm ASND lkas ndlkasm dlkasml kaslkdm laskmf asmfl kasmd ",
            userId: "d2d39e87-7ef2-4d45-a895-09398fc02182",
            chatRoomId: "s",
            name: "Гигачад Мудрый 1"
        },
    ]);
    const [connectedUsers, setConnectedUsers] = useState<User[]>([
        {
            nickname: "Гигачад Мудрый 1",
            name: "Мудрый Гигачад 2",
            sub: "22"
        },
        {
            nickname: "Гигачад Мудрый 2",
            name: "Гигачад Мудрый 2",
            sub: "22"
        },
        {
            nickname: "Дерево Мудрое",
            name: "Гига",
            sub: "22"
        }
    ]);

    function sendMessage(message: string) {
        console.log("не реализовано", message)
    }

    return (
        <ChatContext.Provider value={{ messages, connectedUsers, sendMessage }}>
            <Outlet />
        </ChatContext.Provider>
    );
}