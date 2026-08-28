import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7134/api/Auth';

  private readonly roleClaim =
    'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

  constructor(private http: HttpClient) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${this.apiUrl}/login`,
      request
    );
  }

  getRole(): string | null {
    const token = localStorage.getItem('token');

    if (!token) {
      return null;
    }

    try {
      const payloadPart = token.split('.')[1]
        .replace(/-/g, '+')
        .replace(/_/g, '/');

      const payload = JSON.parse(atob(payloadPart));

      return payload[this.roleClaim] || payload['role'] || null;
    } catch {
      return null;
    }
  }

  isAdmin(): boolean {
    return this.getRole() === 'Admin';
  }
}
