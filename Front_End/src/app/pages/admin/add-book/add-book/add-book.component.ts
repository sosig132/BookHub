import { Component, OnInit } from '@angular/core';
import { Book } from '../../../../data/interfaces/book';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../../../services/category/category.service';
import { Category } from '../../../../data/interfaces/category';
import { BookService } from '../../../../services/book/book.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {
  constructor(private readonly bookService: BookService, private readonly formBuilder: FormBuilder, private readonly categoryService: CategoryService) { }

  insertBook: FormGroup;

  book: Book = {
    id: '',
    title: '',
    author: '',
    description: '',
    isbn: '',
    image: '',
    publisher: '',
    language: '',
    pageCount: 0,

    category: ''

  };

  categories: Category[];


  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe(
      categories => this.categories = categories.sort() as Category[]
    );
    this.insertBook = this.formBuilder.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      description: ['', Validators.required],
      isbn: ['', Validators.required],
      image: [''],
      publisher: [''],
      language: [''],
      pageCount: [''],
      category: ['']
    });
  }

  get title() { return this.insertBook.get('title') as FormControl }
  get author() { return this.insertBook.get('author') as FormControl }
  get description() { return this.insertBook.get('description') as FormControl }
  get isbn() { return this.insertBook.get('isbn') as FormControl }
  get image() { return this.insertBook.get('image') as FormControl }
  get publisher() { return this.insertBook.get('publisher') as FormControl }
  get language() { return this.insertBook.get('language') as FormControl }
  get pageCount() { return this.insertBook.get('pageCount') as FormControl }
  get category() { return this.insertBook.get('category') as FormControl }


  Add() {
    this.book = this.insertBook.value;
    if (this.insertBook.valid) {
      this.bookService.postBook(this.book).subscribe({
        next: book => {
          console.log(book);
        },
        error: err => {
          console.log(err);
        }
      });

    }
  }
}
