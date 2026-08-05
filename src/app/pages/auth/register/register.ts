import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './register.html'
})
export class Register {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private authService = inject(AuthService);

  registerForm = this.fb.group({
    userName: ['', [Validators.required, Validators.minLength(3)]],
    firstName: ['', [Validators.required, Validators.minLength(2)]],
    lastName: ['', [Validators.required, Validators.minLength(2)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]],
    telephone: ['', [Validators.required]],
    agreeTerms: [false, [Validators.requiredTrue]]
  }); 

  showPassword = false;
  isLoading = false;
  errorMessage = '';

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const username = this.registerForm.value.userName || '';
    const firstName = this.registerForm.value.firstName || '';
    const lastName = this.registerForm.value.lastName || '';
    const email = this.registerForm.value.email || '';
    const password = this.registerForm.value.password || '';
    const telephone = this.registerForm.value.telephone || '';


    this.authService.register({ firstName,lastName,telephone, email, password,username }).subscribe({
      next: (response) => {
        this.isLoading = false;
        this.router.navigate(['/verify-otp'], { queryParams: { email } });
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.message || 'Registration failed. Please try again.';
      }
    });
  }
}
