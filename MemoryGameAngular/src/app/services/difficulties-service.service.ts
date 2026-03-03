//Angular
import { inject, Injectable } from "@angular/core";

//Http
import { HttpClient } from "@angular/common/http";

//Models
import { Difficulty } from "../models/entitiesDto/Difficulty.model";
import { ApiResponse } from "../models/ApiResponse.model";
import { getApiUrl } from "../utils/ApiUrl";

//Rxjs
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class DifficultiesService {
    private readonly CONTROLLER_KEY = "difficulties";

    private readonly API_URL = getApiUrl(this.CONTROLLER_KEY);

    private http = inject(HttpClient);

    getDifficulties() : Observable<ApiResponse<Difficulty[]>> {
        return this.http.get<ApiResponse<Difficulty[]>>(this.API_URL);
    }
}