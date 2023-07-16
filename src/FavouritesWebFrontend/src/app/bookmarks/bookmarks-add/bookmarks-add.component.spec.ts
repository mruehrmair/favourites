import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BookmarksAddComponent } from './bookmarks-add.component';
import { BookmarksService } from '../bookmarks-service';
import { TagsPickerComponent } from 'src/app/tags/tags-picker/tags-picker.component';
import { TagsService } from 'src/app/tags/tags-service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { IBookmark } from '../bookmark';
import { TagsListComponent } from 'src/app/tags/tags-list/tags-list.component';

describe('BookmarksAddComponent', () => {
  let component: BookmarksAddComponent;
  let fixture: ComponentFixture<BookmarksAddComponent>;
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
      declarations: [BookmarksAddComponent, TagsPickerComponent, TagsListComponent],
      providers: [{ provide: BookmarksService, useValue: bookmarksServiceStub }, { provide: TagsService, useValue: tagsServiceStub }],
    })
      .compileComponents();

    fixture = TestBed.createComponent(BookmarksAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
