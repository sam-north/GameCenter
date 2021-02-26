import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
 
  get isSignedIn(): boolean { return document.cookie.indexOf('account') !== -1; };
  constructor() { }
}
