import { Injectable, Inject  } from '@angular/core';
import { Book } from '../models/book';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class BookService {
  
     constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }
    rootRoute: string = "api/Book/";
    //TODO baseurlel
    getBooks(page: number, pageSize: number): Observable<any> {

        return this.http.get(this.rootRoute + "getBooks?page=" + page+"&&pageSize="+ pageSize);
    }

    
}