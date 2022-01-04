import { Component, OnInit, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { BlockAndUnBlockUserResponseDto, UserDto } from 'src/app/models/users.models';
import { UserService } from 'src/app/services/api/user.service';
import { GetUsers } from 'src/app/store/actions/user.actions';
import { selectUserList } from 'src/app/store/selectors/user.selector';
import { AppState } from 'src/app/store/state/app.state';
import { CreateOrEditUserModalComponent } from './create-or-edit-user-modal/create-or-edit-user-modal.component';
import { ViewUserModalComponent } from './view-user-modal/view-user-modal.component';
import Swal from 'sweetalert2'
import { ChangePasswordModalComponent } from '../../users/user/change-password-modal/change-password-modal.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  @ViewChild('createOrEditUsersModal', { static: true })
  createOrEditUsersModal: CreateOrEditUserModalComponent = new CreateOrEditUserModalComponent(this.store,this.toastr,this.service);
  @ViewChild('viewUsersModal', { static: true }) viewUsersModal: ViewUserModalComponent = new ViewUserModalComponent(this.store); 
  @ViewChild('changePasswordModal', { static: true }) changePasswordModal: ChangePasswordModalComponent = new ChangePasswordModalComponent(this.service);
  users$: Observable<PagedResponse<UserDto> | any> = this.store.pipe(select(selectUserList));
  filterText='';
  tableFilter: TableFilter = new TableFilter();

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:UserService,
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

  create(){
    this.createOrEditUsersModal.show();
  }

  remove(user:UserDto)
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
        this.service.removeUser(user.id).toPromise().then(
          (res: any) => {
            if(res.succeeded){
              this.toastr.success(`User ${user.userName} Removed!`, 'Removed successful.');
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

  blockAndUnBlockUser(userId: string)
  {
    this.service.blockAndUnBlockUser(userId).toPromise().then((res: BlockAndUnBlockUserResponseDto) => {
      if(res.result)
      {
        if(res.isBlocked)
        {
          this.toastr.success('User Blocked!', 'Block successful.');
          this.onLazyLoad();
        }else{
          this.toastr.success('User UnBlocked!', 'UnBlock successful.');
          this.onLazyLoad();
        }
      }
      else
      {
        this.toastr.error('Block And UnBlock Failed','Failed');
      }
    },
    err => {
      if(err.status == '403'){
        this.toastr.error('You do not have permissions for this action','Block And UnBlock Failed');
        console.log(err.error)
      }else{
        this.toastr.error(err.error,'Block And UnBlock Failed');
        console.log(err.error)
      }
    });
  }

}
