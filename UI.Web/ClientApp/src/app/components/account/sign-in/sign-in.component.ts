import { Component, OnInit } from '@angular/core';
import { SignIn } from 'src/app/models/SignIn';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {

  model: SignIn = {
    email: null,
    password: null
  };

  constructor(private god: GodService) { }

  ngOnInit(): void {
  }

  async signIn(){      
    this.god.notifications.removeAll();
    if (!this.model.email || !this.model.password) {
      this.god.notifications.danger("email and password required");
      return;
    }
    const result = await this.god.api.signInUser(this.model);
    this.god.notifications.info("you are signed in");
    this.god.router.navigate(['dashboard']);
  }
}

