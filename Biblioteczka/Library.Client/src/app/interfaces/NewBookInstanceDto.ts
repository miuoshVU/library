import { Status } from "./Status";

export interface NewBookInstanceDto {
    bookID: number | null;
    spotID: number | null;
    owner: string | null;
    status: Status | null;
}