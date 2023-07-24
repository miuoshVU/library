import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryDto } from '../interfaces/CategoryDto';

@Injectable()
export class CategoryService {
    url: string = "https://localhost:7221/Category";

    constructor(private http: HttpClient) { }

    getAllCategories(): Observable<CategoryDto[]> {
        return this.http.get<CategoryDto[]>(`${this.url}`);
    }

    addCategory(category: CategoryDto): Observable<CategoryDto> {
        return this.http.post<CategoryDto>(`${this.url}`, category)
    }

    deleteCategory(categoryId: number): Observable<void> {
        return this.http.delete<void>(`${this.url}/${categoryId}`);
    }

    editCategory(category: CategoryDto): Observable<void> {
        return this.http.put<void>(`${this.url}/${category.id}`, category);
    }

}