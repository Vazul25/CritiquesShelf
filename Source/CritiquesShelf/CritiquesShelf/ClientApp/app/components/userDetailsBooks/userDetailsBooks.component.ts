import { Inject, OnInit, OnDestroy, Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/rx';

import { UserBooks } from '../../models/UserBooks';
import { Book } from '../../models/Book';
import { UserService } from '../../services/user.service';

@Component({
    selector: 'userDetailsBooks',
    providers: [UserService],
    templateUrl: './userDetailsBooks.component.html',
    styleUrls: ['./userDetailsBooks.component.css'],
})

export class UserDetailsBooksComponent implements OnInit {
    userId: string;
    userBooks: UserBooks;
    selectedTab: string;

    private sub: any;

    constructor(private route: ActivatedRoute, private userService: UserService) { }

    ngOnInit() {
        var params = Observable.combineLatest(this.route.params, this.route.queryParams, (params: any, qparams: any) => ({ params, qparams}));

        this.sub = params.subscribe( ap  => {
            this.userId = ap.params['id'];
            this.selectedTab = ap.qparams['collection'] || 'favourites';

            this.userService.getUserBooks(this.userId).subscribe(data => {
                this.userBooks = data as UserBooks;
            });
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}