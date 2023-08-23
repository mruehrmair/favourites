import { createAction, createReducer, on } from '@ngrx/store';
import { IBookmark } from '../bookmarks/bookmark';

export const bookmarkReducer = createReducer(
  { bookmarks: [] as IBookmark[] },
  on(createAction('[Bookmark] Add Bookmark'), (state) => {
    console.log('original state: ' + JSON.stringify(state));
    return {
      ...state,
      bookmarks: [
        ...state.bookmarks,
        {
          name: 'New Bookmark',
          description: '',
          webLink: '',
          modificationDate: new Date().toDateString(),
          tags: [],
        },
      ],
    };
  })
);
