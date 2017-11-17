import { Injectable, Inject  } from '@angular/core';
import { Book } from '../models/book';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class BookService {
  
     constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }
    rootRoute: string = "api/Book/";
    //TODO baseurlel
    getOrgNodes(): Observable<any> {

        return this.http.get(this.rootRoute + "getBooks");
    }

    
}