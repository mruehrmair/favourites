import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TagsService } from '../tags-service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-tags-picker',
  template: `
  <div>
  <input type="text" class="form-control" [(ngModel)]="inputValue" />
  <button (click)="addInput()" class="btn btn-secondary">Add</button>
  <select [(ngModel)]="selectedValue" (change)="addItem()" class="form-select">
    <option *ngFor="let value of dropdownValues" [value]="value">{{ value }}</option>
  </select>
  </div>
  `,
  styleUrls: ['./tags-picker.component.scss']
})
export class TagsPickerComponent implements OnInit {
  dropdownValues: string[] = [];
  selectedValue: string | undefined;
  inputValue: string | undefined;
  selectedItems: string[] = [];
  errorMessage: any;

  constructor(private tagsService: TagsService) { }

  @Output() selectedItemsChange: EventEmitter<string[]> = new EventEmitter<string[]>();

  ngOnInit(): void {
    this.tagsService.loadAll()
      .pipe(
        catchError(err => {
          this.errorMessage = err;
          return of([]);
        })
      ).subscribe((values) => { this.dropdownValues = values; console.log(values); });
  }

  addInput() {
    this.selectedValue = this.inputValue;
    this.inputValue = '';
    this.addItem();
  }

  addItem() {
    if (this.selectedValue) {
      this.selectedItems.push(this.selectedValue);
      this.dropdownValues = this.dropdownValues.filter((value) => value !== this.selectedValue);
      this.selectedValue = '';
      this.selectedItemsChange.emit(this.selectedItems); // Emit the selected items
    }
  }
}
