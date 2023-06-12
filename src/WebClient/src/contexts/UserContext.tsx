import {createContext, ReactNode, useEffect, useState} from "react";
import {User} from "../models/User";
import {getToken, getUser} from "../utils/authUtils";

interface IUserContext {
    user: User | null
    setUser: (user: User | null) => void;
    token: string
}

export const UserContext = createContext<IUserContext | null>(null)

export const UserContextProvider = ({ children }: { children: ReactNode }) => {
    const [user, setUser] = useState(getUser());
    const [token, setToken] = useState(getToken())

    useEffect(() => {
        setToken(getToken())
    }, [user])

    return (
        <UserContext.Provider value={{ user, setUser, token }}>
            {children}
        </UserContext.Provider>
    );
}