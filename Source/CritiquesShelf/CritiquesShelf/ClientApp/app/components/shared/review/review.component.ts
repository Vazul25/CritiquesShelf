import { Component, Input } from '@angular/core';
import { Review } from '../../../models/Review';
import { BookService } from '../../../services/book.service';

@Component({
    selector: 'review',
    providers: [BookService],
    templateUrl: './review.component.html',
    styleUrls: ['./review.component.css']
})
export class ReviewComponent {
    @Input() review: Review;
    @Input() readOnly: boolean = false;

    constructor(private bookService: BookService) { }

    onRatingChanged(value: number) 
    {
    	this.review.score = value;
    }

    onSendClick() {
    	this.bookService.postReviewForBook(this.review).subscribe(data => {
            console.log("PostReviewForBook: ", data);
        });
    }

    onCancelClick() {
    	console.log("Cancel clicked");
    }
}