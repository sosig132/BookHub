import { Component, Input, OnInit } from '@angular/core';
import { Review } from '../../data/interfaces/review';
import { ReviewService } from '../../services/review/review.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Role } from '../../data/enums/role';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {
  @Input() bookId: string;

  reviews: Review[] = [];
  loggedInUserId: string;

  loggedInUserRole: string;

  content: any;

  constructor(private formBuilder : FormBuilder,private readonly reviewService: ReviewService, private readonly jwtHelper: JwtHelperService) {
    this.loggedInUserId = this.jwtHelper.decodeToken().nameid;
    this.loggedInUserRole= this.jwtHelper.decodeToken().role;
  }


  ngOnInit(): void {
    this.reviewService.getReviewsByBookId(this.bookId as string).subscribe(
      response => {
        const token = localStorage.getItem('token');
        const decodedToken = this.jwtHelper.decodeToken(token as string);
        const username = decodedToken.unique_name;
        const serializedReviews = response as any; // Assuming the reviews are in the $values property
        const reviews = serializedReviews.$values.map((serializedReview: any) => serializedReview as Review);
        this.reviews = reviews;
        
        console.log(this.reviews);

      },
      error => {
        console.log(error);
      }
    );
  }

  ifAdmin() {
    return this.loggedInUserRole==="Admin";
  }
  
  toggleEditForm(review: Review): void {
    review.showEdit = !review.showEdit;
    if (review.showEdit) {
      review.editForm = this.formBuilder.group({
        content: [review.content, Validators.required],
        rating: [review.rating, Validators.required]
      })

    }
  }
  submitEditedReview(review: Review): void {
    if (review.editForm.valid) {
      const updatedContent = review.editForm.get('content')?.value;
      const updatedRating = review.editForm.get('rating')?.value;
      review.showEdit = false;
      review.content = updatedContent;
      review.rating = updatedRating;
      review.editForm = null;
      this.reviewService.updateReview(review).subscribe(
        response => {
          review.content = updatedContent;
          console.log(response);
        },
        error => {
          console.log(error);
        });
    }
  }
  deleteReview(review: Review) {
    this.reviewService.deleteReview(review.id).subscribe(
      response => {
        const index = this.reviews.indexOf(review);
        this.reviews.splice(index, 1);
      },
      error => {
        console.log(error);
      });
  }
}
