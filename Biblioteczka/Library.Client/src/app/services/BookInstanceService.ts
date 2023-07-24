import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorDto } from '../interfaces/AuthorDto';
import { BookInstanceDto } from '../interfaces/BookInstanceDto';
import { NewBookInstanceDto } from '../interfaces/NewBookInstanceDto';
import { UpdateBookInstanceDto } from '../interfaces/UpdateBookInstanceDto';

@Injectable()
export class BookInstanceService {
    url: string = "https://localhost:7221/BookInstance";
    url1: string = "https://localhost:7221/BookInstance/GetAll";

    constructor(private http: HttpClient) { }

    addBookInstance(book: NewBookInstanceDto)
        : Observable<boolean> {
        return this.http.post<boolean>(`${this.url}`, book);
    }

    getBookInstances(): Observable<BookInstanceDto[]> {
        return this.http.get<BookInstanceDto[]>(`${this.url1}`);
    }

    editBookInstance(book: UpdateBookInstanceDto): Observable<void> {
        return this.http.put<void>(`${this.url}/${book.bookID}`, book);
    }

    getBookInstancesById(bookId: number): Observable<BookInstanceDto[]> {
        return this.http.get<BookInstanceDto[]>(`${this.url}/${bookId}`);
    }

    deleteBookInstance(bookId: number): Observable<void> {
        return this.http.delete<void>(`${this.url}/${bookId}`);
    }

}