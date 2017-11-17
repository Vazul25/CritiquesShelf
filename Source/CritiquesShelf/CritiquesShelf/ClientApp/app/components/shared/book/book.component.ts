import { Component, Input } from '@angular/core';
import { Book } from '../../../models/Book';
import { BookService } from '../../services/book.service';
@Component({
    selector: 'book',
    template: './book.component.html>',
})
export class BookComponent {
    @Input() book: Book;
}