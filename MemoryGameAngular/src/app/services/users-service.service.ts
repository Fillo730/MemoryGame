//Angular
import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

//rxjs
import { Observable, tap } from "rxjs";

//Services
import { AuthService } from "./auth-service.service";

//Constants
import { getApiUrl } from "../constants/app.config";

//Models
import { UpdateRequest } from "../models/requests/UpdateRequest.model";
import { LoginResponse } from "../models/entitiesDto/LoginResponse.model";
import { ApiResponse } from "../models/ApiResponse.model";

@Injectable({
    providedIn: 'root',
})

export class UsersService {
    private API_URL = getApiUrl("USERS");

    private http = inject(HttpClient);
    private authService = inject(AuthService);

    updateUser(updateRequest : UpdateRequest) : Observable<ApiResponse<LoginResponse>> {
        return this.http.put<ApiResponse<LoginResponse>>(`${this.API_URL}`, updateRequest).pipe(
            tap((response) => {
                if(response.success) {
                    this.authService.updateLocalStorageInfo(response.data);
                }
            })
        );
    }
}