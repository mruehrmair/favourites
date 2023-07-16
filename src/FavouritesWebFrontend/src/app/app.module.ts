import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BookmarksListComponent } from './bookmarks/bookmarks-list/bookmarks-list.component';
import { HttpClientModule } from '@angular/common/http';
import { BookmarksService } from './bookmarks/bookmarks-service';
import { BookmarksAddComponent } from './bookmarks/bookmarks-add/bookmarks-add.component';
import { BookmarksDeleteComponent } from './bookmarks/bookmarks-delete/bookmarks-delete.component';
import { TagsPickerComponent } from './tags/tags-picker/tags-picker.component';
import { TagsService } from './tags/tags-service';
import { TagsListComponent } from './tags/tags-list/tags-list.component';

@NgModule({
  declarations: [
    AppComponent,
    BookmarksListComponent,
    BookmarksAddComponent,
    BookmarksDeleteComponent,
    TagsPickerComponent,
    TagsListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [BookmarksService, TagsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
