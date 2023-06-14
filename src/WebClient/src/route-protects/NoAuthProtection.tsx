import {Navigate, Outlet} from "react-router-dom";
import React, {useContext} from "react";
import {StoreContext} from "../contexts/_index";

const NoAuthProtect = () => {
    const userContext = useContext(StoreContext)
    if (userContext && userContext.mobxStore.user == null)
        return <Outlet />
    return <Navigate to={'/'} />
};

export default NoAuthProtect;