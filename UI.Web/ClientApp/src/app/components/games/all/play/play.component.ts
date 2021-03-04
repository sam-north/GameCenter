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
  constructor(private activatedRoute: ActivatedRoute, private god: GodService) { }

  ngOnInit(): void {
    let id : string = this.activatedRoute.snapshot.params['id'];
    this.load(id);
  }
  async load(id: string) {
    const response = await this.god.api.getGameInstance(id);
    this.model = response.data;
  }

}
