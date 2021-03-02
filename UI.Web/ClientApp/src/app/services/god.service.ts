import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ApiService } from "./api.service";
import { ClientService } from "./client.service";
import { NotificationService } from "./notification.service";


@Injectable()
export class GodService {

  handleNavigationEnd() {
    this.notifications.removePersistents();
  }

  constructor(
    public client: ClientService,
    public router: Router,
    public api: ApiService,
    public notifications: NotificationService,
  ) { }
}
