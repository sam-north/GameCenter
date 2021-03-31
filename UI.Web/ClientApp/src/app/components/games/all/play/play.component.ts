import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameInstanceDto } from 'src/app/models/GameInstanceDto';
import { GodService } from 'src/app/services/god.service';
import { HubConnection, HubConnectionBuilder} from '@microsoft/signalr';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.scss']
})
export class PlayComponent implements OnInit, OnDestroy {

  model: GameInstanceDto = new GameInstanceDto();
  refreshCheckInterval;
  messageInput: string;
  connection: HubConnection;
  constructor(private activatedRoute: ActivatedRoute, private god: GodService) { }
  

  async ngOnInit(): Promise<void> {
    let id: string = this.activatedRoute.snapshot.params['id'];

    await this.setupSignalRConnection(id);    
    this.load(id);
  }

  private async setupSignalRConnection(id: string) {    
    let that = this;
    that.connection = new HubConnectionBuilder().withUrl("/gameHub").build();

    that.connection.on("ReceiveMessage", function (user, message) {
      var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
      var encodedMsg = user + " says " + msg;
      var li = document.createElement("li");
      li.textContent = encodedMsg;
      document.getElementById("messagesList").appendChild(li);
    });

    that.connection.on("Refresh", async function () {
      await that.load(id);
      that.god.notifications.success("game refreshed!");
    });

    await that.connection
      .start()
      .then(function () {
      })
      .catch(function (err) {
        return console.error(err.toString());
      });

    this.connection
      .invoke("SubscribeToGame", id)
      .catch(function (err) {
          return console.error(err.toString());
      });
  }

  async ngOnDestroy(): Promise<void> {
    await this.connection
    .invoke("UnsubscribeFromGame", this.model.id)
    .catch(function (err) {
        return console.error(err.toString());
    });

    this.connection.stop()
    .then(function () {
    })
    .catch(function (err) {
      return console.error(err.toString());
    });
  }

  async load(id: string) {
    const response = await this.god.api.getGameInstance(id);
    this.model = response.data;
  }

  async handleUserInput(data: any) {
    const response = await this.god.api.playGame({ id: this.model.id, userInput: data });
    if (response.messages.length)
      this.god.notifications.infoList(response.messages);
    if (response.errors.length)
      this.god.notifications.dangerList(response.errors);
    else
      this.connection
      .invoke("GameUpdated", this.model.id)
      .catch(function (err) {
          return console.error(err.toString());
      });

  }

  async messageClick(){
    this.connection
        .invoke("SendMessage", this.model.id, this.messageInput)
        .catch(function (err) {
            return console.error(err.toString());
        });
  }
  
}





