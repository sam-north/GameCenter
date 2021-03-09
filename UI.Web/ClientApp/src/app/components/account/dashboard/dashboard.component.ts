import { Component, OnInit } from '@angular/core';
import { UserGameInstanceStatelessDto } from 'src/app/models/UserGameInstanceStatelessDto';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  userGames: UserGameInstanceStatelessDto[] = [];
  constructor(private god: GodService) { }

  ngOnInit(): void {
    this.load();
  }

  async load() {
    const response = await this.god.api.getUserGames();
    this.userGames = response.data;
  }

  play(userGame: UserGameInstanceStatelessDto){
    this.god.router.navigate(['/play', userGame.id]);
  }

  // async delete(userGame: UserGameInstanceStatelessDto){
  //   await this.god.api.deleteCrudExample(crudExample.id);
  //   await this.load();
  //   this.god.notifications.success('deleted');
  // }
}
