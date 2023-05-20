import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ROUTES from '../app.routes';
import { Observable, catchError, throwError } from "rxjs";

@Injectable()
export class TagsService {
    constructor(private readonly httpClient: HttpClient) { }

    loadAll(): Observable<string[]> {
        return this.httpClient.get<string[]>(ROUTES.loadAllTags())
            .pipe(
                catchError((err) => {
                    console.error(err);
                    return throwError(() => new Error('Error loading tags'));
                })
            );
    }
}