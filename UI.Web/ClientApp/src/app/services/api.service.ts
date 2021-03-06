import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CRUDExample } from '../models/CRUDExample';
import { SignIn } from '../models/SignIn';
import { SignUp } from '../models/SignUp';
import { throwError } from 'rxjs';
import { Response } from '../models/Response';
import { GameDto } from '../models/GameDto';
import { CreateGameInstanceDto } from '../models/CreateGameInstanceDto';
import { GameInstanceDto } from '../models/GameInstanceDto';
import { UserGameInstanceStatelessDto } from '../models/UserGameInstanceStatelessDto';
import { PlayGameInstanceDto } from '../models/PlayGameInstanceDto';
import { GameInstanceUserMessageDto } from '../models/GameInstanceUserMessageDto';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  getRootUrl(): string {
    const origin :string = window.location.origin;
    return origin;
  }
  url: string = window.location.href;
  arr: string[] = this.url.split("/");
  rootUrl: string = this.getRootUrl();

  constructor(private http: HttpClient) { }

  getCrudExamples(): Promise<CRUDExample[]> {
    return this.http.get<CRUDExample[]>(`${this.rootUrl}/crudexample`).toPromise();
  }
  getCrudExample(id: number): Promise<CRUDExample> {
    return this.http.get<CRUDExample>(`${this.rootUrl}/crudexample/${id}`).toPromise();
  }
  createCrudExample(crudExample: CRUDExample): Promise<CRUDExample> {
    return this.http.post<CRUDExample>(`${this.rootUrl}/crudexample`,
      {
        text: crudExample.text
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
    this.getRootUrl();
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
  getGames(): Promise<GameDto[]> {
    return this.http.get<GameDto[]>(`${this.rootUrl}/game`).toPromise();
  }
  newGame(dto: CreateGameInstanceDto): Promise<Response<GameInstanceDto>> {
    return this.http.post<Response<GameInstanceDto>>(`${this.rootUrl}/UserGame/New`,
      {
        opponentEmail: dto.opponentEmail,
        gameId: dto.gameId
      }
    ).toPromise();
  }
  playGame(dto: PlayGameInstanceDto): Promise<Response<GameInstanceDto>> {
    return this.http.post<Response<GameInstanceDto>>(`${this.rootUrl}/UserGame/Play`,
      {
        id: dto.id,
        userInput: dto.userInput
      }
    ).toPromise();
  }
  checkGameInstanceForRefresh(id: number, date: Date): Promise<Response<GameInstanceDto>> {
    return this.http.post<Response<GameInstanceDto>>(`${this.rootUrl}/UserGame/RefreshCheck`,
      {
        id: id,
        date: date
      }
    ).toPromise();
  }
  getGameInstance(id: string): Promise<Response<GameInstanceDto>> {
    return this.http.get<Response<GameInstanceDto>>(`${this.rootUrl}/usergame/get/${id}`).toPromise();
  }
  getGameInstanceChat(id: string): Promise<Response<GameInstanceUserMessageDto[]>> {
    return this.http.get<Response<GameInstanceUserMessageDto[]>>(`${this.rootUrl}/usergame/getchat/${id}`).toPromise();
  }
  getUserGames(): Promise<Response<UserGameInstanceStatelessDto[]>> {
    return this.http.get<Response<UserGameInstanceStatelessDto[]>>(`${this.rootUrl}/usergame/get`).toPromise();
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
