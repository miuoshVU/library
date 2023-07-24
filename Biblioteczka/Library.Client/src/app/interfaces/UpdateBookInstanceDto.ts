import { Status } from "./Status";

export interface UpdateBookInstanceDto {
    bookID: number | null;
    spotID: number | null;
    owner: string | null;
    status: Status | null;
}