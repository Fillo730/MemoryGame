//Angular
import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";


//Utils
import { getApiUrl } from "../utils/ApiUrl";
import { GameResult } from "../models/entitiesDto/GameResult.model";
import { Observable } from "rxjs";
import { ApiResponse } from "../models/ApiResponse.model";
import { UserStats } from "../models/stats/userStats.dto";


@Injectable({
  providedIn: 'root',
})

export class GameResultsService {
    private http = inject(HttpClient);

    private readonly controllerKey : string = "gameResults"
    private readonly API_URL = getApiUrl(this.controllerKey);

    public saveGameResults(gameResult : GameResult) : Observable<ApiResponse<GameResult>> {
        return this.http.post<ApiResponse<GameResult>>(this.API_URL, gameResult)
    }

    public getGameResults(): Observable<ApiResponse<GameResult[]>> {
        return this.http.get<ApiResponse<GameResult[]>>(this.API_URL);
    }

    public getUserStats() : Observable<ApiResponse<UserStats[]>> {
        return this.http.get<ApiResponse<UserStats[]>>(`${this.API_URL}/userStats`);
    }
}
