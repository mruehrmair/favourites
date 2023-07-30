import { Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { BookmarksService } from '../bookmarks-service';
import { Observable, Subscription } from 'rxjs';
import { IBookmark } from '../bookmark';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-bookmarks-add',
  templateUrl: './bookmarks-add.component.html',
  styleUrls: ['./bookmarks-add.component.scss']
})

export class BookmarksAddComponent implements OnInit, OnDestroy {
  @ViewChild('closeModal') closeModal: ElementRef | undefined;
  @Input()
  selectedBookmark$!: Observable<IBookmark>;
  tags: string[] = [];
  bookmarkForm!: FormGroup;
  bookmark$: Observable<IBookmark> | undefined;
  isEdit: boolean = false;
  get title(): string {
    return this.isEdit ? 'Edit bookmark' : 'Add bookmark';
  }

  private subscriptions: Subscription[] | undefined;
  constructor(private bookmarksService: BookmarksService) {
    this.bookmarkForm = new FormGroup({
      name: new FormControl(''),
      webLink: new FormControl(''),
      description: new FormControl(''),
    });
  }

  ngOnDestroy(): void {
    for (const subscription of this.subscriptions || []) {
      subscription.unsubscribe();
    }
  }

  ngOnInit(): void {
    if (this.selectedBookmark$) {
      let sub = this.selectedBookmark$.subscribe(bookmark => {
        this.isEdit = bookmark?.name ? true : false;
        this.bookmarkForm.get('name')?.setValue(bookmark?.name);
        this.bookmarkForm.get('webLink')?.setValue(bookmark?.webLink);
        this.bookmarkForm.get('description')?.setValue(bookmark?.description);
        this.tags = bookmark?.tags || [];
      })
      this.subscriptions?.push(sub);
    }
  }

  addBookmark(): void {
    let bookmark: IBookmark = {
      modificationDate: '',
      name: this.bookmarkForm?.value.name,
      webLink: this.bookmarkForm?.value.webLink,
      description: this.bookmarkForm?.value.description,
      tags: this.tags
    };

    if (this.isEdit) {
      console.log(`Edit: ${JSON.stringify(bookmark)}`);
      this.bookmark$ = this.bookmarksService.editBookmark(bookmark);
    }
    else {
      console.log(`Add: ${JSON.stringify(bookmark)}`);
      this.bookmark$ = this.bookmarksService.addBookmark(bookmark);
    }
    let sub = this.bookmark$?.subscribe(value => console.log(value))
    this.subscriptions?.push(sub);
    this.closeModal?.nativeElement.click();
  }

  handleSelectedItemsChange(selectedItems: string[]) {
    console.log(selectedItems);
    this.tags = selectedItems;
  }
}
