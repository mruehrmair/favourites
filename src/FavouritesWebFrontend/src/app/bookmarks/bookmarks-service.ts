import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import ROUTES from '../app.routes';
import { IBookmark } from './bookmark';

@Injectable()
export class BookmarksService {
  constructor(private readonly httpClient: HttpClient) {}

  loadAll(): Observable<IBookmark[]> {
    return this.httpClient.get<IBookmark[]>(ROUTES.loadAll());
  }

//   addArticle(): Observable<Article[]> {
//     return this.httpClient.post<Article[]>(ROUTES.addArticle(), null);
//   }

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
