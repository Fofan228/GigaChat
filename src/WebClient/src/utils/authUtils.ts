import axios from "axios";
import constants from "../constants";
import {Token, User} from "../models/_index";

function parseJwt (token: string) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload) as User;
}

const TOKEN_KEY = 'token'

export async function registration(name: string, login: string, password: string) {
    return await axios.post<Token>(constants.API_URL + constants.AUTH_URL + '/register', {
        name,
        login,
        password
    })
        .then(r => {
            localStorage.setItem(TOKEN_KEY, r.data.token)
            return  parseJwt(r.data.token)
        })
        .catch(e => {
            console.log(e)
        })
}

export async function login(login: string, password: string) {
    return await axios.post<Token>(constants.API_URL + constants.AUTH_URL + '/login', {
        login,
        password
    })
        .then(r => {
            localStorage.setItem(TOKEN_KEY, r.data.token)
            return  parseJwt(r.data.token)
        })
        .catch(e => {
            console.log(e)
        })
}

export function logout() {
    localStorage.removeItem(TOKEN_KEY)
}

export function getUser() {
    const user = localStorage.getItem(TOKEN_KEY)
    return user ? parseJwt(user) : null;
}