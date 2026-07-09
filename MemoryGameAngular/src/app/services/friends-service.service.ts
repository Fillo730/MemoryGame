//Angular
import { inject, Injectable, signal } from "@angular/core";
import { HttpClient } from "@angular/common/http";

//Contants
import { getApiUrl } from "../constants/app.config";

//Models
import { Friend, FriendComparisonEntry } from "../models/entitiesDto/Friend.model";
import { FriendRequest } from "../models/entitiesDto/FriendRequest.model";
import { UserSearchResult } from "../models/entitiesDto/UserSearchResult.model";
import { FriendProfile } from "../models/entitiesDto/FriendProfile.model";
import { ApiResponse } from "../models/ApiResponse.model";

//Rxjs
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root',
})

export class FriendsService {
    private readonly API_URL = getApiUrl("FRIENDS");

    private http = inject(HttpClient);

    public pendingRequestsCount = signal<number>(0);

    public getFriends(): Observable<ApiResponse<Friend[]>> {
        return this.http.get<ApiResponse<Friend[]>>(this.API_URL);
    }

    public getIncomingRequests(): Observable<ApiResponse<FriendRequest[]>> {
        return this.http.get<ApiResponse<FriendRequest[]>>(`${this.API_URL}/requests`);
    }

    public refreshPendingRequestsCount(): void {
        this.getIncomingRequests().subscribe({
            next: (response) => {
                if (response.success) {
                    this.pendingRequestsCount.set(response.data.length);
                }
            }
        });
    }

    public searchUsers(query: string): Observable<ApiResponse<UserSearchResult[]>> {
        return this.http.get<ApiResponse<UserSearchResult[]>>(`${this.API_URL}/search`, {
            params: { query }
        });
    }

    public sendFriendRequest(userId: number): Observable<ApiResponse<FriendRequest>> {
        return this.http.post<ApiResponse<FriendRequest>>(`${this.API_URL}/request/${userId}`, {});
    }

    public acceptRequest(friendshipId: number): Observable<ApiResponse<boolean>> {
        return this.http.post<ApiResponse<boolean>>(`${this.API_URL}/${friendshipId}/accept`, {});
    }

    public declineRequest(friendshipId: number): Observable<ApiResponse<boolean>> {
        return this.http.post<ApiResponse<boolean>>(`${this.API_URL}/${friendshipId}/decline`, {});
    }

    public removeFriend(friendshipId: number): Observable<ApiResponse<boolean>> {
        return this.http.delete<ApiResponse<boolean>>(`${this.API_URL}/${friendshipId}`);
    }

    public getFriendProfile(userId: number, historyPage: number, historyPageSize: number): Observable<ApiResponse<FriendProfile>> {
        return this.http.get<ApiResponse<FriendProfile>>(`${this.API_URL}/${userId}/profile`, {
            params: { historyPage, historyPageSize }
        });
    }

    public getFriendsComparison(): Observable<ApiResponse<FriendComparisonEntry[]>> {
        return this.http.get<ApiResponse<FriendComparisonEntry[]>>(`${this.API_URL}/comparison`);
    }
}
