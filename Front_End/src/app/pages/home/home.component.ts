import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Book } from '../../data/interfaces/book';
import { BookService } from '../../services/book/book.service';
import { Book2 } from '../../data/interfaces/book2';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  books: Book[];
  displayedColumns: string[] = ['image', 'title', 'author', 'category'];
  constructor(private changeDetectorRef: ChangeDetectorRef, private readonly bookService: BookService) {
    this.bookService.getAllBooks().subscribe(
      books => {
        this.books = books as Book[];
        this.len = books.length;
        this.dataSource = new MatTableDataSource<Book>(this.books);
        this.dataSource.paginator = this.paginator;

      }
    );
  }
  len: number;
  ngOnInit(): void {
    console.log(this.dataSource)
  }

  dataSource: MatTableDataSource<Book>;

  ngAfterViewInit() {
  }

  

}
