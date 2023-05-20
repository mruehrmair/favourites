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
  //   removeArticle(articleId: string): Observable<Article[]> {
  //     return this.httpClient.delete<Article[]>(ROUTES.removeArticleFromOrderList(articleId));
  //   }

  //   changeArticleNumber(articleId: string, articleNumber: string): Observable<Article> {
  //     return this.httpClient.put<Article>(ROUTES.changeArticleNumber(articleId), { articleNumber: articleNumber });
  //   }

  //   changeArticleDescription(articleId: string, articleDescription: string): Observable<Article> {
  //     return this.httpClient.put<Article>(ROUTES.changeArticleDescription(articleId), { description: articleDescription });
  //   }

  //   changeArticleManufacturer(articleId: string, articleManufacturer: string): Observable<Article> {
  //     return this.httpClient.put<Article>(ROUTES.changeArticleManufacturer(articleId), { manufacturer: articleManufacturer });
  //   }

  //   changeArticleQuantity(articleId: string, articleQuantity: number): Observable<Article> {
  //     return this.httpClient.put<Article>(ROUTES.changeArticleQuantity(articleId), { quantity: articleQuantity });
  //   }
}
