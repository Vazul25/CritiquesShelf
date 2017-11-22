import { Injectable, Inject  } from '@angular/core';
import { Book } from '../models/book';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
@Injectable()
export class BookService {
  
     constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }
    rootRoute: string = "api/Book/";
    //TODO baseurlel
    getBooks(page: number = 0, pageSize: number = 20, tags: string[], searchText: string): Observable<any> {
         

     
        let body = JSON.stringify({ page: page, pageSize: pageSize, tags: tags, searchText: searchText });

        return this.http.post(this.rootRoute + "getBooks", body, {
            headers: new HttpHeaders().set('Content-Type', 'application/json'),
        });
       // return this.http.get(this.rootRoute + "getBooks?page=" + page+"&&pageSize="+ pageSize);
    }
    getBookProposals(page: number, pageSize: number): Observable<any> {

        return this.http.get(this.rootRoute + "getBookProposals?page=" + page + "&&pageSize=" + pageSize);
    }
    
}