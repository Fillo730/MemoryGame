//Angular
import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

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
  private router = inject(Router);
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
    })
  });

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

    const signupData: RegisterRequest = this.signupForm.getRawValue() as RegisterRequest;

    this.authService.register(signupData).pipe(
      finalize(() => this.isLoading.set(false)
    ))
    .subscribe({
      next: (response) => {
        if (response.success) {
          this.isLoading.set(false);
          this.router.navigate(['/home']);
        }
      },
      error: () => {
        this.isLoading.set(false);
        this.error.set(this.translateService.instant("Signup.HttpError"));
      }
    });
  }
}