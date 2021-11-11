import { Component, OnInit } from '@angular/core';

import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse } from 'src/app/models/base';
import { UserDto } from 'src/app/models/users.models';
import { UserService } from 'src/app/services/api/user.service';
import { GetUsers } from 'src/app/store/actions/user.actions';
import { selectUserList } from 'src/app/store/selectors/user.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {

  constructor(
    private service: UserService,
    private store: Store<AppState>) { }

  list: PagedResponse<UserDto> = {data:[], total:0};
  users$: Observable<PagedResponse<UserDto>> = this.store.pipe(select(selectUserList));

  ngOnInit(): void {
    //this.getAll();
    let p: PagedRequest = {
      page: 1,
      itemPerPage: 10
    };
    this.store.dispatch(new GetUsers(p))
    console.log(this.users$)
  }

  getAll(){
    let p: PagedRequest = {
      page: 1,
      itemPerPage: 15
    };
    this.service.getAll(p).toPromise().then((res:PagedResponse<UserDto>)=>{
      this.list = res;
      console.log(this.list);
    });
  }
}
