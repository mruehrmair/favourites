import { Component } from '@angular/core';
import { IBookmark } from '../bookmark';

@Component({
  selector: 'app-bookmarks-list',
  templateUrl: './bookmarks-list.component.html',
  styleUrls: ['./bookmarks-list.component.scss']
})
export class BookmarksListComponent {
  pageTitle: string = 'Bookmarks';
  bookmarks: IBookmark[] = [
    {
      "name": "dndBeyond",
      "description": "DND Beyond and beyond",
      "webLink": "http://dndbeyond.com",
      "modificationDate": "01.01.2020",
      "tags": ["rpg", "dnd"]
    },
    {
      "name": "chatgpt",
      "description": "OpenAi ChatGPT",
      "webLink": "https://chat.openai.com/",
      "modificationDate": "01.01.2022",
      "tags": ["ai"]
    }
  ];
}
