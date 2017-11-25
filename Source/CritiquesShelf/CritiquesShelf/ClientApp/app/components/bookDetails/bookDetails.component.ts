import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { BookService } from '../../services/book.service';
import { UserService } from '../../services/user.service';
import { OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookDetails } from '../../models/BookDetail';
import { Review } from '../../models/Review';
import { User } from '../../models/User';
@Component({
    selector: 'bookDetails',
    providers: [BookService],
    templateUrl: './bookDetails.component.html'
})
export class BookDetailsComponent {
    id: number;
    private sub: any;
    book: BookDetails;
    myId: number;
    newReview: Review;
    me: User;
    userParsed: boolean = false;
    bookParsed: boolean = false;
    bookRecived: boolean = false;
    emptyString: string = "";
    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id'];
            this.newReview = {
                bookId: 0,
                bookTitle: "",
                date: new Date(),
                description: "",
                score: 0,
                userId: "",
                userName: "",
                id: 0
            }
            this.bookService.getBookDetails(this.id).subscribe(data => {

                this.book = data;
                this.newReview.bookTitle = this.book.title;
                this.newReview.bookId = this.book.id;
               
                this.bookRecived = true;
                this.bookParsed = true;

            });
            if (this.userService.user) {
                this.me = this.userService.user;
                this.newReview.userName = this.me.email, this.newReview.userId = this.me.id;
                this.userParsed = true;
            }
            else this.userService.getCurrentUser().subscribe(d => {
                this.me = d;
                this.newReview.userName = this.me.email, this.newReview.userId = this.me.id;
                this.userParsed = true;
            });
        });

    }

    constructor(private route: ActivatedRoute, http: Http, @Inject('BASE_URL') baseUrl: string, private bookService: BookService,
        private userService: UserService) {

    }
}


