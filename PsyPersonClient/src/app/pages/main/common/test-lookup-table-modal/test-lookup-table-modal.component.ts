import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { TestDto } from 'src/app/models/tests.models';
import { GetTestsForLookupTable } from 'src/app/store/actions/test.actions';
import { selectTestsForLookupTable } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-test-lookup-table-modal',
  templateUrl: './test-lookup-table-modal.component.html',
  styleUrls: ['./test-lookup-table-modal.component.css']
})
export class TestLookupTableModalComponent implements OnInit {

  @ViewChild('lookupTableModal', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  tests$: Observable<PagedResponse<TestDto> | any> = this.store.pipe(select(selectTestsForLookupTable));
  tableFilter: TableFilter;

  filterText = ''; 
  userId = '';
  active = false;

  constructor(private store: Store<AppState>,) {
    this.tableFilter = new TableFilter();
    this.tableFilter.first = 0;
    this.tableFilter.itemPerPage = 5;
   }

  ngOnInit(): void {
  }

  onLazyLoad(event?: LazyLoadEvent){
    if(event)
    {
      const { first, rows} = event;
      const pageIndex = Math.ceil((first as number)/ (rows as number)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: rows as number,
        userId: this.userId ?? ''
      };
      console.log(first, rows);
      this.store.dispatch(new GetTestsForLookupTable(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage,
        userId: this.userId ?? ''
      };
      console.log(this.tableFilter);
      this.store.dispatch(new GetTestsForLookupTable(request));
    }
    
  }

  show(userId: string): void {
    this.userId = userId;
    this.filterText='';
    this.active = true;
    this.modal.show();
  }

  setAndSave(test: TestDto) {
    this.active = false;
    this.modal.hide();
    this.modalSave.emit(test);
  }

  close(): void {
    this.active = false;
    this.modal.hide();
  }

  filterInput(event: any){}
}
