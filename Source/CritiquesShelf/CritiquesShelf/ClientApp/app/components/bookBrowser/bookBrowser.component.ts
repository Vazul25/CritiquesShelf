import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { BookService } from '../../services/book.service';
@Component({
    selector: 'bookBrowser',
 providers: [BookService],
    templateUrl: './bookBrowser.component.html'
})
export class BookBrowserComponent {
    

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private bookService: BookService) {
        
    }
}

 
