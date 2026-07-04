//Angular
import { computed, inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

//Rxjs
import { Observable, tap } from 'rxjs';

//Models
import { LoginResponse } from '../models/entitiesDto/LoginResponse.model';
import { LoginRequest } from '../models/requests/LoginRequest.model';

//Constants
import { getApiUrl } from '../constants/app.config';

//Utils
import { isLocalStorageValid } from '../utils/window-guart.util';
import { STORAGE_KEYS } from '../constants/storage_keys';
import { RegisterRequest } from '../models/requests/RegisterRequest.model';
import { ApiResponse } from '../models/ApiResponse.model';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private API_URL = getApiUrl("AUTH");
  private http = inject(HttpClient);

  public isLoggedIn = computed(() => {
    const user = this.currentUser();

    if(!user || !user.token) return false;

    const now = new Date();
    const expired = new Date(user.expiresDate);

    return now < expired;
  });

  private _currentUser = signal<LoginResponse | null>(this.getUserInfoInTheLocalStorage());
  public currentUser = this._currentUser.asReadonly();

  public login(loginRequest : LoginRequest) : Observable<ApiResponse<LoginResponse>> {
    return this.http.post<ApiResponse<LoginResponse>>(`${this.API_URL}/login`, loginRequest).pipe(
      tap((response) => {
        if(response.success) {
          this.updateLocalStorageInfo(response.data);
        }
      })
    ); 
  }

  public register(registerRequest : RegisterRequest) : Observable<ApiResponse<LoginResponse>> {
    return this.http.post<ApiResponse<LoginResponse>>(`${this.API_URL}/register`, registerRequest).pipe(
      tap((response) => {
        if(response.success) {
          this.updateLocalStorageInfo(response.data);
        }
      })
    );
  }

  public logout() : void {
    if(isLocalStorageValid()) {
      localStorage.removeItem(STORAGE_KEYS.USER_INFO);
      this._currentUser.set(null);
    }
  }

  public updateLocalStorageInfo(response : LoginResponse) : void {
    if(isLocalStorageValid()) {
      localStorage.setItem(STORAGE_KEYS.USER_INFO, JSON.stringify(response));
      this._currentUser.set(response);
    }
  }

  private getUserInfoInTheLocalStorage() : LoginResponse | null {
    if(isLocalStorageValid()) {
      const data = localStorage.getItem(STORAGE_KEYS.USER_INFO);
      if(!data) return null;
      
      const user : LoginResponse = JSON.parse(data);
      const now = new Date();
      const expiry = new Date(user.expiresDate);

      if(now >= expiry) {
        localStorage.removeItem(STORAGE_KEYS.USER_INFO);
        return null;
      }

      return user;
    }
    return null;
  }
}
