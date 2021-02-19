import { Component, OnInit } from '@angular/core';
import { SignIn } from 'src/app/models/SignIn';
import { ApiService } from 'src/app/services/api.service';

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

  constructor(private api: ApiService) { }

  ngOnInit(): void {
  }

  async signIn(){        
    if (!this.model.email || !this.model.password) {
      alert("email and password required");
      return;
    }
    const result = await this.api.signInUser(this.model);
  }
}