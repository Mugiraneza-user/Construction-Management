import { Routes } from '@angular/router';
import { AuthLayoutComponent } from './pages/auth/auth-layout/auth-layout.component';
import { Login } from './pages/auth/login/login';
import { Register } from './pages/auth/register/register';
import { ResetPasswordComponent } from './pages/auth/reset-password/reset-password.component';
import { VerifyOtpComponent } from './pages/auth/verify-otp/verify-otp.component';

export const routes: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: Login },
      { path: 'register', component: Register },
      { path: 'signup', redirectTo: 'register', pathMatch: 'full' },
      { path: 'reset-password', component: ResetPasswordComponent },
      { path: 'verify-otp', component: VerifyOtpComponent }
    ]
  },
  { path: '**', redirectTo: 'login' }
];
