import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameInstanceDto } from 'src/app/models/GameInstanceDto';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.scss']
})
export class PlayComponent implements OnInit {

  model: GameInstanceDto = new GameInstanceDto();
  refreshCheckInterval;
  constructor(private activatedRoute: ActivatedRoute, private god: GodService) { }

  ngOnInit(): void {
    let id: string = this.activatedRoute.snapshot.params['id'];
    this.load(id);
  }
  async load(id: string) {
    const response = await this.god.api.getGameInstance(id);
    this.model = response.data;

    this.setRefreshInterval();
  }

  setRefreshInterval() {
    this.refreshCheckInterval = setInterval(async () => {
      const response = await this.god.api.checkGameInstanceForRefresh(this.model.id, this.model.state.dateCreated);
      if (response && response.data){
        this.god.notifications.success("game refreshed!");
        this.model = response.data;
      }
    }, 3000);
  }

  async handleUserInput(data: any) {
    const response = await this.god.api.playGame({ id: this.model.id, userInput: data });
    if (response.messages.length)
      this.god.notifications.infoList(response.messages);
    if (response.errors.length)
      this.god.notifications.dangerList(response.errors);
  }
}
