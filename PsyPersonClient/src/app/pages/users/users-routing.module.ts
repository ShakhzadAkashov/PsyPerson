import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { ResetPasswordModalComponent } from './user/reset-password/reset-password-modal/reset-password-modal.component';
import { ResetPasswordComponent } from './user/reset-password/reset-password.component';
import { UserComponent } from './user/user.component';
import { UsersComponent } from './users.component';

const routes: Routes = [ 
    {
        path: '', component: UsersComponent,
        children: [
            {
                path: 'user', component: UserComponent,
                children: [
                    { path: 'registration', component: RegistrationComponent },
                    { path: 'login', component: LoginComponent }
                ]
            },
            {
                path: 'resetPswd', component: ResetPasswordComponent,
                children:[
                  {path: 'resetPassword', component: ResetPasswordModalComponent}
                ]
            },
            { path: '', redirectTo: 'user/login', pathMatch: 'full' },
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }