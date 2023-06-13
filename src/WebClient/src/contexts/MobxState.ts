import {makeAutoObservable} from "mobx";
import {Room} from "../models/_index";
import {getToken, getUser} from "../utils/authUtils";

export default class MobxState {
    constructor() {
        makeAutoObservable(this)
    }

    myChats: Room[] = []

    setChats(chats: Room[]) {
        this.myChats = chats
    }

    user = getUser()

    refreshUser() {
        this.user = getUser()
    }

    token = getToken()

    refreshToken() {
        this.token = getToken()
    }
}