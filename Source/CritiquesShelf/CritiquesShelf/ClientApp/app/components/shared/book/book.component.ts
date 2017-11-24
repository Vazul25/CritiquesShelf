import { Component, Input, OnInit } from '@angular/core';
import { Book } from '../../../models/Book';
import { BookService } from '../../../services/book.service';
@Component({
    selector: 'bookDisplay',
    templateUrl: './book.component.html',
    styleUrls: ['./book.component.css']
})
export class BookDisplayComponent implements OnInit {
    ngOnInit(): void {
        console.log(this.book);
    }
    @Input() book: Book;

    requestInProgress: boolean = false;

    constructor(private bookService: BookService) {

    }

    favouritesToggled() {
        this.requestInProgress = true;
        this.book.favourite = !this.book.favourite;
        if (this.book.favourite === true) {   this.addToFavourites(); }
        else {   this.removeFromFavourites(); }
    };
    readToggled() {
        this.requestInProgress = true;
        this.book.read = !this.book.read;
        if (this.book.read === true) { this.addToRead(); }
        else this.removeFromRead();
    };
    likeToReadToggled() {
        this.requestInProgress = true;
        this.book.likeToRead = !this.book.likeToRead;
        if (this.book.likeToRead === true) this.addToLikeToRead();
        else this.removeFromLikeToRead();
    };

    addToFavourites() {

        this.bookService.addToFavourites(this.book.id).subscribe(
            dc => { this.requestInProgress = false;   },
            error => { console.log(error); this.requestInProgress = false; this.book.favourite = false; console.log("error"); },
            () => { console.log("complete"); });
    }
    addToRead() {

        this.bookService.addToRead(this.book.id).subscribe(dc => { this.requestInProgress = false; }, error => { this.requestInProgress = false; this.book.read = false; });
    }
    addToLikeToRead() {

        this.bookService.addToLikeToRead(this.book.id).subscribe(dc => { this.requestInProgress = false; }, error => { this.requestInProgress = false; this.book.likeToRead = false; });
    }
    removeFromFavourites() {

        this.bookService.removeFromFavourites(this.book.id).subscribe(dc => { this.requestInProgress = false; }, error => { this.requestInProgress = false; this.book.favourite = true; });
    }
    removeFromLikeToRead() {

        this.bookService.removeFromLikeToRead(this.book.id).subscribe(dc => { this.requestInProgress = false; }, error => { this.requestInProgress = false; this.book.likeToRead = true; });
    }
    removeFromRead() {

        this.bookService.removeFromRead(this.book.id).subscribe(dc => { this.requestInProgress = false; }, error => { this.requestInProgress = false; this.book.read = true; });
    }
}