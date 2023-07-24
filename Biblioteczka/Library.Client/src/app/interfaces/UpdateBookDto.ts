export interface UpdateBookDto {
    title: string;
    iSBN: string | null;
    yearOfPublication: number | null;
    description: string | null;
    publisher: string | null;
    cover: URL | null;
    pages: number | null;
    categoryIds: number[] | null;
    authorIds: number[] | null;
}