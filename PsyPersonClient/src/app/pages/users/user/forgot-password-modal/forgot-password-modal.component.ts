import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ForgotPasswordDto } from 'src/app/models/users.models';
import { UserService } from 'src/app/services/api/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-forgot-password-modal',
  templateUrl: './forgot-password-modal.component.html',
  styleUrls: ['./forgot-password-modal.component.css']
})
export class ForgotPasswordModalComponent{

  @ViewChild('forgotPasswordModal', { static: true }) modal!: ModalDirective;
  saving = false;
  forgotPassword: ForgotPasswordDto = new ForgotPasswordDto();
  private readonly ClientURI = environment.URI.ClientURI;

  constructor(private service: UserService, private toastr: ToastrService, private router: Router) { }

  show(): void { 
    this.forgotPassword = new ForgotPasswordDto();
    this.modal.show();
    this.forgotPassword.email = '';
  }

  send(): void {
    this.forgotPassword.clientURI = this.ClientURI + 'users/resetPswd/resetPassword';
    this.saving = true;
    this.service.forgotPassword(this.forgotPassword).toPromise().then(_ => {
      this.toastr.success('Ссылка была отправлена, пожалуйста, проверьте свою электронную почту, чтобы сбросить пароль.','Отправлено!');
      this.saving = false;
      this.close();
    },
    err => {
      this.toastr.error('Такого email адреса не существует в приложении.','Провал!');
      this.saving = false;
    });
  }

  close(): void {
    this.modal.hide();
  }
}
