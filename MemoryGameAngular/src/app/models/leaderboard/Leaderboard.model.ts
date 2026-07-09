import { Difficulty } from "../entitiesDto/Difficulty.model";

export interface TopPlayer {
    username: string;
    gamesCompleted: number;
}

export interface DifficultyGamesCount {
    difficulty: Difficulty;
    gamesPlayed: number;
}

export interface BestScoreEntry {
    username: string;
    moves: number;
}

export interface DifficultyBestScores {
    difficulty: Difficulty;
    topScores: BestScoreEntry[];
}

export interface BestTimeEntry {
    username: string;
    durationSeconds: number;
}

export interface DifficultyBestTimes {
    difficulty: Difficulty;
    topTimes: BestTimeEntry[];
}

export interface Leaderboard {
    topPlayers: TopPlayer[];
    gamesPerDifficulty: DifficultyGamesCount[];
    bestScoresPerDifficulty: DifficultyBestScores[];
    bestTimesPerDifficulty: DifficultyBestTimes[];
}
