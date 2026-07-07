//Angular
import { inject, Injectable } from "@angular/core";

//Http
import { HttpClient } from "@angular/common/http";

//Contants
import { getApiUrl } from "../constants/app.config";

//Models
import { Achievement } from "../models/entitiesDto/Achievement.model";
import { ApiResponse } from "../models/ApiResponse.model";

//Rxjs
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class AchievementsService {
    private readonly API_URL = getApiUrl("ACHIEVEMENTS");

    private http = inject(HttpClient);

    getAchievements(): Observable<ApiResponse<Achievement[]>> {
        return this.http.get<ApiResponse<Achievement[]>>(this.API_URL);
    }
}
