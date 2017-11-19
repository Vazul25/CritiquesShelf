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
    templateUrl: './profile.component.html'
})
export class ProfileComponent {
    user: User;
    review: Review

    ngOnInit() {
        this.userService.getCurrentUser().subscribe(data => {
            this.user = data as User;
        });

        this.review = {
            userName: 'userName',
            description: 'description',
            date: new Date(),
            score: 1
        }
    }

    constructor(private route: ActivatedRoute,http: Http, @Inject('BASE_URL') baseUrl: string, private userService: UserService) { }
}

 
