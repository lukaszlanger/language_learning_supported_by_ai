import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { LoginDto } from '../dtos/login.dto';
import { UserDto } from '../dtos/user.dto';
import { environment } from 'src/environments/environment.prod';

interface LoginResponse {
  message: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = environment.baseUrl;
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
        console.log('User found:', user);
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
      try {
        this.user = JSON.parse(user) as UserDto;
        console.log('User loaded from storage. User Id:', this.user.id);
      } catch (error) {
        console.error('Failed to parse user from storage:', error);
        localStorage.removeItem('user');
      }
    }
  }

  isLoggedIn(): boolean {
    if (!this.user) {
      this.loadUserFromStorage();
    }
    return !!this.user;
  }

  logout(): void {
    this.user = undefined;
    localStorage.removeItem('user');
    console.log('User logged out.');
  }
}
