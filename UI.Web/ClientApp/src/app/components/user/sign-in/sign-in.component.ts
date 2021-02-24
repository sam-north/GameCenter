import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignIn } from 'src/app/models/SignIn';
import { ApiService } from 'src/app/services/api.service';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  model: SignIn = {
    email: null,
    password: null
  };

  constructor(private api: ApiService, private router: Router) { }

  ngOnInit(): void {
  }

  async signIn(){        
    if (!this.model.email || !this.model.password) {
      alert("email and password required");
      return;
    }
    const result = await this.api.signInUser(this.model);
    this.router.navigate(['']);
  }
}