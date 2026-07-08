export interface UpdateRequest {
    username : string,
    email : string,
    bio : string | null,
    country : string | null,
    birthDate : string | null
}