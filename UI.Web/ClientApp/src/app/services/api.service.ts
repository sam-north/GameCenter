import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CRUDExample } from '../models/CRUDExample';
import { SignUp } from '../models/SignUp';

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
  
  signUpUser(signUpModel: SignUp): Promise<SignUp> {
    return this.http.post<SignUp>(`${this.rootUrl}/User`, 
    { 
      email: signUpModel.email,
      password: signUpModel.password
    }
    ).toPromise();
  }
}
