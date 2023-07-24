import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserDto } from '../interfaces/UserDto';
import { UpdateUserDto } from '../interfaces/UpdateUserDto';

@Injectable()
export class UserService {
    url: string = "https://localhost:7221/User";

    constructor(private http: HttpClient) { }

    addUser(user: UpdateUserDto): Observable<void> {
        return this.http.post<void>(`${this.url}`, user);
    }

    deleteUser(id: string): Observable<void> {
        return this.http.delete<void>(`${this.url}/${id}`);
    }

    //   getUserByEmail(): Observable<UserDto> {
    //     return this.http.get<UserDto>(`${this.url}`)
    //   }

    getUser(): Observable<UserDto> {
        return this.http.get<UserDto>(`${this.url}`)
    }

    getAllUsers(): Observable<UserDto[]> {
        return this.http.get<UserDto[]>(`${this.url}`)
    }

    updateUser(userId: string, user: UpdateUserDto): Observable<void> {
        return this.http.put<void>(`${this.url}/${userId}`, user);
    }

}