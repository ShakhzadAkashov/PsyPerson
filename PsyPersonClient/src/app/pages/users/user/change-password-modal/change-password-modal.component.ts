import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { ChangePasswordDto } from 'src/app/models/users.models';
import { UserService } from 'src/app/services/api/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-change-password-modal',
  templateUrl: './change-password-modal.component.html',
  styleUrls: ['./change-password-modal.component.css']
})
export class ChangePasswordModalComponent implements OnInit {

  @ViewChild('changePasswordModal', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  changePassword: ChangePasswordDto = new ChangePasswordDto();
  saving = false;
  isValid = true;
  showPswd = false;
  toastr: any;

  constructor(private service: UserService) { }

  ngOnInit(): void {
  }

  show(isOwner: boolean, id?: string): void { 
    this.showPswd = false;
    this.changePassword = new ChangePasswordDto();
    this.changePassword.IsOwner = isOwner;
    if (id != null) {
      this.changePassword.userId = id;
    }else{
      this.changePassword.userId = null;
    }
    this.modal.show();
  }

  save(): void {
    this.saving = true;
    this.isValid = true;

    if (this.changePassword.newPassword.length < 4) {
      this.isValid = false;
      Swal.fire({
        title: 'Ошибка!',
        text: 'Длина пароля не должна быть меньше 4 символов!',
        icon: 'error',
        confirmButtonText: 'Ок',
        confirmButtonColor: '#DC3545',
      })
      this.saving = false;
    }

    if (this.isValid) {
      this.service.changePassword(this.changePassword)
        .pipe(finalize(() => { this.saving = false; }))
        .toPromise().then((res) => {
          if(res){
            Swal.fire({
              title: 'Создано!',
              text: 'Новый пароль создан успешно!',
              icon: 'success',
              confirmButtonText: 'Ок',
              confirmButtonColor: '#DC3545',
            })
            this.close();
            this.modalSave.emit(null);
          }
          else{
            Swal.fire({
              title: 'Ошибка!',
              text: 'Изменение пароля провалилось!',
              icon: 'error',
              confirmButtonText: 'Ок',
              confirmButtonColor: '#DC3545',
            })
          }
        },
        err => {
          Swal.fire({
            title: 'Ошибка!',
            text: err.error,
            icon: 'error',
            confirmButtonText: 'Ок',
            confirmButtonColor: '#DC3545',
          })
          console.log(err.error)
        });
    }
  }

  close(): void {
    this.modal.hide();
  }

  showHidePassword(){
    this.showPswd = !this.showPswd;
  }
}
