import React from "react"
import {Route, Routes } from "react-router-dom"
import IndexContainer from "./pages/AppContainer/IndexContainer"
import Menu from "./pages/Menu/Menu";
import {AuthProtect} from "./route-protects/_index"
import {Login} from "./pages/_index";
import NotFound from "./pages/Empty/NotFound";
import NotAccess from "./pages/Empty/NotAccess";
import NoAuthProtection from "./route-protects/NoAuthProtection";

const App = () => {
    return (
        <>
            <Routes>
                <Route path={'/'} element={<IndexContainer />}>

                    <Route element={<NoAuthProtection />}>
                        <Route path={'auth'} element={<Login />} />
                    </Route>

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