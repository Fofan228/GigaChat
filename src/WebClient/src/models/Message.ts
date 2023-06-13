export interface Message {
    id: number
    text: string
    chatRoomId: string
    userId: string
}

export interface NamedMessage extends Message {
    userName: string
}