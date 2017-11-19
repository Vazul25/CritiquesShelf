import { Injectable, Inject  } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';


@Injectable()
export class UserService {
  
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }

    rootRoute: string = "api/User/";

    //TODO baseurlel
    getCurrentUser(): Observable<any> {
        return this.http.get(this.rootRoute + "Current");
    }

    
}