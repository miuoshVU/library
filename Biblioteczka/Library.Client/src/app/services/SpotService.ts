import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SpotDto } from '../interfaces/SpotDto';

@Injectable()
export class SpotService {
    url: string = "https://localhost:7221/Spot";

    constructor(private http: HttpClient) { }

    getSpots(): Observable<SpotDto[]> {
        return this.http.get<SpotDto[]>(`${this.url}`);
    }

    editSpot(spot: SpotDto): Observable<void> {
        return this.http.patch<void>(`${this.url}/Update/${spot.id}`, spot)
    }

    addSpot(spot: SpotDto): Observable<void> {
        return this.http.post<void>(`${this.url}/Add`, spot)
    }

    deleteSpot(spotId: number): Observable<void> {
        return this.http.delete<void>(`${this.url}/Delete/${spotId}`)
    }

}