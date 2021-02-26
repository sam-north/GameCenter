import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/examples/counter/counter.component';
import { FetchDataComponent } from './components/examples/fetch-data/fetch-data.component';
import { CrudExampleComponent } from './components/examples/crud-examples/crud-example/crud-example.component';
import { CrudExamplesComponent } from './components/examples/crud-examples/crud-examples.component';
import { BoardComponent } from './components/games/tic-tac-toe/board/board.component';
import { SquareComponent } from './components/games/tic-tac-toe/square/square.component';
import { ApiService } from './services/api.service';
import { SignUpComponent } from './components/account/sign-up/sign-up.component';
import { SignInComponent } from './components/account/sign-in/sign-in.component';
import { ErrorInterceptorProvider } from './utilities/ErrorInterceptor';
import { ClientService } from './services/client.service';
import { DashboardComponent } from './components/account/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CrudExampleComponent,
    CrudExamplesComponent,
    SquareComponent,
    BoardComponent,
    SignUpComponent,
    SignInComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'sign-in', component: SignInComponent },
      { path: 'sign-up', component: SignUpComponent },
      { path: 'crud-examples', component: CrudExamplesComponent },
      { path: 'crud-example/:id', component: CrudExampleComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'tic-tac-toe', component: BoardComponent }
    ])
  ],
  providers: [
    ApiService, 
    ErrorInterceptorProvider,
    ClientService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
