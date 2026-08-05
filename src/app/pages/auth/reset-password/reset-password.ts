import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './reset-password.component.html'
})
export class ResetPassword{
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private authService = inject(AuthService);

  resetForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]]
  });

  step: 'request' | 'sent' = 'request';
  isLoading = false;
  errorMessage = '';

  onRequestReset(): void {
    if (this.resetForm.invalid) {
      this.resetForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const email = this.resetForm.value.email || '';

    this.authService.requestPasswordReset({ email }).subscribe({
      next: () => {
        this.isLoading = false;
        this.step = 'sent';
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.message || 'Failed to send reset link. Please try again.';
      }
    });
  }

  resendEmail(): void {
    this.isLoading = true;
    const email = this.resetForm.value.email || '';

    this.authService.requestPasswordReset({ email }).subscribe({
      next: () => {
        this.isLoading = false;
        alert('Password reset instructions resent successfully!');
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }
}
