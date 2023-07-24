export interface UpdateProposedBookDto {
    id: number | null;
    title: string | null;
    urlLink: URL | null;
    points: number | null;
    authors: string;
    categories: string;
}