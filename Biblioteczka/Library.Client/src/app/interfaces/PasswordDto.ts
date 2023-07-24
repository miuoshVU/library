import { UserDto } from "./UserDto";

export interface PasswordDto {
    id: string;
    salt: string;
    hash: string;
    rounds: number;
    user: UserDto;
    userId: string;
}