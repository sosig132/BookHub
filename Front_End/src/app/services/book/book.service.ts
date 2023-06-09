import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { Observable } from 'rxjs';
import { Book } from '../../data/interfaces/book';
import { Book2 } from '../../data/interfaces/book2';
import { Book3 } from '../../data/interfaces/book3';

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
  getBookById(id: string): Observable<Book2> {
    return this.apiService.get(this.route + '/byId?id=' + id);
  }
  getAllBooks2(): Observable<Book3[]> {
    return this.apiService.get(this.route);
  }
  getBookById2(id: string): Observable<Book3> {
    return this.apiService.get(this.route + '/byId?id=' + id);
  }
}
