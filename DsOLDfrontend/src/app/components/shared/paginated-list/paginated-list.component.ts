import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-paginated-list',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="paginated-container">
      <ng-content></ng-content>
      
      <div *ngIf="!isLoading && pagination.totalRecords > 0" class="pagination">
        <button (click)="prevPage()" [disabled]="pagination.pageNumber === 1">Previous</button>
        <span>Page {{ pagination.pageNumber }}</span>
        <button (click)="nextPage()" [disabled]="pagination.pageNumber * pagination.pageSize >= pagination.totalRecords">Next</button>
      </div>
    </div>
  `,
  styles: [`
    .pagination { margin-top: 1rem; }
    button { margin: 0 0.5rem; }
  `]
})
export class PaginatedListComponent<T> {
  @Input() isLoading = false;
  @Input() pagination = {
    pageNumber: 1,
    pageSize: 10,
    totalRecords: 0
  };

  prevPage() { this.pagination.pageNumber--; }
  nextPage() { this.pagination.pageNumber++; }
}