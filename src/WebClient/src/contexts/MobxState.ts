import {makeAutoObservable} from "mobx";
import {Room, User} from "../models/_index";
import {getToken, getUser} from "../utils/authUtils";

export default class MobxState {
    constructor() {
        makeAutoObservable(this)
    }

    myChats: Room[] = [
        {
            id: 1,
            title: "Комната 1"
        },
        {
            id: 2,
            title: "Бизнес холл"
        }
    ]

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