import { Component, Input } from '@angular/core';
import { Review } from '../../../models/Review';
@Component({
    selector: 'review',
    templateUrl: './review.component.html',
    styleUrls: ['./review.component.css']
})
export class ReviewComponent {
    @Input() review: Review;
}