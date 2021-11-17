import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { RoleDto } from 'src/app/models/roles.models';
import { AssignRoleToUserCommand } from 'src/app/models/users.models';
import { UserService } from 'src/app/services/api/user.service';
import { GetUserRoles } from 'src/app/store/actions/user.actions';
import { selectUserRolesList } from 'src/app/store/selectors/user.selector';
import { AppState } from 'src/app/store/state/app.state';
import { RoleLookupTableModalComponent } from '../../common/role-lookup-table-modal/role-lookup-table-modal.component';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-urer-roles',
  templateUrl: './urer-roles.component.html',
  styleUrls: ['./urer-roles.component.css']
})
export class UrerRolesComponent implements OnInit {

  @ViewChild('roleLookupTableModal', { static: true }) roleLookupTableModal: RoleLookupTableModalComponent = new RoleLookupTableModalComponent(this.store);
  userRoles$: Observable<PagedResponse<RoleDto> | any> = this.store.pipe(select(selectUserRolesList));
  filterText='';
  tableFilter: TableFilter = new TableFilter();
  userId = '';
  from = '';

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:UserService,
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

  openSelectRoleModal(){
    this.roleLookupTableModal.show();
  }

  selectRole(role:RoleDto){
    let assignRole = new AssignRoleToUserCommand();
    assignRole.userId = this.userId;
    assignRole.roleId = role.id;
    assignRole.roleName = role.name

    this.service.assingRoleToUser(assignRole).toPromise().then(
      (res: any) => {
        if(res.succeeded){
          this.toastr.success('New role Assigned!', 'Assigned successful.');
          this.onLazyLoad();
        }else{
          res.errors.forEach((element:any) => {
            switch(element.code)
            {
              case 'DuplicateRoleName':
                this.toastr.error('Role is already taken','Assigned failed.');
                break;
              case 'UserAlreadyInRole':
                this.toastr.error('User Already Assigned To Role','Assigned failed.');
                break;
              default:
                this.toastr.error(element.description,'Assign failed.');
                break;
            }
          });
        }
      },
      err => {
        this.toastr.error(err,'Assigned failed.');
        console.log(err)
      }
    );
  }

  removeRoleFromUser(role: RoleDto){
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
        this.service.removeRoleFromUser(this.userId,role.id).toPromise().then(
          (res: any) => {
            if(res.succeeded){
              this.toastr.success(`Role ${role.name} Removed from User!`, 'Removed successful.');
              this.onLazyLoad();
            }else{
              res.errors.forEach((element:any) => {
                switch(element.code)
                {
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

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }

}
