import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { Category } from '../entities/category';
import { ContextService } from './context.service';

@Injectable()
export class CategoryService {

  constructor(private http: HttpClient,
    private context: ContextService) {
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(environment.api + "/categories", this.context.getHeaders());
  }

  getSubCategories(categoryId: number): Observable<Category[]> {
    return this.http.get<Category[]>(environment.api + "/categories/next/" + categoryId, this.context.getHeaders());
  }

  getCategoryById(id: number): Observable<Category> {
    return this.http.get<Category>(environment.api + "/categories/" + id, this.context.getHeaders());
  }

  getSubCategoryById(categoryId: number, id: number): Observable<Category> {
    return this.http.get<Category>(environment.api + "/categories/next/" + categoryId + "/" + id, this.context.getHeaders());
  }

  createCategory(category: Category): Observable<boolean> {
    return this.http.post<boolean>(environment.api + "/categories", category, this.context.getHeaders());
  }

  updateCategory(category: Category): Observable<boolean> {
    return this.http.put<boolean>(environment.api + "/categories", category, this.context.getHeaders());
  }

  deleteCategory(categoryId: number): Observable<boolean> {
    return this.http.delete<boolean>(environment.api + "/categories/" + categoryId, this.context.getHeaders());
  }

  deleteSubCategory(categoryId: number, id: number): Observable<boolean> {
    return this.http.delete<boolean>(environment.api + "/categories/" + categoryId + "/" + id, this.context.getHeaders());
  }
}
