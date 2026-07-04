//Angular
import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';

//Components
import { GenericButtonComponent } from '../../components/generic-button/generic-button.component';

//Services
import { AuthService } from '../../services/auth-service.service';
import { NavigationService } from '../../services/NavigationService.service';

//Models
import { RegisterRequest } from '../../models/requests/RegisterRequest.model';

//i18n
import { TranslatePipe, TranslateService } from '@ngx-translate/core';

//Utils
import { finalize } from 'rxjs';

function passwordsMatchValidator(group: AbstractControl): ValidationErrors | null {
  const password = group.get('password')?.value;
  const confirmPassword = group.get('confirmPassword')?.value;

  return password === confirmPassword ? null : { passwordMismatch: true };
}

@Component({
  selector: 'signup-page',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    GenericButtonComponent,
    TranslatePipe
  ],
  templateUrl: './signup.page.html',
  styleUrl: './signup.page.css'
})
export class SignupPage {
  private authService = inject(AuthService);
  private translateService = inject(TranslateService);
  public navigationService = inject(NavigationService);

  public signupForm = new FormGroup({
    username: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.minLength(5), Validators.required]
    }),
    email: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.email, Validators.required]
    }),
    password: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.minLength(5), Validators.required]
    }),
    confirmPassword: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required]
    })
  }, { validators: passwordsMatchValidator });

  get f() { return this.signupForm.controls; }

  isLoading = signal(false);
  error = signal<string | null>(null);

  signup(e: Event) {
    e.preventDefault();
    
    if (this.signupForm.invalid) {
      this.signupForm.markAllAsTouched();
      this.error.set(this.translateService.instant("Signup.InvalidForm"));
      return;
    }

    this.error.set(null);
    this.isLoading.set(true);

    const { username, email, password } = this.signupForm.getRawValue();
    const signupData: RegisterRequest = { username, email, password };

    this.authService.register(signupData).pipe(
      finalize(() => this.isLoading.set(false)
    ))
    .subscribe({
      next: (response) => {
        if (response.success) {
          this.isLoading.set(false);
          this.navigationService.goToHome();
        }
      },
      error: () => {
        this.isLoading.set(false);
        this.error.set(this.translateService.instant("Signup.HttpError"));
      }
    });
  }
}