import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { RoleDto } from 'src/app/models/roles.models';
import { RoleService } from 'src/app/services/api/role.service';
import { GetUserRoles } from 'src/app/store/actions/user.actions';
import { selectUserRolesList } from 'src/app/store/selectors/user.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-urer-roles',
  templateUrl: './urer-roles.component.html',
  styleUrls: ['./urer-roles.component.css']
})
export class UrerRolesComponent implements OnInit {

  userRoles$: Observable<PagedResponse<RoleDto> | any> = this.store.pipe(select(selectUserRolesList));
  filterText='';
  tableFilter: TableFilter = new TableFilter();
  userId = '';
  from = '';

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:RoleService,
    public activatedRoute: ActivatedRoute,
    private router: Router
  ) { 
    this.userId = this.activatedRoute.snapshot.queryParams['userId'];
    this.from = this.activatedRoute.snapshot.queryParams['from'];
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
        itemPerPage: rows as number,
      };
      request.userId = this.userId;
      this.store.dispatch(new GetUserRoles(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage
      };
      request.userId = this.userId;
      this.store.dispatch(new GetUserRoles(request));
    }
    
  }

  create(){
  }

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }

}
