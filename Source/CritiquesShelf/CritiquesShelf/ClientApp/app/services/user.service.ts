import { Injectable, Inject, OnInit } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';


@Injectable()
export class UserService implements OnInit {
    ngOnInit(): void {
      
    }
    public userRole: string;
    public user: User;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
       
            this.getCurrentUser().subscribe(data => { this.user = data; });
            this.getCurrentUserRole().subscribe(data => { this.userRole = data; });
         
    }

    rootRoute: string = "api/User/";

    //TODO baseurlel
    getCurrentUser(): Observable<any> {
        return this.http.get(this.rootRoute + "Current");
    }
    getCurrentUserRole(): Observable<any> {
        return this.http.get(this.rootRoute + "Role");
    }


}