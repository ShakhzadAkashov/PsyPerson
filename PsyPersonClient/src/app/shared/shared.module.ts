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
import {ToolbarModule} from 'primeng/toolbar';
import {DividerModule} from 'primeng/divider';
import { AngularEditorModule } from '@kolkov/angular-editor';
import {RadioButtonModule} from 'primeng/radiobutton';
import { NoSanitizePipe } from './pipes/noSanitize.pipe';
import {AvatarModule} from 'primeng/avatar';
import {SidebarModule} from 'primeng/sidebar';
import { ChangePasswordModalComponent } from '../pages/users/user/change-password-modal/change-password-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';
import {ScrollPanelModule} from 'primeng/scrollpanel';


@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    ModalModule.forRoot(),
  ],
  declarations: [
    NoSanitizePipe,
    ChangePasswordModalComponent,
    ForbiddenComponent,
  ],
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
    CheckboxModule,
    ToolbarModule,
    DividerModule,
    AngularEditorModule,
    RadioButtonModule,
    NoSanitizePipe,
    AvatarModule,
    SidebarModule,
    ChangePasswordModalComponent,
    ScrollPanelModule
  ]
})
export class SharedModule { }