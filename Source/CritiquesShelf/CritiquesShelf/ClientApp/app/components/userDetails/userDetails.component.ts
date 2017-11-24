import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { UserService } from '../../services/user.service';
import { User } from '../../models/User';
import { Review } from '../../models/Review';
import { OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'userDetails',
    providers: [UserService],
    templateUrl: './userDetails.component.html',
    styleUrls: ['./userDetails.component.css'],
})
export class UserDetailsComponent implements OnInit {
    id: string;
    user: User;

    private sub: any;

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = params['id'];
            
            this.userService.getUserById(this.id).subscribe(data => {
                this.user = data as User;
            });
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    constructor(private route: ActivatedRoute,http: Http, @Inject('BASE_URL') baseUrl: string, private userService: UserService) { }
}