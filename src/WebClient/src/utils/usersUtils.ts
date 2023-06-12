import axios from "axios";
import {User} from "../models/_index";
import constants from "../constants";

export async function usersList() {
    return await axios.get<User[]>(constants.API_URL + constants.ROOM_URL)
        .then(r => r.data)
}