import { NgModule } from '@angular/core';
import { TableModule } from 'primeng/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DataViewModule } from 'primeng/dataview';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';
import { DropdownModule } from 'primeng/dropdown';
import { PanelModule } from 'primeng/panel';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { TooltipModule } from 'primeng/tooltip';
import { TagModule } from 'primeng/tag';
import { CheckboxModule } from 'primeng/checkbox';

@NgModule({
  imports: [
  ],
  declarations: [],
  exports: [
    TableModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    DataViewModule,
    ButtonModule,
    RatingModule,
    DropdownModule,
    PanelModule,
    DialogModule,
    InputTextModule,
    RippleModule,
    TooltipModule,
    TagModule,
    CheckboxModule
  ]
})
export class SharedModule { }