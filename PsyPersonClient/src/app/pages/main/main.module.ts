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
import { CreateOrEditSimpleTypeTestQuestionModalComponent } from './tests/test-questions/create-or-edit-simple-type-test-question-modal/create-or-edit-simple-type-test-question-modal.component';
import { CreateTestQuestionsFromFileModalComponent } from './tests/test-questions/create-test-questions-from-file/create-test-questions-from-file.component';
import { FileDownloadComponent } from './common/file-download/file-download.component';
import { SimpleTypeTestingComponent } from './tests/testing/simple-type-testing/simple-type-testing.component';
import { UserListComponent } from '../main/userTests/user-list/user-list.component';
import { UserTestListComponent } from './userTests/user-test-list/user-test-list.component';
import { TestLookupTableModalComponent } from './common/test-lookup-table-modal/test-lookup-table-modal.component';
import { UserTestsDetailsModalComponent } from '../main/userTests/user-list/user-tests-details-modal/user-tests-details-modal.component';
import { UserTestingHistoryComponent } from '../main/userTests/user-testing-history/user-testing-history.component';
import { CreateOrEditFirstLevelDifficultTestQuestionComponent } from './tests/test-questions/create-or-edit-first-level-difficult-test-question/create-or-edit-first-level-difficult-test-question.component';
import { FirstLevelDifficultTypeTestingComponent } from './tests/testing/first-level-difficult-type-testing/first-level-difficult-type-testing.component';
import { CreateOrEditSecondLevelDifficultTestQuestionComponent } from './tests/test-questions/create-or-edit-second-level-difficult-test-question/create-or-edit-second-level-difficult-test-question.component';
import { SecondLevelDifficultTypeTestingComponent } from './tests/testing/second-level-difficult-type-testing/second-level-difficult-type-testing.component';
import { UserTestingListForCheckComponent } from './userTests/user-testing-list-for-check/user-testing-list-for-check.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { UserTestingReportComponent } from './statistics/user-testing-report/user-testing-report.component';
import { SuggestionListComponent } from './suggestions/suggestion-list/suggestion-list.component';
import { CreateOrEditSuggestionModalComponent } from './suggestions/create-or-edit-suggestion-modal/create-or-edit-suggestion-modal.component';
import { SuggestionListForUserComponent } from './suggestions/suggestion-list-for-user/suggestion-list-for-user.component';

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
      CreateOrEditSimpleTypeTestQuestionModalComponent,
      CreateTestQuestionsFromFileModalComponent,
      FileDownloadComponent,
      SimpleTypeTestingComponent,
      UserListComponent,
      UserTestListComponent,
      TestLookupTableModalComponent,
      UserTestsDetailsModalComponent,
      UserTestingHistoryComponent,
      CreateOrEditFirstLevelDifficultTestQuestionComponent,
      FirstLevelDifficultTypeTestingComponent,
      CreateOrEditSecondLevelDifficultTestQuestionComponent,
      SecondLevelDifficultTypeTestingComponent,
      UserTestingListForCheckComponent,
      StatisticsComponent,
      UserTestingReportComponent,
      SuggestionListComponent,
      CreateOrEditSuggestionModalComponent,
      SuggestionListForUserComponent,
    ],
  exports: [MainComponent]
})
export class MainModule { }