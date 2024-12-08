import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginDto } from '../dtos/login.dto';
import { UserDto } from '../dtos/user.dto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:5046/api';
  user: UserDto | undefined;

  constructor(private http: HttpClient) {}

  login(credentials: LoginDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/auth/login`, credentials);
  }

  getUser(email: string): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.baseUrl}/auth/getUser/${email}`);
  }

  async loginAndSetUser(credentials: LoginDto): Promise<void> {
    try {
      const response = await this.login(credentials).toPromise();
      console.log('Login successful', response);
      const user = await this.getUser(credentials.email).toPromise();
      if (user) {
        this.user = user;
      } else {
        throw new Error('User not found');
      }
      console.log('User found', user);
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
