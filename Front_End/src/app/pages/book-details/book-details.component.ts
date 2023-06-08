import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookService } from '../../services/book/book.service';
import { Book } from '../../data/interfaces/book';
import { Book2 } from '../../data/interfaces/book2';
import { Review } from '../../data/interfaces/review';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ReviewService } from '../../services/review/review.service';
import { JwtHelperService } from '@auth0/angular-jwt'; 
import { Review2 } from '../../data/interfaces/review2';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']

})
export class BookDetailsComponent implements OnInit {
  book: Book2;
  review: Review2 = {
    title: '',
    content: '',
    rating: 0,
    bookId: '',
    userId: '',
    dateCreated: new Date(),
    dateModified: new Date()
    
  };
  reviews: Review[] = [];
  reviewForm: FormGroup;
  bookId: string;
  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private formBuilder: FormBuilder,
    private reviewService: ReviewService,
    private jwtHelper: JwtHelperService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const bookId = params['id'];
      this.bookId = bookId;

      // Retrieve the book details based on the bookId
      this.bookService.getBookById(bookId).subscribe(
        (response: Book2) => {
          this.book = response;
          console.log(this.book);
        },
        error => {
          console.log(error);
        }
      );
    });
    this.reviewForm = this.formBuilder.group({
      content: ['', Validators.required],
      rating: [null, Validators.required]
    });

  }

  get content() { return this.reviewForm.get('content') as FormControl }
  get rating() { return this.reviewForm.get('rating') as FormControl }

  submitReview(): void {
    const token = localStorage.getItem('token') as string;
    const nameid: string = this.jwtHelper.decodeToken().nameid;
    this.review.userId = nameid;
    this.review.content = this.reviewForm.controls['content'].value;
    this.review.rating = this.reviewForm.controls['rating'].value;
    
    this.review.bookId = this.book.id;
    console.log(this.review);
    this.reviewService.postReview(this.review).subscribe(
      response => {
        console.log(response);
      },
      error => {
        console.log(error);
    });
  }
}


