import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Book } from '../../data/interfaces/book';
import { BookService } from '../../services/book/book.service';
import { Book2 } from '../../data/interfaces/book2';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Observable } from 'rxjs';
import { CategoryService } from '../../services/category/category.service';
import { Router } from '@angular/router';
import { Book3 } from '../../data/interfaces/book3';
import { Category } from '../../data/interfaces/category';
import { BookCategories } from '../../data/interfaces/bookCategories';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  books: Book3[];
  dataSource: MatTableDataSource<Book3>;
  displayedColumns: string[] = ['image', 'title', 'author', 'category', 'action'];
  len: number;
  searchTerm: string;
  booksToDisplay: Book3[];
  categories: Category[];
  selectedCategories: Category[];

  constructor(private readonly router: Router, private readonly categoryService: CategoryService,private changeDetectorRef: ChangeDetectorRef, private readonly bookService: BookService) {
    console.log(localStorage.getItem('banned'));
    this.bookService.getAllBooks2().subscribe(
      response => {
        const serializedBooks = response as any; // Assuming response is the serialized data
        const books = serializedBooks.$values.map((serializedBook: any) => {
          const book: Book3 = serializedBook as Book3;
          //console.log(book.bookCategories.$values);

           
            // Serialize the categories
            const categories: Category[] = book.bookCategories.$values.map(
              (serializedCategory: any) => serializedCategory.category as Category
            );
          //console.log(categories);

            // Assign the serialized categories back to the book
          book.bookCategories.categories = categories;
          //console.log(book.bookCategories.categories);
          
          return book;
        });

        this.books = books;
        this.books.forEach(book => { console.log(book.bookCategories.categories); });
        this.booksToDisplay = this.books;
        this.len = books.length;
        this.dataSource = new MatTableDataSource<Book3>(this.booksToDisplay);
        this.dataSource.paginator = this.paginator;
        this.changeDetectorRef.detectChanges();
      },
      error => {
        console.log(error);
      }
    );
  }
  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe(
      response => {
        const serializedCategories = response as any; // Assuming response is the serialized data
        const categories = serializedCategories.$values.map((serializedCategory: any) => serializedCategory as Category);
        this.categories = categories as Category[];
        console.log(this.categories);

      },
      error => {
        console.log(error);
      }
    );

  }


  ngAfterViewInit() {
  }


  performSearch() {
    const regexPattern = new RegExp(this.searchTerm, 'i');
    const filteredBooks = this.books.filter(book => regexPattern.test(book.title));
    this.booksToDisplay = filteredBooks;
    this.dataSource.data = this.booksToDisplay;
  }
  performFilter() {
    if (this.selectedCategories.length === 0) {
      this.booksToDisplay = this.books;
    } else {
      this.booksToDisplay = this.books.filter(book => {
        return this.selectedCategories.every(category => {
          return book.bookCategories.categories.some(bookCategory => bookCategory.id === category.id);
        });
      });
    }
    this.dataSource.data = this.booksToDisplay;
    
  }


  goToBook(bookId: number) {
    this.router.navigate(['/book-details', bookId]);
  }
}
