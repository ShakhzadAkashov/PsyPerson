import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { ModalModule } from 'ngx-bootstrap/modal';

import { MainComponent } from './main.component';
import { MainRoutingModule } from './main-routing.module';
import { TestsComponent } from './tests/tests.component';

@NgModule({
  imports: [
      MainRoutingModule, 
      SharedModule,
      CommonModule,
      ModalModule.forRoot(),
    ],
  declarations: [
      MainComponent,
      TestsComponent,
    ],
  exports: [MainComponent]
})
export class MainModule { }