import { Injectable } from '@angular/core';
import { Review } from '../../data/interfaces/review';
import { Observable } from 'rxjs';
import { ApiService } from '../api/api.service';
import { Review2 } from '../../data/interfaces/review2';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private readonly apiService: ApiService) { }

  private readonly route = 'Review';

  postReview(review: Review2): Observable<Review2> {
    return this.apiService.post(this.route + '/addReview', review);
  }

  getReviewsByBookId(bookId: string): Observable<Review[]> {
    return this.apiService.get(this.route + '/byBookId/' + bookId);
  }
}
