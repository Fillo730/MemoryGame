import { Achievement } from "./Achievement.model";
import { GameResult } from "./GameResult.model";
import { UserStats } from "../stats/userStats.dto";
import { PagedResult } from "../PagedResult.model";

export interface FriendProfile {
    userId : number,
    username : string,
    achievements : Achievement[],
    stats : UserStats[],
    history : PagedResult<GameResult>
}
