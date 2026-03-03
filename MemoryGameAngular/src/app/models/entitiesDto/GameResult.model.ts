import { Difficulty } from "./Difficulty.model";

export interface GameResult {
    id? : number,
    moves : number,
    difficulty : Difficulty,
    playedAt : Date
}