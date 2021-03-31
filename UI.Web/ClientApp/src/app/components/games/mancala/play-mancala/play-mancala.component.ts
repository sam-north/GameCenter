import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { MancalaGameState } from 'src/app/models/MancalaGameState';
import { MancalaPerspectiveGameState } from 'src/app/models/MancalaPerspectiveGameState';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'app-play-mancala',
  templateUrl: './play-mancala.component.html',
  styleUrls: ['./play-mancala.component.scss']
})
export class PlayMancalaComponent implements OnChanges {
  columns: number[] = new Array(6);
  whosTurnMessage: string;
  @Input() gameState: string;
  @Output() userInputReceived: EventEmitter<any> = new EventEmitter<any>();

  perspectiveGameState: MancalaPerspectiveGameState = new MancalaPerspectiveGameState();

  constructor(private god: GodService) {
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes.gameState) {
      this.translateGameStateToPerspective(changes.gameState.currentValue);
    }
  }

  currentPlayerSlotClick(i: number) {
    this.userInputReceived.emit((i + 1).toString());
  }

  translateGameStateToPerspective(currentValue: string) {
    let gameState: MancalaGameState = JSON.parse(currentValue);
    let client: any = this.god.client.getClient();

    this.perspectiveGameState.hasGameBeenSetup = gameState.hasGameBeenSetup;
    this.perspectiveGameState.gameIsPlayable = gameState.gameIsPlayable;

    if (gameState.player1.user.userId === client) {
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