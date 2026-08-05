import { Component, OnInit, OnDestroy, ElementRef, ViewChildren, QueryList, inject } from '@angular/core';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormControl, FormArray, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-verify-otp',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './verify-otp.component.html'
})
export class VerifyOtp implements OnInit, OnDestroy {
  @ViewChildren('otpInput') otpInputs!: QueryList<ElementRef<HTMLInputElement>>;

  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private authService = inject(AuthService);

  otpDigits = [0, 1, 2, 3, 4, 5];
  otpForm = this.fb.group({
    digits: this.fb.array(
      this.otpDigits.map(() => this.fb.control('', [Validators.required, Validators.pattern(/^[0-9]$/)]))
    )
  });

  isLoading = false;
  resendTimer = 60;
  timerInterval: any;
  canResend = false;
  userEmail = 'username@example.com';
  errorMessage = '';

  get digitsArray(): FormArray {
    return this.otpForm.get('digits') as FormArray;
  }

  getControl(index: number): FormControl {
    return this.digitsArray.at(index) as FormControl;
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if (params['email']) {
        this.userEmail = params['email'];
      }
    });
    this.startCountdown();
  }

  ngOnDestroy(): void {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
  }

  startCountdown(): void {
    this.canResend = false;
    this.resendTimer = 60;
    if (this.timerInterval) clearInterval(this.timerInterval);

    this.timerInterval = setInterval(() => {
      if (this.resendTimer > 0) {
        this.resendTimer--;
      } else {
        this.canResend = true;
        clearInterval(this.timerInterval);
      }
    }, 1000);
  }

  onInput(event: Event, index: number): void {
    const input = event.target as HTMLInputElement;
    const value = input.value;

    if (value.length > 0) {
      const singleDigit = value.slice(-1);
      this.digitsArray.at(index).setValue(singleDigit);

      if (index < this.otpDigits.length - 1) {
        const inputElements = this.otpInputs.toArray();
        inputElements[index + 1].nativeElement.focus();
      }
    }
  }

  onKeyDown(event: KeyboardEvent, index: number): void {
    if (event.key === 'Backspace') {
      const currentVal = this.digitsArray.at(index).value;
      if (!currentVal && index > 0) {
        const inputElements = this.otpInputs.toArray();
        inputElements[index - 1].nativeElement.focus();
      }
    }
  }

  onPaste(event: ClipboardEvent): void {
    event.preventDefault();
    const pastedData = event.clipboardData?.getData('text').trim();
    if (pastedData && /^\d{6}$/.test(pastedData)) {
      const chars = pastedData.split('');
      chars.forEach((char, i) => {
        if (i < this.digitsArray.length) {
          this.digitsArray.at(i).setValue(char);
        }
      });
      const inputElements = this.otpInputs.toArray();
      inputElements[5].nativeElement.focus();
    }
  }

  onVerify(): void {
    if (this.otpForm.invalid) {
      this.otpForm.markAllAsTouched();
      return;
    }

    const code = this.digitsArray.value.join('');
    this.isLoading = true;
    this.errorMessage = '';

    this.authService.verifyOtp({ email: this.userEmail, code }).subscribe({
      next: (res) => {
        this.isLoading = false;
        alert(res.message || 'OTP Verified successfully!');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err.error?.message || 'Invalid verification code. Please check and try again.';
      }
    });
  }

  resendCode(): void {
    if (!this.canResend) return;

    this.authService.resendOtp(this.userEmail).subscribe({
      next: (res) => {
        this.startCountdown();
        alert(res.message || 'A new verification code has been sent.');
      },
      error: () => {
        this.startCountdown();
      }
    });
  }
}
