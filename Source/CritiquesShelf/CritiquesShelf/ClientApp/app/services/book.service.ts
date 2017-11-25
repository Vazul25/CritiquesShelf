import { Injectable, Inject  } from '@angular/core';
import { Book } from '../models/book';
import { Review } from '../models/review';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
@Injectable()
export class BookService {
  
     constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }
    rootRoute: string = "api/Book/";
    //TODO baseurlel
    getBooks(page: number = 0, pageSize: number = 20, tags: string[], searchText: string,orderBy:string): Observable<any> {
         


        let body = JSON.stringify({ page: page, pageSize: pageSize, tags: tags, searchText: searchText, orderBy: orderBy });

        return this.http.post(this.rootRoute + "getBooks", body, {
            headers: new HttpHeaders().set('Content-Type', 'application/json'),
        });
       // return this.http.get(this.rootRoute + "getBooks?page=" + page+"&&pageSize="+ pageSize);
    }
    getBookProposals(page: number, pageSize: number): Observable<any> {

        return this.http.get(this.rootRoute + "getBookProposals?page=" + page + "&&pageSize=" + pageSize);
    }
    getBookDetails(id:number): Observable<any> {

        return this.http.get(this.rootRoute + id);
    }
    postBookProposal(title: string, description: string, tags: string[], authors: Author[],datePublished:number): Observable<any> {

        let body = JSON.stringify({ title: title, description: description, tags: tags, authors: authors, datePublished: datePublished });

        return this.http.post(this.rootRoute + "postBookProposal", body, {
            headers: new HttpHeaders().set('Content-Type', 'application/json'),
        });
    }


   addToFavourites(bookId: number ): Observable<any> {
       let body = JSON.stringify({dummy:bookId });
       return this.http.post(this.rootRoute + "addToFavourites/" + bookId, body, {
           headers: new HttpHeaders().set('Content-Type', 'application/json'),
       });
    }
   addToLikeToRead(bookId: number): Observable<any> {
       let body = JSON.stringify({ dummy: bookId });
       return this.http.post(this.rootRoute + "addToLikeToRead/" + bookId, body, {
           headers: new HttpHeaders().set('Content-Type', 'application/json'),
       });
   }
   addToRead(bookId: number): Observable<any> {
       let body = JSON.stringify({ dummy: bookId });
       return this.http.post(this.rootRoute + "addToRead/" + bookId, body, {
           headers: new HttpHeaders().set('Content-Type', 'application/json'),
       });
   }
   removeFromFavourites(bookId: number): Observable<any> {

       return this.http.delete(this.rootRoute + "removeFromFavourites/" + bookId );
   }
   removeFromLikeToRead(bookId: number): Observable<any> {

       return this.http.delete(this.rootRoute + "removeFromLikeToRead/" + bookId   );
   }
   removeFromRead(bookId: number): Observable<any> {

       return this.http.delete(this.rootRoute + "removeFromRead/" + bookId );
   }
   

   approveBookProposal(bookId: number) {
       let body = JSON.stringify({ dummy: bookId });
       return this.http.put(this.rootRoute + "approveBookProposal/" + bookId, body);
   }

   rejectBookProposal(bookId: number) {
       return this.http.delete(this.rootRoute + "rejectBookProposal/" + bookId);
   }
    postReviewForBook(review: Review): Observable<any> {
        let body = JSON.stringify(review); 

        return this.http.post(this.rootRoute + review.bookId + '/review', body, {
            headers: new HttpHeaders().set('Content-Type', 'application/json'),
        });
   } 
    getPagedBookReviews(bookId: number, page: number, pageSize: number): Observable<any> {
        return this.http.get(this.rootRoute + "getBookReviews/" + bookId+"?page=" + page + "&&pageSize=" + pageSize); 
    }
    getTrendingReviews( ): Observable<any> {
        return this.http.get(this.rootRoute + "getTrendingReviews/" );
    }
    getTrendingBooks( ): Observable<any> {
        return this.http.get(this.rootRoute + "getTrendingBooks/" );
    }
}