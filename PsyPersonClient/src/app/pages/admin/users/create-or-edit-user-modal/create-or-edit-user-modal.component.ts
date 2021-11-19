import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { UserDto } from 'src/app/models/users.models';
import { UserService } from 'src/app/services/api/user.service';
import { GetUser } from 'src/app/store/actions/user.actions';
import { selectselectedUser } from 'src/app/store/selectors/user.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-create-or-edit-user-modal',
  templateUrl: './create-or-edit-user-modal.component.html',
  styleUrls: ['./create-or-edit-user-modal.component.css']
})
export class CreateOrEditUserModalComponent implements OnInit {

  @ViewChild('createOrEditModal', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<UserDto> = new EventEmitter<UserDto>();
  user$: Observable<UserDto> = this.store.pipe(select(selectselectedUser));

  active = false;
  saving = false;
  edit = false;

  user: UserDto = new UserDto();

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:UserService, 
    ) { }

  ngOnInit(): void {
  }

  show(user?: UserDto): void { 
    this.edit = false;
    
    if (!user) {
        this.user = new UserDto();

        this.active = true;
        this.modal.show();
    } else {
        this.store.dispatch(new GetUser(user.id));
        this.user$.subscribe(res => {
          let r = res; 
          this.user.id = r.id;
          this.user.userName = r.userName;
          this.user.firstName = r.firstName;
          this.user.lastName = r.lastName;
          this.user.patronymic = r.patronymic;
          this.user.email = r.email;
          this.user.phoneNumber = r.phoneNumber;
          this.user.role = r.role;
        });

        this.edit = true;
        this.active = true;
        this.modal.show();
    }
  }

  save(): void {
    this.saving = true;

    var appUser = new UserDto();

    appUser.email = this.user.email;
    appUser.firstName = this.user.firstName;
    appUser.lastName = this.user.lastName;
    appUser.patronymic = this.user.patronymic;
    appUser.phoneNumber = this.user.phoneNumber;
    appUser.userName = this.user.userName;
    appUser.id = this.user.id;
    appUser.role = this.user.role;

    if(this.edit == true){
      this.service?.update(appUser)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(res =>{
        this.toastr?.success('Saved!', 'User Saved successful.');
        this.close();
        this.modalSave.emit(this.user);
      });
    }else{
      appUser.password = this.user.password;
      this.service?.create(appUser)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(
        (res: any) => {
          if(res.succeeded){
            this.toastr?.success('New user created!', 'Created successful.');
            this.close();
            this.modalSave.emit(this.user);
          }else{
            res.errors.forEach((element:any) => {
              switch(element.code)
              {
                case 'DublicateUserName':
                  this.toastr?.error('UserName is already taken','Created failed.');
                  break;
                default:
                  this.toastr?.error(element.description,'Created failed.');
                  break;
              }
            });
          }
        },
        err => {
          console.log(err)
        }
      );
    }
  }

  close(): void {
    this.active = false;
    this.modal.hide();
  }

}
