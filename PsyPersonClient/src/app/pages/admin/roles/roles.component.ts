import { Component, OnInit, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { RoleDto } from 'src/app/models/roles.models';
import { RoleService } from 'src/app/services/api/role.service';
import { GetRoles } from 'src/app/store/actions/role.actions';
import { selectRoleList } from 'src/app/store/selectors/role.selector';
import { AppState } from 'src/app/store/state/app.state';
import { CreateOrEditRoleModalComponent } from './create-or-edit-role-modal/create-or-edit-role-modal.component';
import { ViewRoleModalComponent } from './view-role-modal/view-role-modal.component';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  @ViewChild('createOrEditRoleModal', { static: true })
  createOrEditRoleModal: CreateOrEditRoleModalComponent = new CreateOrEditRoleModalComponent(this.store,this.toastr,this.service);
  @ViewChild('viewRoleModal', { static: true }) viewRoleModal: ViewRoleModalComponent = new ViewRoleModalComponent(this.store); 
  roles$: Observable<PagedResponse<RoleDto> | any> = this.store.pipe(select(selectRoleList));
  filterText='';
  tableFilter: TableFilter = new TableFilter();

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:RoleService
    ) { }

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
      this.store.dispatch(new GetRoles(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage
      };
      this.store.dispatch(new GetRoles(request));
    }
    
  }

  create(){
    this.createOrEditRoleModal.show();
  }

  remove(role:RoleDto)
  {
    Swal.fire({
      title: 'Удаление пользователя',
      text: 'Вы Уверены ?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Ок',
      cancelButtonText: 'Отмена',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#7367F0',
    }).then((result) => {
      if (result.value) {
        this.service.removeRole(role.id).toPromise().then(
          (res: any) => {
            if(res.succeeded){
              this.toastr.success(`Role ${role.name} Removed!`, 'Removed successful.');
              this.onLazyLoad();
            }else{
              res.errors.forEach((element:any) => {
                switch(element.code)
                {
                  // case 'DuplicateRoleName':
                  //   this.toastr.error('Role is already taken','Assigned failed.');
                  //   break;
                  // case 'UserAlreadyInRole':
                  //   this.toastr.error('User Already Assigned To Role','Assigned failed.');
                  //   break;
                  default:
                    this.toastr.error(element.description,'Remove failed.');
                    break;
                }
              });
            }
          },
          err => {
            this.toastr.error(err,'Remove failed.');
            console.log(err)
          }
        );
      } 
    })
  }
}
