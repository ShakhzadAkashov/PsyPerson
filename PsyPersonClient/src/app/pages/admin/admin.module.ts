import { NgModule } from '@angular/core';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { UsersComponent } from './users/users.component';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { CreateOrEditUserModalComponent } from './users/create-or-edit-user-modal/create-or-edit-user-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ViewUserModalComponent } from './users/view-user-modal/view-user-modal.component';


@NgModule({
  imports: [
      AdminRoutingModule, 
      SharedModule,
      CommonModule,
      ModalModule.forRoot(),
    ],
  declarations: [
      AdminComponent, 
      UsersComponent, CreateOrEditUserModalComponent, ViewUserModalComponent
    ],
  exports: [AdminComponent]
})
export class AdminModule { }