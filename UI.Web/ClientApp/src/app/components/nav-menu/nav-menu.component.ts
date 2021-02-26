import { Component } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  get isSignedin (): boolean { return this.clientService.isSignedIn; };
  constructor(private api: ApiService, private clientService: ClientService){
    
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  async signOut(){
    await this.api.signOutUser();
  }
}
