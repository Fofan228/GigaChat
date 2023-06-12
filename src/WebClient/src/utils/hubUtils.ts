import {HubConnectionBuilder, LogLevel} from '@microsoft/signalr';
import {HubConnection} from '@microsoft/signalr/dist/esm/HubConnection';
import constants from '../constants'
export function buildConnection(token: string): HubConnection {
    return new HubConnectionBuilder()
        .withUrl(constants.API_URL, { accessTokenFactory: () => token })
        .withAutomaticReconnect()
        .configureLogging(LogLevel.Debug)
        .build();
}
export async function startConnection(connection: HubConnection): Promise<void> {
    try {
        await connection.start();
        console.log('Connected!');
    } catch (e) {
        console.log('Connection failed: ', e)
    }
}