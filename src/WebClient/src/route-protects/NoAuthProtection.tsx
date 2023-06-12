import {Navigate, Outlet} from "react-router-dom";
import {useContext} from "react";
import {UserContext} from "../contexts/_index";

const NoAuthProtect = () => {
    const userContext = useContext(UserContext)
    if (userContext && userContext.user == null)
        return <Outlet />
    return <Navigate to={'/'} />
};

export default NoAuthProtect;