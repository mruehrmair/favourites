import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-tags-list',
  template: `
   <button type="button" *ngFor="let item of tags" (click)="deleteTag(item)" class="btn btn-secondary btn-sm" style="margin-right: 2px;">
                                {{ item }} 
   </button>
  `
})
export class TagsListComponent {

  @Input() tags: string[] = [];

  deleteTag(tag: string) {
    this.tags = this.tags.filter((value) => value !== tag);
    console.log(this.tags);
  }

}
