import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
import { StatisticsComponent } from './statistics/statistics.component';
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
    path: '', 
    component: MainComponent,
    children:[
      { 
        path: 'tests', 
        component:TestsComponent
      },
      { 
        path: 'testQuestions', 
        component:TestQuestionsComponent
      },
      { 
        path: 'simpleTypeTesting', 
        component:SimpleTypeTestingComponent
      },
      { 
        path: 'L1DTypeTesting', 
        component:FirstLevelDifficultTypeTestingComponent
      },
      { 
        path: 'l2DTypeTesting', 
        component:SecondLevelDifficultTypeTestingComponent
      },
      { 
        path: 'userTestUsers', 
        component:UserListComponent
      },
      { 
        path: 'createOrEditL1DTypeTestQuestion', 
        component:CreateOrEditFirstLevelDifficultTestQuestionComponent
      },
      { 
        path: 'createOrEditL2DTypeTestQuestion', 
        component:CreateOrEditSecondLevelDifficultTestQuestionComponent
      },
      { 
        path: 'userTests', 
        component:UserTestListComponent
      },
      { 
        path: 'userTestingListForCheck', 
        component:UserTestingListForCheckComponent
      },
      { 
        path: 'TestingHistory', 
        component:UserTestingHistoryComponent
      },
      { 
        path: 'statistics', 
        component:StatisticsComponent
      },
      {
        path: '',
        redirectTo: 'statistics',
        pathMatch: 'full',
      }
    ] 
  } 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }