import { Component, Input } from '@angular/core';
import { Book } from '../../../models/Book';
import { BookService } from '../../../services/book.service';
@Component({
    selector: 'bookDisplay',
    templateUrl: './book.component.html',
})
export class BookDisplayComponent {
    @Input() book: Book;
}