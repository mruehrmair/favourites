import { IBookmarkState } from "./bookmark.state";

export interface State {
    bookmarks: IBookmarkState;
    tags: any;
}