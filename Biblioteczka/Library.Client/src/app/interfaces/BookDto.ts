import { CategoryDto } from "./CategoryDto";
import { AuthorDto } from "./AuthorDto";

export interface BookDto {
    id: number;
    title: string;
    iSBN: string | null;
    yearOfPublication: number;
    description: string | null;
    publisher: string | null;
    cover: URL | null;
    pages: number | null;
    categories: CategoryDto[];
    authors: AuthorDto[];
}