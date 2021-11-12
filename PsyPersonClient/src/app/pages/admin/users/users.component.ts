import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { UserDto } from 'src/app/models/users.models';
import { GetUsers } from 'src/app/store/actions/user.actions';
import { selectUserList } from 'src/app/store/selectors/user.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users$: Observable<PagedResponse<UserDto> | any> = this.store.pipe(select(selectUserList));
  // loading: boolean = false;
  filterText='';
  tableFilter: TableFilter = new TableFilter();

  constructor(private store: Store<AppState>) {
    // let request: PagedRequest = {
    //   page: 1,
    //   itemPerPage: 10
    // };
    // this.store.dispatch(new GetUsers(request))
   }

  ngOnInit(): void {
  }

  filterInput(event: any){}

  onLazyLoad(event?: LazyLoadEvent){
    if(event)
    {
      const { first, rows } = event;
      const pageIndex = Math.ceil((first as number)/ (rows as number)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: rows as number
      };
      this.store.dispatch(new GetUsers(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage
      };
      this.store.dispatch(new GetUsers(request));
    }
    
  }

}
