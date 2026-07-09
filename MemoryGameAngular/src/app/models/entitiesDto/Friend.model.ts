export interface Friend {
    friendshipId : number,
    userId : number,
    username : string
}

export interface FriendComparisonEntry {
    userId : number,
    username : string,
    totalGamesPlayed : number,
    bestOverallScore : number,
    isCurrentUser : boolean
}
