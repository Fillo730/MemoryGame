import { Difficulty } from "../entitiesDto/Difficulty.model";

export interface UserStats {
    difficulty : Difficulty,
    totalMoves : number,
    bestScore : number,
    gamesPlayed : number,
    averageMovesPerGame : number,
}