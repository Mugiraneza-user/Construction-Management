import { Routes } from '@angular/router';
import { AuthLayoutComponent } from './pages/auth/auth-layout/auth-layout.component';
import { Login } from './pages/auth/login/login';
import { Register } from './pages/auth/register/register';
import { ResetPassword } from './pages/auth/reset-password/reset-password';
import { VerifyOtp } from './pages/auth/verify-otp/verify-otp';

export const routes: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: Login },
      { path: 'register', component: Register },
      { path: 'signup', redirectTo: 'register', pathMatch: 'full' },
      { path: 'reset-password', component: ResetPassword },
      { path: 'verify-otp', component: VerifyOtp }
    ]
  },
  { path: '**', redirectTo: 'login' }
];
