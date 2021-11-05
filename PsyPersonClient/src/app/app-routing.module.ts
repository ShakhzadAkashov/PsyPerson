import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { StatisticsComponent } from './pages/home/statistics/statistics.component';
import { LoginComponent } from './pages/user/login/login.component';
import { RegistrationComponent } from './pages/user/registration/registration.component';
import { UserComponent } from './pages/user/user.component';
import { AuthGuard } from './services/auth/auth.guard';

const routes: Routes = [
  {path:'', redirectTo:'/user/login', pathMatch:'full'},
  {
    path: 'user', component:UserComponent,
    children:[
      {path: 'registration', component: RegistrationComponent},
      {path: 'login', component: LoginComponent}
    ]
  },
  {
    path: 'home', component: HomeComponent,canActivate:[AuthGuard],
    children:[
      {path: 'statistics', component: StatisticsComponent,canActivate:[AuthGuard] },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
