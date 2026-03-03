//Models
import { UserRole } from "../UserRoleEnum.model";

export interface LoginResponse {
    token : string,
    username : string,
    email : string,
    userRole : UserRole,
    id : number,
    expiresDate : Date
}