import { Injectable, Inject, OnInit, EventEmitter } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
 



@Injectable()
export class DataStorageService  {
    public tagsChanged$: EventEmitter<any>;
    public authorsChanged$: EventEmitter<any>;
   
    private _baseUrl: string;
    public storage: any;
    public tags: string[];
    public authors: Author[]; 
    get isEmpty(): boolean {
        return this.storage === null || this.storage === undefined;
    }
    constructor(private http: HttpClient,  @Inject('BASE_URL') baseUrl: string) {
        this._baseUrl = baseUrl;
        this.tagsChanged$ = new EventEmitter();
        this.authorsChanged$ = new EventEmitter();
        this.getAuthors().subscribe(data => {
            this.authors = data;
            console.log(data);
            console.log(this.authors);
            this.authorsChanged$.emit("");
        });

        this.getTags().subscribe(data => { this.tags = data; console.log(data); console.log(this.tags); this.tagsChanged$.emit("");});
    }
    getTags()  : Observable<any>{

        return this.http.get("api/Tag/" + "getTags");
    }
    getAuthors(): Observable<any> {

        return this.http.get("api/Book/" + "getAuthors");
    }
}