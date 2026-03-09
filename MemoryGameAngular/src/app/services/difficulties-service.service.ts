//Angular
import { inject, Injectable } from "@angular/core";

//Http
import { HttpClient } from "@angular/common/http";

//Contants
import { getApiUrl } from "../constants/app.config";

//Models
import { Difficulty } from "../models/entitiesDto/Difficulty.model";
import { ApiResponse } from "../models/ApiResponse.model";

//Rxjs
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class DifficultiesService {
    private readonly API_URL = getApiUrl("DIFFICULTIES");

    private http = inject(HttpClient);

    getDifficulties() : Observable<ApiResponse<Difficulty[]>> {
        return this.http.get<ApiResponse<Difficulty[]>>(this.API_URL);
    }
}