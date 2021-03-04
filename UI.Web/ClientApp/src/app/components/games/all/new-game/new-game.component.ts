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
    if (this.isValid()) {
      const response = await this.god.api.newGame({opponentEmail:this.opponentEmail, gameId: this.selectedGame.id});
      this.god.notifications.success('game created');  
      this.god.router.navigate(['/play', response.data.id]);
      return;
    }
  }
  isValid(): boolean {
    this.god.notifications.removeAll();
    let errors: string[] = [];
    if (!this.selectedGame) 
      errors.push('A game must be selected');
    if (!this.opponentEmail) 
      errors.push('A opponent email is required');
    if (errors.length) 
      this.god.notifications.dangerList(errors);
    return errors.length <= 0;
  }

}
