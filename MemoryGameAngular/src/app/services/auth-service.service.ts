//Angular
import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

//Rxjs
import { Observable, tap } from 'rxjs';

//Models
import { LoginResponse } from '../models/entitiesDto/LoginResponse.model';
import { LoginRequest } from '../models/requests/LoginRequest.model';

//Utils
import { getApiUrl } from '../utils/ApiUrl';
import { isLocalStorageValid } from '../utils/window-guart.util';
import { STORAGE_KEYS } from '../utils/storage_keys';
import { RegisterRequest } from '../models/requests/RegisterRequest.model';
import { ApiResponse } from '../models/ApiResponse.model';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private controllerKey = "auth";
  private API_URL = getApiUrl(this.controllerKey);
  private http = inject(HttpClient);

  public isLoggedIn = signal<boolean>(this.isUserInfoInTheLocalStorage());

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
      this.isLoggedIn.set(false);
      this._currentUser.set(null);
    }
  }

  public updateLocalStorageInfo(response : LoginResponse) : void {
    if(isLocalStorageValid()) {
      localStorage.setItem(STORAGE_KEYS.USER_INFO, JSON.stringify(response));
      this.isLoggedIn.set(true);
      this._currentUser.set(response);
    }
  }

  private isUserInfoInTheLocalStorage() : boolean {
    if(isLocalStorageValid()) {
      return localStorage.getItem(STORAGE_KEYS.USER_INFO) ? true : false;
    }
    return false;
  }

  private getUserInfoInTheLocalStorage() : LoginResponse | null {
    if(isLocalStorageValid()) {
      const data = localStorage.getItem(STORAGE_KEYS.USER_INFO);
      return data ? JSON.parse(data) : null;
    }
    return null;
  }
}
