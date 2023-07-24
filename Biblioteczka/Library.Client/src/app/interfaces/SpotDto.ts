export interface SpotDto {
    id: number;
    name: string;
    building: string;
    floor: number;
    description: string | null;
    qr: string | null;
    bookCount: number | null;
}