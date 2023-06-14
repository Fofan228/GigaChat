import axios from "axios";
import constants from "../constants";
import {Token, User} from "../models/_index";
import React from "react";

function parseJwt (token: string) {
    if (token) {
        const base64Url = token.split('.')[1];
        if (base64Url) {
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));
            const user = JSON.parse(jsonPayload) as User;
            //Перемещаю и удаляю поле sub
            // @ts-ignore
            user.id = user.sub
            // @ts-ignore
            delete user.sub
            // @ts-ignore
            user.login = user.nickname
            // @ts-ignore
            delete user.nickname
            return user
        }
    }
}

const TOKEN_KEY = 'token'

export function getToken() {
    return sessionStorage.getItem(TOKEN_KEY) || ""
}

export async function registration(name: string, login: string, password: string, errorHandler: (err: React.ReactNode) => void) {
    return await axios.post<Token>(constants.API_URL + constants.AUTH_URL + '/register', {
        name,
        login,
        password
    })
        .then(r => {
            sessionStorage.setItem(TOKEN_KEY, r.data.token)
            return parseJwt(r.data.token)
        })
        .catch(e => {
            console.log(e.response.data.errors, e, 'ошибки')
            const errors = e.response?.data?.errors ? (
                <div>
                    {e.response.data.errors.Login}
                    <br />
                    {e.response.data.errors.Password}
                    <br />
                    {e.response.data.errors.Name}

                </div>
            ) : (
                <div>
                    {e?.response?.data?.title}
                </div>
            )
            console.log(errors)
            errorHandler(errors)
        })
}

export async function login(login: string, password: string) {
    return await axios.post<Token>(constants.API_URL + constants.AUTH_URL + '/login', {
        login,
        password
    })
        .then(r => {
            sessionStorage.setItem(TOKEN_KEY, r.data.token)
            return  parseJwt(r.data.token)
        })
        .catch(e => {
            console.log(e)
        })
}

export function logout() {
    sessionStorage.removeItem(TOKEN_KEY)
}

export function getUser() {
    const user = sessionStorage.getItem(TOKEN_KEY)
    console.log(parseJwt(user || "нету") )
    return user ? parseJwt(user) : null;
}