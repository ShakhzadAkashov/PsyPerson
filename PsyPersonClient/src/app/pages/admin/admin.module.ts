import { NgModule } from '@angular/core';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { UsersComponent } from './users/users.component';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  imports: [
      AdminRoutingModule, 
      SharedModule,
      CommonModule,
    ],
  declarations: [
      AdminComponent, 
      UsersComponent
    ],
  exports: [AdminComponent]
})
export class AdminModule { }