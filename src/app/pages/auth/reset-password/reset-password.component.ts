import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './reset-password.component.html'
})
export class ResetPasswordComponent {
  resetForm: FormGroup;
  step: 'request' | 'sent' = 'request';
  isLoading = false;

  constructor(private fb: FormBuilder, private router: Router) {
    this.resetForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onRequestReset(): void {
    if (this.resetForm.invalid) {
      this.resetForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;

    // Simulate sending email reset link
    setTimeout(() => {
      this.isLoading = false;
      this.step = 'sent';
    }, 1000);
  }

  resendEmail(): void {
    this.isLoading = true;
    setTimeout(() => {
      this.isLoading = false;
      alert('Password reset instructions resent!');
    }, 800);
  }
}
