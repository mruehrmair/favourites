import { Component, OnInit } from '@angular/core';
import { BookmarksService } from '../bookmarks-service';
import { Observable, of } from 'rxjs';
import { IBookmark } from '../bookmark';

@Component({
  selector: 'app-bookmarks-add',
  templateUrl: './bookmarks-add.component.html',
  styleUrls: ['./bookmarks-add.component.scss']
})

export class BookmarksAddComponent implements OnInit {
  constructor(private bookmarksService: BookmarksService) { 
    this.bookmark$ = of({name: 'test', description: '', modificationDate: '', tags: [], webLink: ''});
  }
  bookmark = {} as IBookmark;
  bookmark$: Observable<IBookmark> | undefined;
  ngOnInit(): void {
    this.bookmark$?.subscribe(value => this.bookmark = value);
  }

  addBookmark(): void {
    
    this.bookmark$ = this.bookmarksService.addBookmark({
      modificationDate:'',
      name: this.bookmark.name,
      webLink: this.bookmark.webLink,
      description: this.bookmark.description,
      tags: []
    });
  }
  
}
