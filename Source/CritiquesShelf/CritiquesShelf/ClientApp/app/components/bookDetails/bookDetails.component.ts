import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { BookService } from '../../services/book.service';
import {  OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
@Component({
    selector: 'bookDetails',
 providers: [BookService],
    templateUrl: './bookDetails.component.html'
})
export class BookDetailsComponent {
    id: number;
    private sub: any;

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id']; // (+) converts string 'id' to a number

            // In a real app: dispatch action to load the details here.
        });
    }

    constructor(private route: ActivatedRoute,http: Http, @Inject('BASE_URL') baseUrl: string, private bookService: BookService) {
        
    }
}

 
