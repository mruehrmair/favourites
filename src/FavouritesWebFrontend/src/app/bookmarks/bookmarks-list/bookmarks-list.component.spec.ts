import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BookmarksListComponent } from './bookmarks-list.component';
import { BookmarksService } from '../bookmarks-service';
import { Observable } from 'rxjs';
import { IBookmark } from '../bookmark';
import { BookmarksAddComponent } from '../bookmarks-add/bookmarks-add.component';
import { TagsPickerComponent } from 'src/app/tags/tags-picker/tags-picker.component';
import { TagsService } from 'src/app/tags/tags-service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TagsListComponent } from 'src/app/tags/tags-list/tags-list.component';

describe('BookmarksListComponent', () => {
  let component: BookmarksListComponent;
  let fixture: ComponentFixture<BookmarksListComponent>;
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
      imports: [FormsModule, ReactiveFormsModule],
      declarations: [ BookmarksListComponent, BookmarksAddComponent, TagsPickerComponent, TagsListComponent ],
      providers: [{ provide: BookmarksService, useValue: bookmarksServiceStub }, { provide: TagsService, useValue: tagsServiceStub }],
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookmarksListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
