import { Component, Input } from '@angular/core';
import { Book } from '../../../models/Book';
import { BookService } from '../../../services/book.service';
@Component({
    selector: 'bookList',
    templateUrl: './bookList.component.html',
    styleUrls: ['./bookList.component.css'] 
})
export class BookListComponent {
     @Input() books: Book[];
     @Input() showCheckboxes: boolean = true;
}