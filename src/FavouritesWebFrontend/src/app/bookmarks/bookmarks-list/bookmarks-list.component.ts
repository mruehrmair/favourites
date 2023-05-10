import { Component, OnInit } from '@angular/core';
import { IBookmark } from '../bookmark';
import { BookmarksService } from '../bookmarks-service';
import { Observable, catchError, of } from 'rxjs';

@Component({
  selector: 'app-bookmarks-list',
  templateUrl: './bookmarks-list.component.html',
  styleUrls: ['./bookmarks-list.component.scss']
})
export class BookmarksListComponent implements OnInit {
  pageTitle: string = 'Bookmarks';
  errorMessage = '';
  constructor(private bookmarksService: BookmarksService) { }

  ngOnInit(): void {
    this.bookmarks$ = this.bookmarksService.loadAll()
      .pipe(
        catchError(err => {
          this.errorMessage = err;
          return of([]);
        })
      );
  }

  bookmarks$: Observable<IBookmark[]> | undefined;
}
