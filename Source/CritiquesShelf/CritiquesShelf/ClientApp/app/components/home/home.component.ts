import { Component,OnInit } from '@angular/core';
import { Book  } from '../../models/Book';
import { Review } from '../../models/Review';
import { BookService } from '../../services/book.service';
@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    ngOnInit(): void {
        this.bookService.getTrendingBooks().subscribe(data => this.trendingBooks = data);

        this.bookService.getTrendingReviews().subscribe(data => this.trendingReviews = data);
    }
    trendingReviews: Review[];
    trendingBooks: Book[];
    constructor(private bookService: BookService) { }
}
