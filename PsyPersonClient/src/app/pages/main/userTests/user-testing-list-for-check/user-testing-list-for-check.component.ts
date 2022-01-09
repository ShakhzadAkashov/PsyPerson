import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { UserTestingHistoryDto } from 'src/app/models/userTests.model';
import { UserTestService } from 'src/app/services/api/userTest.service';
import { GetUserTestingListForCheck } from 'src/app/store/actions/userTest.actions';
import { selectUserTestingListForCheck } from 'src/app/store/selectors/userTest.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-user-testing-list-for-check',
  templateUrl: './user-testing-list-for-check.component.html',
  styleUrls: ['./user-testing-list-for-check.component.css']
})
export class UserTestingListForCheckComponent implements OnInit {

  userTestingList$: Observable<PagedResponse<UserTestingHistoryDto> | any> = this.store.pipe(select(selectUserTestingListForCheck));
  tableFilter: TableFilter = new TableFilter();
  filterText='';

  constructor(
    private store: Store<AppState>,
  ) { }

  ngOnInit(): void {
  }

  filterInput(event: any){
    if (event.key === 'Enter' || event.keyCode === 13){
      this.onLazyLoad();
    }
  }

  onLazyLoad(event?: LazyLoadEvent){
    if(event)
    {
      const { first, rows } = event;
      const pageIndex = Math.ceil((first as number)/ (rows as number)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: rows as number,
        testName: this.filterText ?? ''
      };
      this.store.dispatch(new GetUserTestingListForCheck(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage,
        testName: this.filterText ?? ''
      };
      this.store.dispatch(new GetUserTestingListForCheck(request));
    }
  }
}
