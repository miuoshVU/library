import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenDto } from '../interfaces/TokenDto';

@Injectable()
export class AuthenticationService {
    url: string = "https://localhost:7221/Password";

    constructor(private http: HttpClient) { }

    getUser(mail: string, paswd: string): Observable<TokenDto> {

        return this.http.post<TokenDto>(`${this.url}`, { mail, paswd });
    }
}