import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { RolePermissionsComponent } from './roles/role-permissions/role-permissions.component';
import { RolesComponent } from './roles/roles.component';
import { UrerRolesComponent } from './roles/urer-roles/urer-roles.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
  { path: '', component: AdminComponent },
  { path: 'users', component:UsersComponent},
  { path: 'roles', component:RolesComponent},
  { path: 'userRoles', component:UrerRolesComponent},
  { path: 'rolePermissions', component:RolePermissionsComponent},
  {
    path: '',
    redirectTo: 'users',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }