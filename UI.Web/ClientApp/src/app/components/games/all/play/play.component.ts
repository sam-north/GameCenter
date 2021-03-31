import { AfterContentInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameInstanceDto } from 'src/app/models/GameInstanceDto';
import { GodService } from 'src/app/services/god.service';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { GameInstanceUserMessageDto } from 'src/app/models/GameInstanceUserMessageDto';
import { Client } from 'src/app/models/Client';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.scss']
})
export class PlayComponent implements OnInit, AfterContentInit, OnDestroy {

  model: GameInstanceDto = new GameInstanceDto();
  chatMessages: GameInstanceUserMessageDto[] = [];
  refreshCheckInterval;
  messageInput: string;
  connection: HubConnection;
  currentUser: Client;
  @ViewChild("chatHistoryWindow") chatHistoryWindow: ElementRef;
  constructor(private activatedRoute: ActivatedRoute, private god: GodService) { }

  async ngAfterContentInit(): Promise<void> {
    let id: string = this.activatedRoute.snapshot.params['id'];
    await this.loadChatHistory(id);
  }

  async ngOnInit(): Promise<void> {
    let id: string = this.activatedRoute.snapshot.params['id'];
    this.currentUser = this.god.client.getClient();

    await this.setupSignalRConnection(id);
    await this.load(id);
    await this.loadChatHistory(id);
  }

  private async setupSignalRConnection(id: string) {
    let that = this;
    that.connection = new HubConnectionBuilder().withUrl("/gameHub").build();

    that.connection.on("ReceiveMessage", function (message: GameInstanceUserMessageDto) {
      that.chatMessages.push(message);
      that.scrollBottomOfChatHistory(100);
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
  scrollBottomOfChatHistory(bufferTimeout) {
    setTimeout(() => {
      this.chatHistoryWindow.nativeElement.scrollTop = this.chatHistoryWindow.nativeElement.scrollHeight;
    }, bufferTimeout);
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

  async loadChatHistory(id: string) {
    const response = await this.god.api.getGameInstanceChat(id);
    this.chatMessages = response.data;
    this.scrollBottomOfChatHistory(100);
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

  async sendMessage() {
    if (this.messageInput && this.messageInput.length) {
      await this.connection
        .invoke("SendMessage", { id: this.model.id, text: this.messageInput})
        .catch(function (err) {
          return console.error(err.toString());
        });
      this.messageInput = null;
    }
  }
}