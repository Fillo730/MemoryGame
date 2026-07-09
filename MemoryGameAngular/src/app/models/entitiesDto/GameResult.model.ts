import { Difficulty } from "./Difficulty.model";

export interface GameResult {
    id? : number,
    moves : number,
    durationSeconds : number,
    difficulty : Difficulty,
    playedAt : Date
}