import { createContext, ReactNode, useState, useEffect } from "react";
import { Message, User } from "../models/_index";

interface IChatContext {
    messages: Message[]
    connectedUsers: User[]
}

export const ChatContext = createContext<IChatContext | null>(null)

export const ChatContextProvider = ({ children }: { children: ReactNode }) => {

    const [messages, setMessages] = useState<Message[]>([]);
    const [connectedUsers, setConnectedUsers] = useState<User[]>([]);


    return (
        <ChatContext.Provider value={{ messages, connectedUsers }}>
            {children}
        </ChatContext.Provider>
    );
}