export interface ProposedBooksDto {
    id: number;
    title: string;
    urlLink: URL | null;
    points: number;
    cover: URL | null;
    authors: string;
    categories: string;
}