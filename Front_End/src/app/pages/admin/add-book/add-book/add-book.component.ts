import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Book } from '../../../../data/interfaces/book';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../../../services/category/category.service';
import { Category } from '../../../../data/interfaces/category';
import { BookService } from '../../../../services/book/book.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {
  constructor(private changeDetectorRef: ChangeDetectorRef,private readonly bookService: BookService, private readonly formBuilder: FormBuilder, private readonly categoryService: CategoryService) {

  }

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

    categories: []

  };

  loaded: boolean = false;
  //categories: FormArray;
  allCategories: Category[];

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe(
      response => {
        const serializedCategories = response as any; // Assuming response is the serialized data
        const categories = serializedCategories.$values.map((serializedCategory: any) => serializedCategory as Category);

        this.allCategories = categories as Category[];
        //this.categories = this.buildCategoriesFormArray();

        const formControls = this.allCategories.map(() => this.formBuilder.control(false));

        this.changeDetectorRef.detectChanges();

        this.loaded = true;


        console.log(this.allCategories);
      }
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
      categories: this.formBuilder.array([])

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
  get categories() { return this.insertBook.get('categories') as FormArray }

  onCategorySelectionChange(event: any): void {
    const selectedOptions = event.value as Category[]; // Assuming the option values are numbers

    // Clear the existing selection
    this.categories.clear();

    // Add the selected options to the FormArray
    selectedOptions.forEach(optionValue => {
      const categoryControl = this.formBuilder.control(optionValue);
      this.categories.push(categoryControl);
    });
  }


  Add() {
    this.book = this.insertBook.value;
    this.bookService.postBook(this.book).subscribe({
      next: book => {
        console.log(book);
      },
      error: err => {
        console.log(err);
        console.log(this.book);
      }
    });


    
  }
}
