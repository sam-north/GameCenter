import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CRUDExample } from '../models/CRUDExample';
import { SignIn } from '../models/SignIn';
import { SignUp } from '../models/SignUp';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { Client } from '../models/Client';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  url:string = window.location.href;
  arr:string[] = this.url.split("/");
  rootUrl:string = "https://localhost:44353"

  constructor(private http: HttpClient) { }

  getCrudExamples(): Promise<CRUDExample[]> {
    return this.http.get<CRUDExample[]>(`${this.rootUrl}/crudexample`).toPromise();
  }
  getCrudExample(id: number): Promise<CRUDExample> {
    return this.http.get<CRUDExample>(`${this.rootUrl}/crudexample/${id}`).toPromise();
  }
  createCrudExample(crudExample: CRUDExample): Promise<CRUDExample> {
    return this.http.post<CRUDExample>(`${this.rootUrl}/crudexample`, 
    { text: crudExample.text
    }
    ).toPromise();
  }
  updateCrudExample(crudExample: CRUDExample): Promise<CRUDExample> {
    return this.http.put<CRUDExample>(`${this.rootUrl}/crudexample/${crudExample.id}`, 
    { text: crudExample.text }).toPromise();
  }
  deleteCrudExample(id: number): Promise<void> {
    return this.http.delete<void>(`${this.rootUrl}/crudexample/${id}`).toPromise();
  }  
  signUpUser(signUpModel: SignUp): Promise<void> {
    return this.http.post<void>(`${this.rootUrl}/User/SignUp`, 
    { 
      email: signUpModel.email,
      password: signUpModel.password,
      confirmPassword: signUpModel.confirmPassword
    }
    ).toPromise();
  }
  signInUser(signInModel: SignIn): Promise<void> {
    return this.http.post<void>(`${this.rootUrl}/User/SignIn`, 
    { 
      email: signInModel.email,
      password: signInModel.password
    }
    ).toPromise();
  }
  signOutUser(): Promise<void> {
    return this.http.post<void>(`${this.rootUrl}/User/SignOut`, null).toPromise();
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError('Something bad happened; please try again later.');
  }
}
