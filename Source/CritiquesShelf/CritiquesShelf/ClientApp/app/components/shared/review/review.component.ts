import { Component, Input } from '@angular/core';
import { Review } from '../../../models/Review';
import { BookService } from '../../../services/book.service';
import { ModalService } from '../../../services/modal.service';
@Component({
    selector: 'review',
    providers: [BookService],
    templateUrl: './review.component.html',
    styleUrls: ['./review.component.css']
})
export class ReviewComponent {
    @Input() review: Review;
    @Input() readOnly: boolean = false;
    requestInProgress: boolean = false;
    constructor(private bookService: BookService, private modalSerivce: ModalService) { }

    onRatingChanged(value: number) 
    {
    	this.review.score = value;
    }

    onSendClick() {
        this.requestInProgress = true;
        if (this.review.id == undefined || this.review.id == 0) {
            this.bookService.postReviewForBook(this.review).subscribe(data => {
                console.log("PostReviewForBook: ", data);
                this.requestInProgress = false;
                this.review.description = "";
                this.review.score = 0;

                this.modalSerivce.openSimpleMessageDialog("Success", "Your review was added, thank you.")
            }, err => {
                console.log(err); this.requestInProgress = false; this.modalSerivce.openSimpleMessageDialog("Error", "Something bad happened, please try again later")
            });
        } else {
            this.bookService.updateMyReviewForBook(this.review).subscribe(data => {
               
                this.requestInProgress = false;
                this.review.description = "";
                this.review.score = 0;

                this.modalSerivce.openSimpleMessageDialog("Success", "Your review was added, thank you.")
            }, err => {
                console.log(err); this.requestInProgress = false; this.modalSerivce.openSimpleMessageDialog("Error", "Something bad happened, please try again later")
            });
        }
    
    }

    onCancelClick() {
        this.review.description = "";
        this.review.score = 0;
    	console.log("Cancel clicked");
    }
}