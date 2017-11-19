import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book';

import { ActivatedRoute, Router } from '@angular/router'
@Component({
    selector: 'bookBrowser',
    providers: [BookService],
    templateUrl: './bookBrowser.component.html',
    styleUrls: ['./bookBrowser.component.css']
})
export class BookBrowserComponent implements OnInit {

    page: number ;
    pageSize: number;
    books: Book[];
    hasNext: boolean;
    requestInProgress: boolean;
    numbers: number[]=Array(10);
    private sub: any;
    ngOnInit() {
        
       
        this.requestInProgress= true;
        this.sub = this.route
            .queryParams
            .subscribe(params => {

                this.page = +params['page'] || 0;
                this.pageSize = +params['pageSize'] ||20;

                this.router.navigate(['/browse'], { queryParams: { page: this.page, pageSize: this.pageSize } })
                
                this.refresh();
                

            });
        


    }
    ngOnDestroy() {
        this.sub.unsubscribe();
    }
    refreshPageNumbers() {
        let offset = Math.max(this.page - 5, 0)
        for (let i = offset; i < this.page + 5 + Math.abs(Math.min(this.page - 5, 0)); i++) {
            this.numbers[i - offset] = i;
        }
        
        
    }
    nextPage() {
        if (this.hasNext) {
            this.requestInProgress = true;
            this.bookService.getBooks(this.page+1, this.pageSize).subscribe(data => {
                this.books = data["data"];
                this.hasNext = data["hasNext"];
                this.page = data["page"];
                this.pageSize = data["pageSize"];
                this.router.navigate(['/browse'], { queryParams: { page: this.page, pageSize: this.pageSize } })
                this.requestInProgress = false;
                this.refreshPageNumbers();
            });
        }
    }
    prevPage() {
        if (this.page > 0) {
            this.requestInProgress = true;
            this.bookService.getBooks(this.page - 1, this.pageSize).subscribe(data => {
                this.books = data["data"];
                this.hasNext = data["hasNext"];
                this.page = data["page"];
                this.pageSize = data["pageSize"];
                this.router.navigate(['/browse'], { queryParams: { page: this.page, pageSize: this.pageSize } })
                this.requestInProgress = false;
                this.refreshPageNumbers();

            });
        }
    }
    refresh(): void {
        this.requestInProgress = true;
        this.bookService.getBooks(this.page, this.pageSize).subscribe(data => {
          
            this.books = data["data"];
            this.hasNext = data["hasNext"];
            this.page = data["page"];
            this.pageSize = data["pageSize"];
            this.router.navigate(['/browse'], { queryParams: { page: this.page, pageSize: this.pageSize } })
            this.requestInProgress = false;
            this.refreshPageNumbers();
        });
    }

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private bookService: BookService, private route: ActivatedRoute,
        private router: Router) {

    }
}


