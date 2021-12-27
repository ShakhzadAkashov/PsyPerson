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
import { RolesComponent } from './roles/roles.component';
import { CreateOrEditRoleModalComponent } from './roles/create-or-edit-role-modal/create-or-edit-role-modal.component';
import { ViewRoleModalComponent } from './roles/view-role-modal/view-role-modal.component';
import { UrerRolesComponent } from './roles/urer-roles/urer-roles.component';
import { RoleLookupTableModalComponent } from './common/role-lookup-table-modal/role-lookup-table-modal.component';
import { RolePermissionsComponent } from './roles/role-permissions/role-permissions.component';


@NgModule({
  imports: [
      AdminRoutingModule, 
      SharedModule,
      CommonModule,
      ModalModule.forRoot(),
    ],
  declarations: [
      AdminComponent, 
      UsersComponent, 
      CreateOrEditUserModalComponent, 
      ViewUserModalComponent, 
      RolesComponent, 
      CreateOrEditRoleModalComponent, 
      ViewRoleModalComponent, 
      UrerRolesComponent, 
      RoleLookupTableModalComponent, 
      RolePermissionsComponent
    ],
  exports: [AdminComponent]
})
export class AdminModule { }