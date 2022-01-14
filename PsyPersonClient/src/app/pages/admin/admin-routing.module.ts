import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/services/auth/auth.guard';
import { AdminComponent } from './admin.component';
import { RolePermissionsComponent } from './roles/role-permissions/role-permissions.component';
import { RolesComponent } from './roles/roles.component';
import { UrerRolesComponent } from './roles/urer-roles/urer-roles.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
  { 
    path: '', component: AdminComponent, canActivate: [AuthGuard],
    children:[
      { path: 'users', component: UsersComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Users.Pages' } },
      { path: 'roles', component:RolesComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Roles.Pages' } },
      { path: 'userRoles', component:UrerRolesComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.UserRoles.Pages' } },
      { path: 'rolePermissions', component:RolePermissionsComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Permission.Pages' } },
      { path: '', redirectTo: 'users', pathMatch: 'full', }
    ] 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }