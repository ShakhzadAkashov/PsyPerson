import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { TestDto } from 'src/app/models/tests.model';
import { GetTests } from 'src/app/store/actions/test.actions';
import { selectTestList } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-tests',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.css']
})
export class TestsComponent implements OnInit {

  tests$: Observable<PagedResponse<TestDto> | any> = this.store.pipe(select(selectTestList));
  tableFilter: TableFilter = new TableFilter();

  constructor(
    private store: Store<AppState>,
  ) {
      this.tableFilter.itemPerPage = 8;
   }

  ngOnInit(): void {
  }

  onLazyLoad(event?: LazyLoadEvent){
    if(event)
    {
      const { first, rows } = event;
      const pageIndex = Math.ceil((first as number)/ (rows as number)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: rows as number
      };
      this.store.dispatch(new GetTests(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage
      };
      this.store.dispatch(new GetTests(request));
    }
    
  }

  create(){}
}
