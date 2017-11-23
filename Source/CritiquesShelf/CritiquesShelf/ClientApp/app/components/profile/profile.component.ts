import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { UserService } from '../../services/user.service';
import { User } from '../../models/User';
import { Review } from '../../models/Review';
import { OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
@Component({
    selector: 'profile',
    providers: [UserService],
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css'],
})
export class ProfileComponent {
    user: User;

    ngOnInit() {
        this.userService.getCurrentUser().subscribe(data => {
            this.user = data as User;
            this.user.reviews = [
                {
                    userId: "asd",
                    userName: 'userName',
                    description: 'description very very very description',
                    date: new Date(),
                    score: 3.1,
                    bookTitle: 'Biblia',
                    bookId: 1
                },{
                    userId: "asd",
                    userName: 'userName',
                    description: '',
                    date: new Date(),
                    score: 4,
                    bookTitle: 'Korán',
                    bookId: 2
                },{
                    userId: "asd",
                    userName: 'userName',
                    description: 'description very very very description',
                    date: new Date(),
                    score: 4.5,
                    bookTitle: 'Ószövetség',
                    bookId: 3
                }
            ];
        });
    }

    constructor(private route: ActivatedRoute,http: Http, @Inject('BASE_URL') baseUrl: string, private userService: UserService) { }
}

 
