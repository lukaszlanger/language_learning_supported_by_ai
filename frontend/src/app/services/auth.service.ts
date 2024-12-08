import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { LoginDto } from '../dtos/login.dto';
import { UserDto } from '../dtos/user.dto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:5046/api';
  user: UserDto | undefined;

  constructor(private http: HttpClient) {}

  login(credentials: LoginDto) {
    return this.http.post<{ token: string }>(`${this.baseUrl}/auth/login`, credentials);
  }

  getUser(email: string) {
    return this.http.get<UserDto>(`${this.baseUrl}/auth/getUser/${email}`);
  }

  async loginAndSetUser(credentials: LoginDto): Promise<void> {
    try {
      const response = await firstValueFrom(this.login(credentials));
      console.log('Login successful', response);

      const user = await firstValueFrom(this.getUser(credentials.email));
      if (user) {
        this.user = user;
        console.log('User found', user);
      } else {
        throw new Error('User not found');
      }

      localStorage.setItem('token', response.token);
    } catch (error) {
      console.error('Login or user fetch failed', error);
      throw error;
    }
  }

  isLoggedIn(): boolean {
    return !!this.user;
  }
}
