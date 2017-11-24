import { Injectable, Inject, OnInit } from '@angular/core';
import { User } from '../models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';


@Injectable()
export class UserService {
    public userRole: string;
    public user: User;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
        this.getCurrentUser().subscribe(data => {
            this.user = data; 
        });

        this.getCurrentUserRole().subscribe(data => { 
            this.userRole = data; 
        });
    }

    rootRoute: string = "api/User/";

    //TODO baseurlel
    getCurrentUser(): Observable<any> {
        return this.http.get(this.rootRoute + "Current");
    }

    getCurrentUserRole(): Observable<any> {
        return this.http.get(this.rootRoute + "Role");
    }

    getUserById(id: string): Observable<any> {
        return this.http.get(this.rootRoute + id);
    }

    saveUser(user: User): Observable<any> {
        let body = JSON.stringify(user); 

        return this.http.post(this.rootRoute, body, {
            headers: new HttpHeaders().set('Content-Type', 'application/json'),
        });
    }

    getUserBooks(id: string) {
        return this.http.get(this.rootRoute + id + '/books');
    }
}