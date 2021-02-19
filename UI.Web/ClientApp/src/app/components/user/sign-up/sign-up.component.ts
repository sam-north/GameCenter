import { Component, OnInit } from '@angular/core';
import { SignUp } from 'src/app/models/SignUp';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpModel: SignUp = {
    email: null,
    password: null,
    confirmPassword: null
  };
  constructor(private api: ApiService) { }

  ngOnInit(): void {
  }

  async submitUser(){
    if (this.signUpModel.confirmPassword != this.signUpModel.password) {
      alert("Passwords don't match");
    }

    if (this.signUpModel.email.length && this.signUpModel.password.length && this.signUpModel.confirmPassword.length
      && this.signUpModel.confirmPassword == this.signUpModel.password) {
      //call to server to save
      
    const result = await this.api.signUpUser(this.signUpModel);
    //this.router.navigate(['/crud-examples']);
    }

  }

}
