import { PasswordDto } from "./PasswordDto";
import { BorrowDto } from "./BorrowDto";



export interface UserDto {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    role: string;
    avatar: URL | null;
    remainingVotes: number;
    password: PasswordDto;
    borrows: BorrowDto[];
    // votes: ProposedBooksUser[]; # todo
}