import { OnInit, Component, Input, ViewChild } from '@angular/core';
import { UserBooks } from '../../../models/UserBooks';
import { Book } from '../../../models/Book';
import { UserService } from '../../../services/user.service';
import { PageEvent } from '@angular/material';

@Component({
  selector: 'userBooks',
  providers: [UserService],
  templateUrl: './userBooks.component.html',
  styleUrls: ['./userBooks.component.css'],
})

export class UserBooksComponent implements OnInit {
  @Input() userId: string;
  @Input() userBooks: UserBooks;

  @Input() selectedTab : string;

  selectedIndex: number;
  indexDictionary: { [id: string] : number } = {};

  constructor(private userService: UserService) {
    this.indexDictionary["favourites"] = 0;
    this.indexDictionary["reviewed"] = 1;
    this.indexDictionary["read"] = 2;
    this.indexDictionary["likeToRead"] = 3;
  }

  ngOnInit() {
      this.selectedIndex = this.indexDictionary[this.selectedTab];
  }

  page(paging: PageEvent, collection: string) {
      this.userService.getPagedUserBooksByCollection(this.userId, collection, paging.pageIndex, paging.pageSize).subscribe( data => {
          if (collection == "favourites") {
            this.userBooks.favourites = data as Book[];
          } else
          if (collection == "reviewed") {
            this.userBooks.reviewed = data as Book[];
          } else
          if (collection == "read") {
            this.userBooks.read = data as Book[];
          } else
          if (collection == "likeToRead") {
            this.userBooks.likeToRead = data as Book[];
          }
      });
  }
}