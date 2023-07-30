import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import ROUTES from '../app.routes';
import { IBookmark } from './bookmark';

@Injectable()
export class BookmarksService {
  constructor(private readonly httpClient: HttpClient) { }

  loadAll(): Observable<IBookmark[]> {
    return this.httpClient.get<IBookmark[]>(ROUTES.loadAll())
      .pipe(
        catchError((err) => {
          console.error(err);
          return throwError(() => new Error('Error loading bookmarks'));
        })
      );
  }

  addBookmark(bookmark: IBookmark): Observable<IBookmark> {
    return this.httpClient.post<IBookmark>(ROUTES.addBookmark(), bookmark).pipe(
      catchError((err) => {
        console.error(err);
        return throwError(() => new Error('Error creating bookmark'));
      })
    );
  }

  deleteBookmark(bookmarkName: string): Observable<IBookmark> {
    return this.httpClient.delete<IBookmark>(ROUTES.deleteBookmark(bookmarkName)).pipe(
      catchError((err) => {
        console.error(err);
        return throwError(() => new Error('Error deleting bookmark'));
      })
    );
  }

  editBookmark(bookmark: IBookmark): Observable<IBookmark> {
    return this.httpClient.put<IBookmark>(ROUTES.editBookmark(), bookmark).pipe(
      catchError((err) => {
        console.error(err);
        return throwError(() => new Error('Error editing bookmark'));
      })
    );
  }
}
