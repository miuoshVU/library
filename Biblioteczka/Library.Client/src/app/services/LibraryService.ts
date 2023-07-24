import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookDto } from '../interfaces/BookDto';
import { AddNewBookDto } from '../interfaces/AddNewBookDto';
import { UpdateBookDto } from '../interfaces/UpdateBookDto';
import { HttpHeaders } from '@angular/common/http';
import { BorrowDto } from '../interfaces/BorrowDto';
import { WhoBorrowBookDto } from '../interfaces/WhoBorrowDto';
import { CheckWhoReturnBook } from '../interfaces/CheckWhoReturnDto';
import { BookInstanceDto } from '../interfaces/BookInstanceDto';


@Injectable()
export class LibraryService {
    url: string = "https://localhost:7221/Library";
    url1: string = "https://localhost:7221/Spot";
    url2: string = "https://localhost:7221/Borrowed";
    url3: string = "https://localhost:7221/HowManyDaysToReturnBook";
    url4: string = "https://localhost:7221/RenewABook";

    constructor(private http: HttpClient) { }
    
    headers = new HttpHeaders().set('Authorization',  ``)

    getAllBooks(): Observable<BookDto[]> {
        return this.http.get<BookDto[]>(`${this.url}`, );
    }

    addBook(book: AddNewBookDto): Observable<void> {
        return this.http.post<void>(`${this.url}`, book);
    }

    editBook(bookId: number, book: UpdateBookDto): Observable<void> {
        return this.http.patch<void>(`${this.url}/${bookId}`, book);
    }

    deleteBook(id: number): Observable<void> {
        return this.http.delete<void>(`${this.url}/${id}`);
    }

    searchBooks(keyWord: string): Observable<BookDto[]> {
        return this.http.get<BookDto[]>(`${this.url}/Search/${keyWord}`)
    }

    getAllBooksFromCategory(categoryId: number): Observable<BookDto[]> {
        return this.http.get<BookDto[]>(`${this.url}/Category/Books/${categoryId}`)
    }

    borrowBook(QRBookCode: string, userId: string): Observable<BorrowDto> {
        let body: WhoBorrowBookDto = {
            qRBookCode: QRBookCode,
            userId: userId
        }
        return this.http.post<BorrowDto>(`${this.url}/Borrow`, body);
    }

    returnBook(userId: string, bookQrCode: string, spotQrCode: string): Observable<BorrowDto> {
        var body: CheckWhoReturnBook = { 
            userId: userId, 
            qRBookCode: bookQrCode,
            spotQrCode: spotQrCode
        }
        return this.http.patch<BorrowDto>(`${this.url}/ReturnBook`, body);
    }

    borrowedBooks(userId: string): Observable<BookInstanceDto[]> {
        return this.http.get<BookInstanceDto[]>(`${this.url2}/${userId}`);
    }

    howManyDaysToReturnBook(borrowId: number): Observable<number> {
        return this.http.get<number>(`${this.url3}/${borrowId}`);
    }

    renewABook(borrowId: number): Observable<void> {
        return this.http.patch<void>(`${this.url4}/${borrowId}`, borrowId);
    }

}