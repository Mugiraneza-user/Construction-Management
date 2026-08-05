import { Component, OnInit, OnDestroy, ElementRef, ViewChildren, QueryList } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-verify-otp',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './verify-otp.component.html'
})
export class VerifyOtpComponent implements OnInit, OnDestroy {
  @ViewChildren('otpInput') otpInputs!: QueryList<ElementRef<HTMLInputElement>>;

  otpForm: FormGroup;
  otpDigits = [0, 1, 2, 3, 4, 5];
  isLoading = false;
  resendTimer = 60;
  timerInterval: any;
  canResend = false;
  userEmail = 'username@example.com';

  constructor(private fb: FormBuilder, private router: Router) {
    this.otpForm = this.fb.group({
      digits: this.fb.array(
        this.otpDigits.map(() => this.fb.control('', [Validators.required, Validators.pattern(/^[0-9]$/)]))
      )
    });
  }

  get digitsArray(): FormArray {
    return this.otpForm.get('digits') as FormArray;
  }

  ngOnInit(): void {
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
      // Keep only the last character
      const singleDigit = value.slice(-1);
      this.digitsArray.at(index).setValue(singleDigit);

      // Move focus to next input
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

    // Simulate OTP verification API call
    setTimeout(() => {
      this.isLoading = false;
      alert(`Success! OTP Code (${code}) verified successfully.`);
      this.router.navigate(['/login']);
    }, 1200);
  }

  resendCode(): void {
    if (!this.canResend) return;
    this.startCountdown();
    alert('A new 6-digit OTP code has been sent to your email.');
  }
}
