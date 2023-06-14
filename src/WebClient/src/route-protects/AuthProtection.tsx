import {Navigate, Outlet} from "react-router-dom";
import React, {useContext} from "react";
import {StoreContext} from "../contexts/_index";

const AuthProtect = () => {
    const store = useContext(StoreContext)
    if (store && store.mobxStore.user)
        return <Outlet />
    return <Navigate to={'/auth'} />
};

export default AuthProtect;