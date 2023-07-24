import { UserDto } from "./UserDto";

export interface TokenDto {
    token: string | null;
    exp: string | null;
    user: UserDto | null;
}