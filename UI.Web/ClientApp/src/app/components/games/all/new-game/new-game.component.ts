import { Component, OnInit } from '@angular/core';
import { GameDto } from 'src/app/models/GameDto';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'app-new-game',
  templateUrl: './new-game.component.html',
  styleUrls: ['./new-game.component.scss']
})
export class NewGameComponent implements OnInit {
  games: GameDto[];
  selectedGame: GameDto;
  opponentEmail: string;
  constructor(private god: GodService) { }

  ngOnInit(): void {
    this.load();
  }
  async load() {
    this.games = await (await this.god.api.getGames()).map(x => { return {displayName: x.displayName, id: x.id}});
  }

  async submit(){
    const response = await this.god.api.newGame({opponentEmail:this.opponentEmail, gameId: this.selectedGame.id});
    
  }

}
