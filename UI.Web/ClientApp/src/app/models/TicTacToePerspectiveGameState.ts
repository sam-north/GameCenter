import { TicTacToePlayer } from "./TicTacToePlayer";

export class TicTacToePerspectiveGameState{
    constructor(){    }
    
    hasGameBeenSetup: boolean;
    gameIsPlayable: boolean;
    board: string[];
    isCurrentPlayerTurn: boolean;
    currentPlayer: TicTacToePlayer;
    opposingPlayer: TicTacToePlayer;
}