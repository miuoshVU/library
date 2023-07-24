import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProposedBooksDto } from '../interfaces/ProposedBooksDto';
import { UpdateProposedBookDto } from '../interfaces/UpdateProposedBookDto';

@Injectable()
export class ProposedBookService {
    url: string = "https://localhost:7221/ProposedBook";

    constructor(private http: HttpClient) { }

    getProposedBooks(): Observable<ProposedBooksDto[]> {
        return this.http.get<ProposedBooksDto[]>(`${this.url}`);
    }

    addProposedBook(proposedBook: ProposedBooksDto): Observable<void> {
        return this.http.post<void>(`${this.url}`, proposedBook)
    }

    deleteProposedBook(bookId: number): Observable<void> {
        return this.http.delete<void>(`${this.url}/${bookId}`)
    }

    editProposedBook(proposedBook: UpdateProposedBookDto) {
        return this.http.put<void>(`${this.url}/${proposedBook.id}`, proposedBook)
    }

}