import { GameInstanceStateDto } from "./GameInstanceStateDto";
import { GameInstanceUserDto } from "./GameInstanceUserDto";

export class GameInstanceDto {
    constructor(){
    }
    id: number;
    gameId: number;
    dateCreated: Date;
    gameDisplayName: string;

    state: GameInstanceStateDto;
    users: GameInstanceUserDto[];
}