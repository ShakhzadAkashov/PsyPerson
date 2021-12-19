import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { ModalModule } from 'ngx-bootstrap/modal';

import { MainComponent } from './main.component';
import { MainRoutingModule } from './main-routing.module';
import { TestsComponent } from './tests/tests.component';
import { FileUploadComponent } from './common/file-upload/file-upload.component';
import { CreateOrEditTestModalComponent } from './tests/create-or-edit-test-modal/create-or-edit-test-modal.component';
import {FieldsetModule} from 'primeng/fieldset';
import { TestQuestionsComponent } from './tests/test-questions/test-questions.component';
import { CreateOrEditTestQuestionModalComponent } from './tests/test-questions/create-or-edit-test-question/create-or-edit-test-question.component';
import { CreateTestQuestionsFromFileModalComponent } from './tests/test-questions/create-test-questions-from-file/create-test-questions-from-file.component';
import { FileDownloadComponent } from './common/file-download/file-download.component';
import { SimpleTypeTestingComponent } from './tests/testing/simple-type-testing/simple-type-testing.component';
import { UserListComponent } from '../main/userTests/user-list/user-list.component';
import { UserTestListComponent } from './userTests/user-test-list/user-test-list.component';
import { TestLookupTableModalComponent } from './common/test-lookup-table-modal/test-lookup-table-modal.component';
import { UserTestsDetailsModalComponent } from '../main/userTests/user-list/user-tests-details-modal/user-tests-details-modal.component'

@NgModule({
  imports: [
      MainRoutingModule, 
      SharedModule,
      CommonModule,
      FieldsetModule,
      ModalModule.forRoot(),
    ],
  declarations: [
      MainComponent,
      TestsComponent,
      CreateOrEditTestModalComponent,
      FileUploadComponent,
      TestQuestionsComponent,
      CreateOrEditTestQuestionModalComponent,
      CreateTestQuestionsFromFileModalComponent,
      FileDownloadComponent,
      SimpleTypeTestingComponent,
      UserListComponent,
      UserTestListComponent,
      TestLookupTableModalComponent,
      UserTestsDetailsModalComponent,
    ],
  exports: [MainComponent]
})
export class MainModule { }