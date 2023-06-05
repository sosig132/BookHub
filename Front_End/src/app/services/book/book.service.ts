import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { Observable } from 'rxjs';
import { Book } from '../../data/interfaces/book';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private readonly apiService: ApiService) { }

  private readonly route = 'Book';

  postBook(book: Book): Observable<Book> {
    book.id = '00000000-0000-0000-0000-000000000000';
    return this.apiService.post(this.route + '/addBook', book);
  }
  getAllBooks(): Observable<Book[]> {
    return this.apiService.get(this.route);
  }
}
