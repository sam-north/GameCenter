import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { TicTacToeGameState } from 'src/app/models/TicTacToeGameState';
import { TicTacToePerspectiveGameState } from 'src/app/models/TicTacToePerspectiveGameState';
import { GodService } from 'src/app/services/god.service';
import { isNullOrWhiteSpace } from 'src/app/utilities/StringFunctions';

@Component({
    selector: 'app-play-tic-tac-toe',
    templateUrl: './play-tic-tac-toe.component.html',
    styles: [
        `main {display: grid;
    grid-template-columns: 200px 200px 200px;}`
    ]
})

export class PlayTicTacToeComponent implements OnChanges {
    whosTurnMessage: string;
    @Input() gameState: string;
    @Output() userInputReceived: EventEmitter<any> = new EventEmitter<any>();
    perspectiveGameState: TicTacToePerspectiveGameState = new TicTacToePerspectiveGameState();

    constructor(private god: GodService) {
    }

    ngOnChanges(changes: SimpleChanges): void {
      if (changes.gameState) {
        this.translateGameStateToPerspective(changes.gameState.currentValue);
      }
    }
  
    currentPlayerSlotClick(i: number) {
      if (this.perspectiveGameState.isCurrentPlayerTurn && isNullOrWhiteSpace(this.perspectiveGameState.board[i])) 
        this.userInputReceived.emit((i).toString());
    }

    
    translateGameStateToPerspective(currentValue: string) {
        let gameState: TicTacToeGameState = JSON.parse(currentValue);
        let client = this.god.client.getClient();
        this.perspectiveGameState.board = gameState.board;
        
        this.perspectiveGameState.hasGameBeenSetup = gameState.hasGameBeenSetup;
        this.perspectiveGameState.gameIsPlayable = gameState.gameIsPlayable;
    
        if (gameState.player1.user.userId === client.id) {
          this.perspectiveGameState.isCurrentPlayerTurn = gameState.isPlayer1Turn;
          this.perspectiveGameState.currentPlayer = JSON.parse(JSON.stringify(gameState.player1));
          this.perspectiveGameState.opposingPlayer = JSON.parse(JSON.stringify(gameState.player2));
        } else {
          this.perspectiveGameState.isCurrentPlayerTurn = !gameState.isPlayer1Turn;
          this.perspectiveGameState.currentPlayer = JSON.parse(JSON.stringify(gameState.player2));
          this.perspectiveGameState.opposingPlayer = JSON.parse(JSON.stringify(gameState.player1));
        }
        this.whosTurnMessage = this.perspectiveGameState.isCurrentPlayerTurn ? `It's your turn!` : `It's ${this.perspectiveGameState.opposingPlayer.user.userEmail}'s turn!`;
    
        if (!this.perspectiveGameState.gameIsPlayable) 
          this.whosTurnMessage = `The game has ended!`;
      }

}