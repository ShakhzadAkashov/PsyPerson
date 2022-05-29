import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/services/auth/auth.guard';
import { MainComponent } from './main.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { UserTestingReportComponent } from './statistics/user-testing-report/user-testing-report.component';
import { CreateOrEditFirstLevelDifficultTestQuestionComponent } from './tests/test-questions/create-or-edit-first-level-difficult-test-question/create-or-edit-first-level-difficult-test-question.component';
import { CreateOrEditSecondLevelDifficultTestQuestionComponent } from './tests/test-questions/create-or-edit-second-level-difficult-test-question/create-or-edit-second-level-difficult-test-question.component';
import { TestQuestionsComponent } from './tests/test-questions/test-questions.component';
import { FirstLevelDifficultTypeTestingComponent } from './tests/testing/first-level-difficult-type-testing/first-level-difficult-type-testing.component';
import { SecondLevelDifficultTypeTestingComponent } from './tests/testing/second-level-difficult-type-testing/second-level-difficult-type-testing.component';
import { SimpleTypeTestingComponent } from './tests/testing/simple-type-testing/simple-type-testing.component';
import { TestsComponent } from './tests/tests.component';
import { UserListComponent } from './userTests/user-list/user-list.component';
import { UserTestListComponent } from './userTests/user-test-list/user-test-list.component';
import { UserTestingHistoryComponent } from './userTests/user-testing-history/user-testing-history.component';
import { UserTestingListForCheckComponent } from './userTests/user-testing-list-for-check/user-testing-list-for-check.component';

const routes: Routes = [
  {
    path: '', component: MainComponent, canActivate: [AuthGuard],
    children: [
      { path: 'tests', component: TestsComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Tests.Pages' } },
      { path: 'testQuestions', component: TestQuestionsComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.TestQuestions.Pages' } },
      { path: 'simpleTypeTesting', component: SimpleTypeTestingComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Testings.Pages' } },
      { path: 'L1DTypeTesting', component: FirstLevelDifficultTypeTestingComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Testings.Pages' } },
      { path: 'l2DTypeTesting', component: SecondLevelDifficultTypeTestingComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Testings.Pages' } },
      { path: 'userTestUsers', component: UserListComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.UserTests.ViewUsers' } },
      { path: 'createOrEditL1DTypeTestQuestion', component: CreateOrEditFirstLevelDifficultTestQuestionComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.TestQuestions.Create' } },
      { path: 'createOrEditL2DTypeTestQuestion', component: CreateOrEditSecondLevelDifficultTestQuestionComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.TestQuestions.Create' } },
      { path: 'userTests', component: UserTestListComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.UserTests.Pages' } },
      { path: 'userTestingListForCheck', component: UserTestingListForCheckComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Testings.ForCheck' } },
      { path: 'TestingHistory', component: UserTestingHistoryComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Testings.ViewHistory' } },
      { path: 'statistics', component: StatisticsComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Statistics.Pages' } },
      { path: 'userTestingReport', component: UserTestingReportComponent, canActivate: [AuthGuard], data: { permission: 'Permissions.Statistics.UserTestingHistoryReports' } },
      { path: '', redirectTo: 'statistics', pathMatch: 'full', }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }