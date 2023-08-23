import { Component, OnInit } from '@angular/core';
import { IBookmark } from '../bookmark';
import { BookmarksService } from '../bookmarks-service';
import { BehaviorSubject, Observable, catchError, of } from 'rxjs';
import { BookmarkStore } from '../bookmarks.store';

@Component({
  selector: 'app-bookmarks-list',
  templateUrl: './bookmarks-list.component.html',
  styleUrls: ['./bookmarks-list.component.scss'],
  providers: [BookmarkStore]
})
export class BookmarksListComponent implements OnInit {
  pageTitle: string = 'Bookmarks';
  errorMessage = '';
  bookmarks$ = this.bookmarkStore.bookmarks$;
  selectedBookmark$ = new BehaviorSubject<IBookmark>({} as IBookmark);
  constructor(private bookmarkStore: BookmarkStore ) { }

  ngOnInit(): void {
    this.bookmarkStore.getBookmarks();
  }
  selectBookmark(bookmark?: IBookmark): void {
    if (bookmark) {
      this.selectedBookmark$.next(bookmark);
    }
    else {
      this.selectedBookmark$.next({} as IBookmark);
    }
  };

}
