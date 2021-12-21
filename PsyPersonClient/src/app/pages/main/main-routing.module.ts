import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
import { CreateOrEditFirstLevelDifficultTestQuestionComponent } from './tests/test-questions/create-or-edit-first-level-difficult-test-question/create-or-edit-first-level-difficult-test-question.component';
import { TestQuestionsComponent } from './tests/test-questions/test-questions.component';
import { SimpleTypeTestingComponent } from './tests/testing/simple-type-testing/simple-type-testing.component';
import { TestsComponent } from './tests/tests.component';
import { UserListComponent } from './userTests/user-list/user-list.component';
import { UserTestListComponent } from './userTests/user-test-list/user-test-list.component';
import { UserTestingHistoryComponent } from './userTests/user-testing-history/user-testing-history.component';

const routes: Routes = [
  { 
    path: '', 
    component: MainComponent 
  },
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
    path: 'userTestUsers', 
    component:UserListComponent
  },
  { 
    path: 'createOrEditL1DTypeTestQuestion', 
    component:CreateOrEditFirstLevelDifficultTestQuestionComponent
  },
  { 
    path: 'userTests', 
    component:UserTestListComponent
  },
  { 
    path: 'TestingHistory', 
    component:UserTestingHistoryComponent
  },
  {
    path: '',
    redirectTo: 'tests',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }