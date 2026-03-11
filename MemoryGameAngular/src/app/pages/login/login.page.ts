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

//i18n
import { TranslatePipe, TranslateService } from '@ngx-translate/core';

//Models
import { LoginRequest } from '../../models/requests/LoginRequest.model';

//rxjs
import { finalize, tap } from 'rxjs';

@Component({
  selector: 'login-page',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    GenericButtonComponent,
    TranslatePipe
  ],
  templateUrl: './login.page.html',
  styleUrl: './login.page.css'
})
export class LoginPage {
  private authService = inject(AuthService);
  private router = inject(Router);
  private translateService = inject(TranslateService);
  public navigationService = inject(NavigationService);

  public loginForm = new FormGroup({
    username: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.minLength(5), Validators.required]
    }),
    password: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.minLength(5), Validators.required]
    })
  });

  get f() { return this.loginForm.controls; }

  isLoading = signal(false);
  error = signal<string | null>(null);

  login(e: Event) {
    e.preventDefault();
    
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      this.error.set(this.translateService.instant("Login.LoginError"));
      return;
    }

    this.error.set(null);
    this.isLoading.set(true);

    const loginData: LoginRequest = this.loginForm.getRawValue() as LoginRequest;

    this.authService.login(loginData).pipe(
      finalize(() => this.isLoading.set(false))
    )
    .subscribe({
      next: (response) => {
        if (response.success) {
          this.navigationService.goToHome();
        } else {
          this.error.set(this.translateService.instant("Login.LoginError"));
        }
      },
      error: () => {
        this.error.set(this.translateService.instant("Login.HttpError"));
      }
    });
  }
}