import React, {useContext} from "react"
import {Route, Routes, useNavigate} from "react-router-dom"
import IndexContainer from "./pages/AppContainer/IndexContainer"
import Menu from "./pages/Menu/Menu";
import {AuthProtect} from "./route-protects/_index"
import {Login} from "./pages/_index";
import NotFound from "./pages/Empty/NotFound";
import NotAccess from "./pages/Empty/NotAccess";
import {UserContext} from "./contexts/UserContext";

const App = () => {
    const nav = useNavigate()
    const userContext = useContext(UserContext)
    if (userContext && userContext.user == null)
        nav('/auth')

    return (
        <>
            <Routes>
                <Route path={'/'} element={<IndexContainer />}>
                    <Route path={'auth'} element={<Login />} />

                    <Route element={<AuthProtect />}>
                        <Route index element={<Menu />}/>
                        <Route path={'chat'} element={<NotAccess />}/>
                    </Route>

                    <Route path={'*'} element={<NotFound />} />
                </Route>
            </Routes>
        </>
    )
};

export default App