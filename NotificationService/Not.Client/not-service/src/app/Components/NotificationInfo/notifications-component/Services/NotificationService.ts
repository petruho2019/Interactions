import { inject, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: HubConnection | null = null;

  constructor() {}

  public startConnection = () => {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('http://localhost/notificationHub') // URL of the SignalR hub
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection started'))
      .catch((err) => console.log('Error establishing SignalR connection: ' + err));
  };

  public addMessageListener = () => {
    this.hubConnection!.on('ReceiveMessage', (user: string, message: string) => {
      console.log(`User: ${user}, Message: ${message}`);
    });
  };

  public sendMessage = (user: string, message: string) => {
    this.hubConnection!.invoke('SendNotification', message, user).catch((err) =>
      console.error(err),
    );
  };
}
