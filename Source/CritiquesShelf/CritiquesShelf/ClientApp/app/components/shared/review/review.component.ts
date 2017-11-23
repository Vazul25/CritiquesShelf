import { Component, Input } from '@angular/core';
import { Review } from '../../../models/Review';
@Component({
    selector: 'review',
    templateUrl: './review.component.html',
    styleUrls: ['./review.component.css']
})
export class ReviewComponent {
    @Input() review: Review;
    @Input() readOnly: boolean = false;

    onRatingChanged(value: number) 
    {
    	this.review.score = value;
    }

    onSendClick() {
    	console.log("Send clicked");
    }

    onCancelClick() {
    	console.log("Cancel clicked");
    }
}