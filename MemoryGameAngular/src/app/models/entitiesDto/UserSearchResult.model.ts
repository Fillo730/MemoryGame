export type FriendshipSearchStatus = "None" | "PendingOutgoing" | "PendingIncoming" | "Friends" | "Declined";

export interface UserSearchResult {
    userId : number,
    username : string,
    status : FriendshipSearchStatus
}
