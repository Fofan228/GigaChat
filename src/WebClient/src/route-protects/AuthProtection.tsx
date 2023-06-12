import {Navigate, Outlet} from "react-router-dom";
import {useContext} from "react";
import {UserContext} from "../contexts/_index";

const AuthProtect = () => {
    const userContext = useContext(UserContext)
    if (userContext && userContext.user)
        return <Outlet />
    return <Navigate to={'/auth'} />
};

export default AuthProtect;