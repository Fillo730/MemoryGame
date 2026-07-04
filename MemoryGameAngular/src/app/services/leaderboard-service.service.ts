//Angular
import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

//Contants
import { getApiUrl } from "../constants/app.config";

//Utils
import { Observable } from "rxjs";
import { ApiResponse } from "../models/ApiResponse.model";
import { Leaderboard } from "../models/leaderboard/Leaderboard.model";

@Injectable({
  providedIn: 'root',
})

export class LeaderboardService {
    private http = inject(HttpClient);

    private readonly API_URL = getApiUrl("LEADERBOARD");

    public getLeaderboard(): Observable<ApiResponse<Leaderboard>> {
        return this.http.get<ApiResponse<Leaderboard>>(this.API_URL);
    }
}
