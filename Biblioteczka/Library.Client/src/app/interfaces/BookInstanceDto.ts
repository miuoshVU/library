import {Status} from './Status'
import {BookDto} from './BookDto'
import {SpotDto} from './SpotDto'
import {BorrowDto} from './BorrowDto'

export interface BookInstanceDto {
    id: number;
    status: Status;
    ownerName: string;
    qR: string | null;
    book: BookDto;
    bookId: number;
    spot: SpotDto;
    spotId: number;
    borrows: BorrowDto[];
}