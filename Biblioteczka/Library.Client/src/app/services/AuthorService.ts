import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorDto } from '../interfaces/AuthorDto';

@Injectable()
export class AuthorService {
    url: string = "https://localhost:7221/Author";

    constructor(private http: HttpClient) { }

    getAuthors(): Observable<AuthorDto[]> {
        return this.http.get<AuthorDto[]>(`${this.url}`);
    }

    addAuthor(author: AuthorDto): Observable<void> {
        return this.http.post<void>(`${this.url}`, author);
    }

    deleteAuthor(authorId: number): Observable<void> {
        return this.http.delete<void>(`${this.url}/${authorId}`);
    }

    editAuthor(author: AuthorDto): Observable<void> {
        return this.http.put<void>(`${this.url}/${author.id}`, author);
    }

}