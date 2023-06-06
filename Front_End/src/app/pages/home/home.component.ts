import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Book } from '../../data/interfaces/book';
import { BookService } from '../../services/book/book.service';
import { Book2 } from '../../data/interfaces/book2';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Observable } from 'rxjs';
import { CategoryService } from '../../services/category/category.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  books: Book[];
  dataSource: MatTableDataSource<Book>;
  displayedColumns: string[] = ['image', 'title', 'author'];
  len: number;

  constructor(private readonly categoryService: CategoryService,private changeDetectorRef: ChangeDetectorRef, private readonly bookService: BookService) {
    this.bookService.getAllBooks().subscribe(
      response => {
        const serializedBooks = response as any; // Assuming response is the serialized data
        const books = serializedBooks.$values.map((serializedBook: any) => serializedBook as Book);
        this.books = books;
        this.len = books.length;
        this.dataSource = new MatTableDataSource<Book>(this.books);
        this.dataSource.paginator = this.paginator;
        this.changeDetectorRef.detectChanges();
      },
      error => {
        console.log(error);
      }
    );
  }
  ngOnInit(): void {
  }


  ngAfterViewInit() {
  }

  

}
