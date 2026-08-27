import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SignalRService } from './Services/NotificationService';

@Component({
  selector: 'app-notifications-component',
  imports: [FormsModule],
  templateUrl: './notifications-component.html',
  styleUrl: './notifications-component.scss',
})
export class NotificationsComponent implements OnInit {
  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addMessageListener();
  }
  private signalRService = inject(SignalRService);

  public message: string = '';
  public username: string = '';

  sendNotification() {
    this.signalRService.sendMessage(this.username, this.message);
    this.message = ''; // Clear the input after sending
  }
}
