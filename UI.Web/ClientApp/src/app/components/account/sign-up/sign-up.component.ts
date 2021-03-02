import { Component, OnInit } from '@angular/core';
import { SignUp } from 'src/app/models/SignUp';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  signUpModel: SignUp = {
    email: null,
    password: null,
    confirmPassword: null
  };
  constructor(private god: GodService) { }

  ngOnInit(): void {
  }

  async submitUser(){
    this.god.notifications.removeAll();
    if (this.signUpModel.email && this.signUpModel.password && this.signUpModel.confirmPassword) {
      if (this.signUpModel.confirmPassword != this.signUpModel.password) {
        this.god.notifications.danger("Passwords don't match");
      }
      if (this.signUpModel.email.length && this.signUpModel.password.length && this.signUpModel.confirmPassword.length
        && this.signUpModel.confirmPassword == this.signUpModel.password) {
        this.god.router.navigate(['/sign-in']);
      }
    }
    else
      this.god.notifications.danger("email and passwords required");

  }

}
