import { GameInstanceUserDto } from "./GameInstanceUserDto";

export class UserGameInstanceStatelessDto {
    constructor(){
    }
    id: number;
    gameId: number;
    dateCreated: Date;
    gameDisplayName: string;
    users: GameInstanceUserDto[];
}