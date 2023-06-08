import { Component, Input, OnInit } from '@angular/core';
import { Review } from '../../data/interfaces/review';
import { ReviewService } from '../../services/review/review.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {
  @Input() bookId: string;

  reviews: Review[] = [];

  constructor(private readonly reviewService: ReviewService, private readonly jwtHelper: JwtHelperService) {
    
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
}
