import { Component, Input } from '@angular/core';
import { BookmarksService } from '../bookmarks-service';

@Component({
  selector: 'app-bookmarks-delete',
  template: `
    <a [routerLink]="" (click)="deleteBookmark()">Delete bookmark</a>
  `,
  styleUrls: ['./bookmarks-delete.component.scss']
})
export class BookmarksDeleteComponent {

  @Input()
  bookmarkName: string | undefined;

  constructor(private bookmarksService: BookmarksService) { }

  deleteBookmark() {
    if (this.bookmarkName != undefined) {
      this.bookmarksService.deleteBookmark(this.bookmarkName).subscribe(value => console.log(value));;
    }

  }
}
