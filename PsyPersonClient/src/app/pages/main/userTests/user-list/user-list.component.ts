import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { UserTestDto, UserTestUserDto } from 'src/app/models/userTests.model';
import { UserTestService } from 'src/app/services/api/userTest.service';
import { GetUserTestUsers } from 'src/app/store/actions/userTest.actions';
import { selectUserTestUsers } from 'src/app/store/selectors/userTest.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users$: Observable<PagedResponse<UserTestUserDto> | any> = this.store.pipe(select(selectUserTestUsers));
  filterText='';
  tableFilter: TableFilter = new TableFilter();

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:UserTestService,
    ) {}

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
      this.store.dispatch(new GetUserTestUsers(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage
      };
      this.store.dispatch(new GetUserTestUsers(request));
    }
  }

  filterArray(arr: UserTestDto[], isTested: boolean){
    if(isTested)
      return arr.filter(x => x.isTested === true).length;
    else
      return arr.filter(x => x.isTested === false).length;
  }

}
