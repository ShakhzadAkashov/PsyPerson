import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
import { TestQuestionsComponent } from './tests/test-questions/test-questions.component';
import { TestsComponent } from './tests/tests.component';

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