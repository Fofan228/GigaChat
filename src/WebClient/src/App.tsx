import React from "react"
import {Route, Routes} from "react-router-dom"
import IndexContainer from "./pages/AppContainer/IndexContainer"
import Menu from "./pages/Menu/Menu"
import {AuthProtect} from "./route-protects/_index"
import {Login} from "./pages/_index"
import NotFound from "./pages/Errors/NotFound"
import {RoomsLayer} from "./logic-layers/RoomsLayer"
import {ChatContextProvider, ConnectContextProvider} from "./contexts/_index"
import ChatRoom from "./pages/ChatRoom/ChatRoom"

const App = () => {
    return (
        <Routes>
            <Route path={'/'} element={<IndexContainer/>}>
                <Route path={'auth'} element={<Login/>}/>

                <Route element={<AuthProtect/>}>
                    <Route element={<ConnectContextProvider />}>
                        <Route element={<RoomsLayer/>}>
                            <Route index element={<Menu/>}/>
                        </Route>
                        <Route element={<ChatContextProvider/>}>
                            <Route path={'chat'} element={<ChatRoom/>}/>
                        </Route>
                    </Route>
                </Route>

                <Route path={'*'} element={<NotFound/>}/>
            </Route>
        </Routes>
    )
};

export default App