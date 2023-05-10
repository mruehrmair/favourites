import { Component, OnInit } from '@angular/core';
import { IBookmark } from '../bookmark';
import { BookmarksService } from '../bookmarks-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-bookmarks-list',
  templateUrl: './bookmarks-list.component.html',
  styleUrls: ['./bookmarks-list.component.scss']
})
export class BookmarksListComponent implements OnInit {
  pageTitle: string = 'Bookmarks';
  sub!: Subscription;
  errorMessage = '';
  constructor(private bookmarksService: BookmarksService) { }

  ngOnInit(): void {
    this.sub = this.bookmarksService.loadAll().subscribe({
      next: bookmarks => {
        this.bookmarks = bookmarks;
      },
      error: err => this.errorMessage = err
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
  
  bookmarks: IBookmark[] = [];
}
