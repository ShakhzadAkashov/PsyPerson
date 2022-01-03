import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { StatisticsComponent } from './pages/home/statistics/statistics.component';
import { AuthGuard } from './services/auth/auth.guard';
import { ForbiddenComponent } from './shared/components/forbidden/forbidden.component';

const routes: Routes = [
  { path: '', redirectTo: 'users', pathMatch: 'full' },
  {
    path: 'users', loadChildren: () => import('./pages/users/users.module').then(m => m.UsersModule)
  },
  {
    path: 'home', component: HomeComponent, canActivate: [AuthGuard],
    children: [
      { path: 'statistics', component: StatisticsComponent, canActivate: [AuthGuard] },
      {
        path: 'admin', loadChildren: () => import('./pages/admin/admin.module').then(m => m.AdminModule)
      },
      {
        path: 'main', loadChildren: () => import('./pages/main/main.module').then(m => m.MainModule)
      }
    ]
  },
  { path: 'forbidden', component: ForbiddenComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
