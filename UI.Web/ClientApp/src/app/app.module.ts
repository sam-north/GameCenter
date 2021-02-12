import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { CrudExampleComponent } from './components/crud-examples/crud-example/crud-example.component';
import { CrudExamplesComponent } from './components/crud-examples/crud-examples.component';
import { BoardComponent } from './components/tic-tac-toe/board/board.component';
import { SquareComponent } from './components/tic-tac-toe/square/square.component';
import { ApiService } from './services/api.service';
import { SignUpComponent } from './components/user/sign-up/sign-up.component';

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
    SignUpComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'sign-up', component: SignUpComponent },
      { path: 'crud-examples', component: CrudExamplesComponent },
      { path: 'crud-example/:id', component: CrudExampleComponent },
      { path: 'tic-tac-toe', component: BoardComponent }
    ])
  ],
  providers: [ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
