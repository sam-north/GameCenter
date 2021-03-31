export class GameInstanceUserMessageDto {
    constructor(){
    }
    id: number;
    userId: number;
    userEmail: string;
    text: string;
    dateCreated: Date;
}