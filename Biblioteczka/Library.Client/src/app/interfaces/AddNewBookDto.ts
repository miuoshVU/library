export interface AddNewBookDto {
    title: string;
    iSBN: string | null;
    yearOfPublication: number | null;
    description: string | null;
    publisher: string | null;
    cover: URL | null;
    pages: number | null;
    authorIds: number[];
    categoryIds: number[];
}