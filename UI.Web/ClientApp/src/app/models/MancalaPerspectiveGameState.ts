import { MancalaPlayer } from "./MancalaPlayer";

export class MancalaPerspectiveGameState{
    constructor(){    }
    
    hasGameBeenSetup: boolean;
    gameIsPlayable: boolean;
    isCurrentPlayerTurn: boolean;
    currentPlayer: MancalaPlayer;
    opposingPlayer: MancalaPlayer;
}