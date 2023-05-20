import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BookmarksListComponent } from './bookmarks/bookmarks-list/bookmarks-list.component';
import { HttpClientModule } from '@angular/common/http';
import { BookmarksService } from './bookmarks/bookmarks-service';
import { BookmarksAddComponent } from './bookmarks/bookmarks-add/bookmarks-add.component';
import { BookmarksDeleteComponent } from './bookmarks/bookmarks-delete/bookmarks-delete.component';

@NgModule({
  declarations: [
    AppComponent,
    BookmarksListComponent,
    BookmarksAddComponent,
    BookmarksDeleteComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [BookmarksService],
  bootstrap: [AppComponent]
})
export class AppModule { }
