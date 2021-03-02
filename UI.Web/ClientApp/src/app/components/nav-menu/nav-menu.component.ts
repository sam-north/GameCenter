import { Component } from '@angular/core';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;
  get isSignedin (): boolean { return this.god.client.isSignedIn; };
  constructor(private god: GodService){
    
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  async signOut(){
    this.god.notifications.removeAll();
    await this.god.api.signOutUser();
    this.god.notifications.info('you have been signed out.');
  }
}
