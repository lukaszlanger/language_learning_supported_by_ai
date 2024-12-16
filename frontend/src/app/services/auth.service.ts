import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { LoginDto } from '../dtos/login.dto';
import { UserDto } from '../dtos/user.dto';

interface LoginResponse {
  message: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'http://localhost:5046/api';
  user: UserDto | undefined;

  constructor(private http: HttpClient) {
    this.loadUserFromStorage();
  }

  login(credentials: LoginDto) {
    return this.http.post<LoginResponse>(`${this.baseUrl}/auth/login`, credentials);
  }

  getUser(email: string) {
    return this.http.get<UserDto>(`${this.baseUrl}/auth/getUser/${email}`);
  }

  async loginAndSetUser(credentials: LoginDto): Promise<void> {
    try {
      const response = await firstValueFrom(this.login(credentials));
      console.log('Login response: ', response);

      const user = await firstValueFrom(this.getUser(credentials.email));
      if (user) {
        this.user = user;
        localStorage.setItem('user', JSON.stringify(user));
        console.log('User found: ', user);
      } else {
        throw new Error('User not found.');
      }

    } catch (error) {
      console.error('Login or user fetch failed.', error);
      throw error;
    }
  }

  loadUserFromStorage(): void {
    const user = localStorage.getItem('user');
    if (user) {
      this.user = JSON.parse(user) as UserDto;
      console.log('User loaded from storage.');
    }
  }

  isLoggedIn(): boolean {
    return !!this.user;
  }
}
