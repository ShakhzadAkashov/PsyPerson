import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { RoleDto } from 'src/app/models/roles.models';
import { GetRoles } from 'src/app/store/actions/role.actions';
import { selectRoleList } from 'src/app/store/selectors/role.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-role-lookup-table-modal',
  templateUrl: './role-lookup-table-modal.component.html',
  styleUrls: ['./role-lookup-table-modal.component.css']
})
export class RoleLookupTableModalComponent implements OnInit {

  @ViewChild('createOrEditModal', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  roles$: Observable<PagedResponse<RoleDto> | any> = this.store.pipe(select(selectRoleList));
  tableFilter: TableFilter;

  filterText=''; 
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
        name: this.filterText ?? ''
      };
      console.log(first, rows);
      this.store.dispatch(new GetRoles(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage,
        name: this.filterText ?? ''
      };
      console.log(this.tableFilter);
      this.store.dispatch(new GetRoles(request));
    }
    
  }

  show(): void {
    this.filterText='';
    this.active = true;
    this.modal.show();
  }

  setAndSave(role: RoleDto) {
    this.active = false;
    this.modal.hide();
    this.modalSave.emit(role);
  }

  close(): void {
    this.active = false;
    this.modal.hide();
  }

  filterInput(event: any){
    if (event.key === 'Enter' || event.keyCode === 13){
      this.onLazyLoad();
    }
  }
}
