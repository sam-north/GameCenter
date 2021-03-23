import { MancalaPlayer } from "./MancalaPlayer";

export class MancalaGameState{
    constructor(){    }
    
    hasGameBeenSetup: boolean;
    gameIsPlayable: boolean;
    isPlayer1Turn: boolean;
    player1: MancalaPlayer;
    player2: MancalaPlayer;
}