import { Difficulty } from "./Difficulty.model";

export interface PlatformStats {
    totalPlayers : number,
    totalGamesPlayed : number,
    mostPopularDifficulty : Difficulty | null
}
