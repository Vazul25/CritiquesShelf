import { Component, Inject, OnInit, EventEmitter } from '@angular/core';
import { Http } from '@angular/http';
import { BookService } from '../../services/book.service';
import { DataStorageService } from '../../services/storage.service';
import { Book } from '../../models/book';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
    selector: 'bookBrowser',
    providers: [BookService, DataStorageService],
    templateUrl: './bookBrowser.component.html',
    styleUrls: ['./bookBrowser.component.css']
})
export class BookBrowserComponent implements OnInit {

    tagsFc: FormControl = new FormControl();

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private bookService: BookService, private route: ActivatedRoute, private storageService: DataStorageService,
        private router: Router, public dialog: MatDialog) {
        this.tagsChangedSubscription = this.storageService.tagsChanged$.subscribe(() => {
            console.log("change catched");
            this.tags = this.storageService.tags;
            console.log(this.tags);
        });
        this.authorsChangedSubscription = this.storageService.authorsChanged$.subscribe(() => {
            console.log("change catched");
            this.authors = this.storageService.authors;
            console.log(this.authors);
        });
    }
    authors: Author[];
    tags: string[];
    page: number;
    pageSize: number;
    books: Book[];
    hasNext: boolean;
    requestInProgress: boolean;
    searchText: string;
    numbers: number[] = Array(10);
    private sub: any;
    private tagsChangedSubscription: EventEmitter<any>;
    private authorsChangedSubscription: EventEmitter<any>;
    ngOnInit() {


        this.requestInProgress = true;
        this.sub = this.route
            .queryParams
            .subscribe(params => {

                this.page = +params['page'] || 0;
                this.pageSize = +params['pageSize'] || 20;
                this.searchText = params['searchText'] || "";
                this.tagsFc.setValue ( params['tags'] || []);
                this.authors = this.storageService.authors;
                this.tags = this.storageService.tags;
                console.log(this.tags); console.log(this.storageService.tags);
                this.refresh();


            });



    }
    ngOnDestroy() {
        this.sub.unsubscribe();
    }
    private reciveResponse(data: any): void {
        this.books = data["data"];
        this.hasNext = data["hasNext"];
        this.page = data["page"];
        this.pageSize = data["pageSize"];

        this.requestInProgress = false;
        this.refreshPageNumbers();
    }
    refreshPageNumbers() {
        let offset = Math.max(this.page - 5, 0)
        for (let i = offset; i < this.page + 5 + Math.abs(Math.min(this.page - 5, 0)); i++) {
            this.numbers[i - offset] = i;
        }
        


    }
    nextPage() {
        if (this.hasNext) {
            this.requestInProgress = true;
            this.router.navigate(['/browse'], { queryParams: { page: this.page + 1, pageSize: this.pageSize } });

        }
    }
    prevPage() {
        if (this.page > 0) {
            this.requestInProgress = true;
            this.router.navigate(['/browse'], { queryParams: { page: this.page - 1, pageSize: this.pageSize } });


        }
    }
    refresh(): void {
        this.requestInProgress = true;
        console.log(this.tagsFc.value);
        this.bookService.getBooks(this.page, this.pageSize, this.tagsFc.value, this.searchText).subscribe(data => {

            this.reciveResponse(data);
        });
       
    }
    search() {
        this.router.navigate(['/browse'], { queryParams: { page: 0, pageSize: this.pageSize, searchText: this.searchText, tags: this.tagsFc.value } });

    }


    openNewBookDialog(): void {
        console.log("dialog");

        let dialogRef = this.dialog.open(DialogNewBookProposal, {
            width: '450px',
            data: { tags: this.tags, authors: this.authors }
        });

        dialogRef.afterClosed().subscribe(result => {
            console.log('The dialog was closed');
            console.log(result);

            if (result) {

                this.bookService.postBookProposal(result.title, result.description, result.tags, result.authors, result.datePublished).subscribe();
            }
        });
        console.log(dialogRef);
    }


}

@Component({
    selector: 'dialog-new-book-proposal',
    templateUrl: 'dialog-new-book-proposal.html',
    styleUrls: ['bookBrowser.component.css']
})
export class DialogNewBookProposal implements OnInit {
    ngOnInit(): void {

        this.authorFc.setValue('');
        this.tagsFc.valueChanges.subscribe(value => { this.data.bookProposal.tags = value; });
        this.authorFc.valueChanges.subscribe(value => {

            //this.filteredAuthors = (this.data.authors as Author[]).filter(a => { a.name.toLocaleLowerCase().startsWith(value); });
            var tmp = new Array();
            tmp.push({ name: value, id: undefined });
            for (let a of this.data.authors as Author[]) {
                if (a.name.indexOf(value) !== -1) { tmp.push(a); }

            }
            this.filteredAuthors = tmp;

        });


    }



    filteredAuthors: Author[];
    tagsFc: FormControl = new FormControl();
    authorFc: FormControl = new FormControl();
    constructor(
        public dialogRef: MatDialogRef<DialogNewBookProposal>,
        @Inject(MAT_DIALOG_DATA) public data: any) {


        this.data.bookProposal = { authors: [], description: "", tags: [], title: "" , datePublished:2017};

    }

    selectAuthor(a: Author) {
        console.log("selectAuthor");
        this.authorFc.reset();
        if ((this.data.bookProposal.authors as Author[]).indexOf(a) == -1) {
            this.data.bookProposal.authors.push(a);
        }
    }

    onNoClick(): void {
        this.authorFc.reset();
        this.dialogRef.close();
    }
    removeAuthor(a: Author) {
        console.log(this.data.bookProposal.authors);
        let index = (this.data.bookProposal.authors as Author[]).indexOf(a);

        if (index > -1) {
            (this.data.bookProposal.authors as Author[]).splice(index, 1);
        }
        console.log(this.data.bookProposal.authors);
    }
}
