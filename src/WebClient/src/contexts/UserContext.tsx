import { createContext, ReactNode, useState } from "react";
import {User} from "../models/User";
import {getUser} from "../utils/authUtils";

interface IUserContext {
    user: User | null
    setUser: (user: User | null) => void;
}

export const UserContext = createContext<IUserContext | null>(null)

export const UserContextProvider = ({ children }: { children: ReactNode }) => {
    const [user, setUser] = useState(getUser());
    return (
        <UserContext.Provider value={{ user, setUser }}>
            {children}
        </UserContext.Provider>
    );
}