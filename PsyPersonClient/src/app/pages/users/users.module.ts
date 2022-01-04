import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';
import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';
import { ResetPasswordComponent } from './user/reset-password/reset-password.component';
import { ForgotPasswordModalComponent } from './user/forgot-password-modal/forgot-password-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ResetPasswordModalComponent } from './user/reset-password/reset-password-modal/reset-password-modal.component';

@NgModule({
  imports: [
      UsersRoutingModule,
      SharedModule,
      CommonModule,
      ModalModule.forRoot(),
    ],
  declarations: [
    UsersComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    ResetPasswordComponent,
    ForgotPasswordModalComponent,
    ResetPasswordModalComponent,
    ],
  exports: [
    UsersComponent,
  ]
})
export class UsersModule { }