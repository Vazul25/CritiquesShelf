import { Injectable, Inject, OnInit, EventEmitter } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
 



@Injectable()
export class DataStorageService  {
    public tagsChanged$: EventEmitter<any>;
    rootRoute: string = "api/Tag/";
    private _baseUrl: string;
    public storage: any;
    public tags: string[]; 
    get isEmpty(): boolean {
        return this.storage === null || this.storage === undefined;
    }
    constructor(private http: HttpClient,  @Inject('BASE_URL') baseUrl: string) {
        this._baseUrl = baseUrl;
        this.tagsChanged$ = new EventEmitter();
        this.getTags().subscribe(data => { this.tags = data; console.log(data); console.log(this.tags); this.tagsChanged$.emit("");});
    }
    getTags()  : Observable<any>{

        return this.http.get(this.rootRoute + "getTags");
    }
}