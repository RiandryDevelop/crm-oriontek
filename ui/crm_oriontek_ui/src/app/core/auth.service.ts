// src/app/core/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { User } from '../models/user/user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private userSubject = new BehaviorSubject<User | null>(this.getUserFromLocalStorage());
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) {}

  login(credentials: { username: string; password: string }) {
    return this.http.post<User>('/api/auth/login', credentials).subscribe(user => {
      this.userSubject.next(user);
      localStorage.setItem('user', JSON.stringify(user));
    });
  }

  logout() {
    localStorage.removeItem('user');
    this.userSubject.next(null);
  }

  isLoggedIn(): boolean {
    return this.userSubject.value !== null;
  }

  private getUserFromLocalStorage(): User | null {
    const userJson = localStorage.getItem('user');
    return userJson ? JSON.parse(userJson) as User : null;
  }
}
