//Models
import { UserRoleType } from "../types/UserRoleEnum.model"

export interface LoginResponse {
    token : string,
    username : string,
    email : string,
    userRole : UserRoleType,
    id : number,
    expiresDate : Date,
    bio : string | null,
    country : string | null,
    birthDate : Date | null
}