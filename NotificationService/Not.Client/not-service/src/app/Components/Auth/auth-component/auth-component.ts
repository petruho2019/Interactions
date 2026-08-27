import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-component',
  imports: [FormsModule],
  templateUrl: './auth-component.html',
  styleUrl: './auth-component.scss',
})
export class AuthComponent {
  private httpClient = inject(HttpClient);
  private router = inject(Router);

  public username: String = '';

  async login() {
    if (this.username) {
      this.httpClient
        .get(`http://localhost:80/api/v1/auth/login?username=${this.username}`, {
          observe: 'response',
          withCredentials: true,
        })
        .subscribe(() => {
          this.router.navigate(['/notifications']);
        });
    }
  }
}
