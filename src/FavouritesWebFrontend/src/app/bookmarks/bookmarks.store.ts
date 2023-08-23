import { Injectable } from '@angular/core';
import { IBookmark } from './bookmark';
import { BookmarksService } from './bookmarks-service';
import { ComponentStore } from '@ngrx/component-store';
import { exhaustMap, tap } from 'rxjs';

interface BookmarksState {
  bookmarks: IBookmark[];
}

@Injectable()
export class BookmarkStore extends ComponentStore<BookmarksState> {
  bookmarks$ = this.select((state) => state.bookmarks);

  constructor(private bookmarksService: BookmarksService) {
    super({ bookmarks: [] });
  }

  addBookmarks = this.updater((state, bookmarks: IBookmark[]) => {
    return {
      ...state,
      bookmarks,
    };
  });

  getBookmarks = this.effect((trigger$) =>
    trigger$.pipe(
      exhaustMap(() =>
        this.bookmarksService.loadAll().pipe(tap({ next: this.addBookmarks }))
      )
    )
  );
}
