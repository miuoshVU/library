import { BookInstanceDto } from "./BookInstanceDto";
import {UserDto} from "./UserDto";

export interface BorrowDto {
    id: number;
    borrowDate: string | null;
    returnDate: string | null;
    bookInstances: BookInstanceDto[];
    userDtos: UserDto[];
}