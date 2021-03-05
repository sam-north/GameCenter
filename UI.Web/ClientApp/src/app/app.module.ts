import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
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
import { MancalaAboutComponent } from './components/games/mancala/mancala-about/mancala-about.component';
import { CanActivateGuard } from './services/can-activate-guard.service';
import { Notifier } from './utilities/notifications/Notifier';
import { ToastrNotifier } from './utilities/notifications/implementations/ToastrNotifier';
import { NotificationService } from './services/notification.service';
import { GodService } from './services/god.service';
import { StyleExamplesComponent } from './components/examples/style-examples/style-examples.component';
import { NewGameComponent } from './components/games/all/new-game/new-game.component';
import { PlayComponent } from './components/games/all/play/play.component';
import { CheckersAboutComponent } from './components/games/checkers/checkers-about/checkers-about.component';

const anonymousRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'mancala', component: MancalaAboutComponent },
  { path: 'checkers', component: CheckersAboutComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
];
const authenticatedRoutes: Routes = [
  { 
    path: '', canActivate: [CanActivateGuard], children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'style-examples', component: StyleExamplesComponent },
      { path: 'new-game', component: NewGameComponent },
      { path: 'play/:id', component: PlayComponent },
      { path: 'crud-examples', component: CrudExamplesComponent },
      { path: 'crud-example/:id', component: CrudExampleComponent },
      { path: 'tic-tac-toe', component: BoardComponent },
      { path: '**', component: HomeComponent },
    ]
  }
];

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
    DashboardComponent,
    MancalaAboutComponent,
    StyleExamplesComponent,
    NewGameComponent,
    PlayComponent,
    CheckersAboutComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([...anonymousRoutes, ...authenticatedRoutes])
  ],
  providers: [
    ApiService, 
    ErrorInterceptorProvider,
    ClientService,    
    CanActivateGuard,
    GodService,
    NotificationService,
    { provide: Notifier, useClass: ToastrNotifier }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
