import { TicTacToePlayer } from "./TicTacToePlayer";

export class TicTacToeGameState{
    constructor(){    }
    
    hasGameBeenSetup: boolean;
    board:string[];
    gameIsPlayable: boolean;
    isPlayer1Turn: boolean;
    player1: TicTacToePlayer;
    player2: TicTacToePlayer;
}