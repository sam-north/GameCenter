import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ClientService } from './client.service';

@Injectable()
export class CanActivateGuard implements CanActivate {
  constructor(private router: Router, private clientService: ClientService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    if (route.url && route.url[0] && route.url[0].path && !this.isUserLoggedIn()) {
        this.router.navigate(['dashboard']);
        return false;
    }
    return true;
  }
  isUserLoggedIn(): boolean {
    return this.clientService.isSignedIn;
  }
}