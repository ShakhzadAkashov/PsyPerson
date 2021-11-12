import { NgModule } from '@angular/core';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@NgModule({
  imports: [
  ],
  declarations: [],
  exports: [
    TableModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
  ]
})
export class SharedModule { }