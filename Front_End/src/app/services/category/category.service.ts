import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { Category } from '../../data/interfaces/category';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private readonly apiService: ApiService) { }

  private readonly route = 'Category';

  postCategory(category: Category): Observable<Category> {
    category.id = '00000000-0000-0000-0000-000000000000';
    return this.apiService.post(this.route+ '/add', category);
  }
  getAllCategories(): Observable<Category[]> {
    return this.apiService.get(this.route);
  }
}
