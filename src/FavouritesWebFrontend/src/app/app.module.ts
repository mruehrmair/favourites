import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BookmarksListComponent } from './bookmarks/bookmarks-list/bookmarks-list.component';
import { HttpClientModule } from '@angular/common/http';
import { BookmarksService } from './bookmarks/bookmarks-service';

@NgModule({
  declarations: [
    AppComponent,
    BookmarksListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [BookmarksService],
  bootstrap: [AppComponent]
})
export class AppModule { }
