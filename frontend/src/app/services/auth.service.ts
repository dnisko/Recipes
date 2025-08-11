import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';


export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}
export interface DecodedToken {
  unique_name: string;
  role: string;
  exp: number;
}
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = 'http://localhost:5014/api/user';
  private currentUserSubject = new BehaviorSubject<DecodedToken | null>(null);
  
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) { 
    const token = localStorage.getItem('token');
    if(token) {
      const decoded = this.decodeToken(token);
      if(decoded) {
        this.currentUserSubject.next(decoded);
      } else {
        localStorage.removeItem('token');
      }
    }
  }

  login(credentials: { username: string; password: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials).pipe(
      tap((response: any) => {
        if (response?.data?.token) { // matches your API shape
          localStorage.setItem('token', response.data.token);
        }
      })
    );
  }

  register(model: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, model);
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  private decodeToken(token: string): DecodedToken | null {
    try {
      return jwtDecode<DecodedToken>(token);
    } catch (error) {
      return null;
    }
  }

  isLoggedIn(): boolean {
    return !!this.currentUserSubject.value;
  }

  hasRole(role: string): boolean {
    return this.currentUserSubject.value?.role === role;
  }
}
function jwtDecode<T>(token: string): DecodedToken | null {
  throw new Error('Function not implemented.');
}

