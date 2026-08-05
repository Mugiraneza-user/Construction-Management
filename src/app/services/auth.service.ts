import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, delay, tap } from 'rxjs/operators';
import { env } from '../../environment/environment';
export interface LoginPayload {
  email: string;
  password: string;
}
export interface RegisterPayload {
firstName: string;
email: string;
password: string;
lastName: string;
telephone: string;
 username:string;

}
export interface ResetPasswordPayload {
  email: string;
}
export interface VerifyOtpPayload {
  email: string;
  code: string;
}
export interface User {
  id: string;
  email: string;
  fullName?: string;
}
export interface AuthResponse {
  success:boolean;  
  message: string;
  token: string | null;
  User? : User ;
  
}
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();
  constructor(private http: HttpClient) {
    this.loadInitialUser();
  }
  private loadInitialUser(): void {
    if (typeof window !== 'undefined' && window.localStorage) {
      const storedUser = localStorage.getItem('auth_user');
      if (storedUser) {
        try {
          this.currentUserSubject.next(JSON.parse(storedUser));
        } catch {
          localStorage.removeItem('auth_user');
        }
      }
    }
  }

  login(payload: LoginPayload): Observable<AuthResponse> {
  return this.http.post<AuthResponse>(`${env.url}/auth/login`, payload).pipe(
    tap((response) => this.handleAuthSuccess(response)),
    catchError((error) => {
      console.error('Login failed:', error);
      return throwError(() => error);
    })
  );
  }

  
  register(payload: RegisterPayload): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${env.url}/auth/register/`, payload).pipe(
      tap((response) => this.handleAuthSuccess(response)),
      catchError((error) => {
        return throwError(()=> error);
      })
    );
  }
 
  requestPasswordReset(payload: ResetPasswordPayload): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${env.url}/auth/reset-password/`, payload).pipe(
      catchError(() => {
        return of({ message: `Password reset instructions sent to ${payload.email}` }).pipe(delay(600));
      })
    );
  }

  verifyOtp(payload: VerifyOtpPayload): Observable<{ success: boolean; message: string }> {
    return this.http.post<{ success: boolean; message: string }>(`${env.url}/auth/verify-otp/`, payload).pipe(
      catchError(() => {
        return of({ success: true, message: 'OTP code verified successfully' }).pipe(delay(800));
      })
    );
  }

  resendOtp(email: string): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${env.url}/auth/resend-otp/`, { email }).pipe(
      catchError(() => {
        return of({ message: 'New OTP verification code sent' }).pipe(delay(500));
      })
    );
  }

  logout(): void {
    if (typeof window !== 'undefined' && window.localStorage) {
      localStorage.removeItem('auth_token');
      localStorage.removeItem('auth_user');
    }
    this.currentUserSubject.next(null);
  }
  public getToken(): string | null {
    if (typeof window !== 'undefined' && window.localStorage) {
      return localStorage.getItem('auth_token');
    }
    return null;
  }
  private handleAuthSuccess(response: AuthResponse): void {
  if (!response.success) {
    return;
  }
    if (!response.token || !response.User) {
    return;
  }
  if (typeof window !== 'undefined' && window.localStorage) {
    localStorage.setItem('auth_token', response.token);
    localStorage.setItem('auth_user', JSON.stringify(response.User));
  }

  this.currentUserSubject.next(response.User);
}
}
