import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { BookmarksListComponent } from './bookmarks/bookmarks-list/bookmarks-list.component';
import { BookmarksService } from './bookmarks/bookmarks-service';
import { TagsService } from './tags/tags-service';
import { Observable } from 'rxjs';
import { IBookmark } from './bookmarks/bookmark';
import { TagsPickerComponent } from './tags/tags-picker/tags-picker.component';
import { BookmarksAddComponent } from './bookmarks/bookmarks-add/bookmarks-add.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('AppComponent', () => {
  let bookmarksServiceStub: Partial<BookmarksService>;
  let tagsServiceStub: Partial<TagsService>;

  beforeEach(async () => {
    bookmarksServiceStub = {
      loadAll: () => new Observable<IBookmark[]>(),
    };
    tagsServiceStub = {
      loadAll: () => new Observable<string[]>(),
    };
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule, FormsModule, ReactiveFormsModule
      ],
      declarations: [
        AppComponent, BookmarksListComponent, BookmarksAddComponent, TagsPickerComponent
      ],
      providers: [{ provide: BookmarksService, useValue: bookmarksServiceStub }, { provide: TagsService, useValue: tagsServiceStub }],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'FavouritesWebFrontend'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('FavouritesWebFrontend');
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.card-header')?.textContent).toContain('Bookmarks');
  });
});
