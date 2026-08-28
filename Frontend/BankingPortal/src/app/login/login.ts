import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import {
  AuthService,
  LoginRequest,
  LoginResponse
} from '../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  username = '';
  password = '';

  constructor(
    private router: Router,
    private authService: AuthService
  ) {}

  login() {

    const request: LoginRequest = {
      username: this.username,
      password: this.password
    };

    this.authService.login(request).subscribe({
      next: (response: LoginResponse) => {

        console.log('JWT received:', response.token);

        localStorage.setItem('token', response.token);

        this.router.navigate(['/dashboard']);
      },

      error: (error) => {

        console.error('Login failed:', error);

        alert('Invalid Username or Password');
      }
    });
  }
}