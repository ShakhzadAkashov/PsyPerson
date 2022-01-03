import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';
import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';

@NgModule({
  imports: [
      UsersRoutingModule,
      SharedModule,
      CommonModule,
    ],
  declarations: [
    UsersComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    ],
  exports: [
    UsersComponent,
  ]
})
export class UsersModule { }