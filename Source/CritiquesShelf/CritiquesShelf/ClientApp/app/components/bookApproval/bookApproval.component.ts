import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book';
import { UserService } from '../../services/user.service';
import { CritiquesShelfRoles } from '../../models/CritiquesShelfRoles';

import { ActivatedRoute, Router } from '@angular/router'
@Component({
    selector: 'bookApproval',
    providers: [BookService, UserService],
    templateUrl: './bookApproval.component.html',
    styleUrls: ['./bookApproval.component.css']
})
export class BookApprovalComponent implements OnInit {

    page: number;
    pageSize: number;
    books: Book[];
    hasNext: boolean;
    requestInProgress: boolean;
    numbers: number[] = Array(10);
    private sub: any;
    private sub3: any;
    ngOnInit() {


        this.requestInProgress = true;
        this.sub = this.route.url.subscribe(dontCare => {
            let sub2 = this.userService.getCurrentUserRole().subscribe(data => {

                if (data["role"] == CritiquesShelfRoles.Admin) {
                    console.log("itt1");
                    this.sub3 = this.route
                        .queryParams
                        .subscribe(params => {
                            console.log(params);
                            this.page = +params['page'] || 0;
                            this.pageSize = +params['pageSize'] || 20;

                          

                            this.refresh();


                        });
                }
                else {

                    this.router.navigateByUrl("/home");
                }
                sub2.unsubscribe();
            })

        });



    }
    ngOnDestroy() {
        console.log("Destroyed");
        if (this.sub)
            this.sub.unsubscribe();
        if (this.sub3)
            this.sub3.unsubscribe();
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
            this.router.navigate(['/bookApproval'], { queryParams: { page: this.page+1, pageSize: this.pageSize } })
           
        }
    }
    prevPage() {
        if (this.page > 0) {
            this.requestInProgress = true;
            this.router.navigate(['/bookApproval'], { queryParams: { page: this.page - 1, pageSize: this.pageSize } })

        }
    }

    private reciveResponse(data: any): void {
        console.log("itt4");
        this.books = data["data"];
        this.hasNext = data["hasNext"];
        this.page = data["page"];
        this.pageSize = data["pageSize"];
         
        this.requestInProgress = false;
        this.refreshPageNumbers();
    }

    refresh(): void {
        this.requestInProgress = true;
        this.bookService.getBookProposals(this.page, this.pageSize).subscribe(data => {

            this.reciveResponse(data);
        });
    }

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private bookService: BookService, private route: ActivatedRoute,
        private router: Router, private userService: UserService) {

    }
}


