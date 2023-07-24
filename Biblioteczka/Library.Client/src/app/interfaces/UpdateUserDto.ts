import { BorrowDto } from "./BorrowDto";
import { ProposedBooksDto } from "./ProposedBooksDto";

export interface UpdateUserDto {
    firstName: string | null;
    lastName: string | null;
    email: string | null;
    role: string | null;
    avatar: URL | null;
    remainingVotes: number | null;
    paswd: string | null;
    borrows: BorrowDto[] | null;
    proposedBooks: ProposedBooksDto[];
}