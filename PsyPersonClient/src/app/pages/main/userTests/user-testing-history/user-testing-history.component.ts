import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, TableFilter } from 'src/app/models/base';
import { TestingHistoryDto } from 'src/app/models/userTests.model';
import { GetTestingHistory } from 'src/app/store/actions/userTest.actions';
import { selectTestingHistory } from 'src/app/store/selectors/userTest.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-user-testing-history',
  templateUrl: './user-testing-history.component.html',
  styleUrls: ['./user-testing-history.component.css']
})
export class UserTestingHistoryComponent implements OnInit {

  testingHistory$: Observable<TestingHistoryDto | any> = this.store.pipe(select(selectTestingHistory));
  userTestingHistoryId: string = '';
  from: string = '';
  tableFilter: TableFilter = new TableFilter();

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { 
    this.userTestingHistoryId = this.activatedRoute.snapshot.queryParams['userTestingHistoryId'];
    this.from = this.activatedRoute.snapshot.queryParams['from'];
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
        itemPerPage: rows as number,
        userTestingHistoryId: this.userTestingHistoryId
      };
      this.store.dispatch(new GetTestingHistory(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage,
        userTestingHistoryId: this.userTestingHistoryId
      };
      this.store.dispatch(new GetTestingHistory(request));
    }
  }

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }

}
