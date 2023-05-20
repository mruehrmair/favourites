import { Component, OnInit } from '@angular/core';
import { BookmarksService } from '../bookmarks-service';
import { Observable } from 'rxjs';
import { IBookmark } from '../bookmark';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-bookmarks-add',
  templateUrl: './bookmarks-add.component.html',
  styleUrls: ['./bookmarks-add.component.scss']
})

export class BookmarksAddComponent implements OnInit {
  constructor(private bookmarksService: BookmarksService) {
    //this.bookmark$ = of({name: 'test', description: '', modificationDate: '', tags: [], webLink: ''});
    this.bookmarkForm = new FormGroup({
      name: new FormControl(''),
      webLink: new FormControl(''),
      description: new FormControl(''),
      tags: new FormControl('')
    });
  }
  bookmarkForm: FormGroup;
  //bookmark = {} as IBookmark;
  bookmark$: Observable<IBookmark> | undefined;
  ngOnInit(): void {
    //this.bookmark$?.subscribe(value => this.bookmark = value);
  }

  addBookmark(): void {
    //console.log(this.bookmarkForm.value);
    this.bookmark$ = this.bookmarksService.addBookmark({
      modificationDate: '',
      name: this.bookmarkForm.value.name,
      webLink: this.bookmarkForm.value.webLink,
      description: this.bookmarkForm.value.description,
      tags: this.bookmarkForm.value.tags.split(',')
    });
    this.bookmark$?.subscribe(value => console.log(value));
  }

}
