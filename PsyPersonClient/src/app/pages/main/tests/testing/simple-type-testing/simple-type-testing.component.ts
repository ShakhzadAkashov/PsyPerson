import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { TestDto, TestForTestingDto, TestQuestionAnswerDto, TestQuestionDto } from 'src/app/models/tests.models';
import { GetTest, GetTestForTesting } from 'src/app/store/actions/test.actions';
import { selectselectedTest, selectTestForTesting } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-simple-type-testing',
  templateUrl: './simple-type-testing.component.html',
  styleUrls: ['./simple-type-testing.component.css']
})
export class SimpleTypeTestingComponent implements OnInit {

  test$: Observable<TestForTestingDto> = this.store.pipe(select(selectTestForTesting));
  test: TestForTestingDto = new TestForTestingDto();
  testId = '';
  from = '';
  bal = 0.0;
  loading: boolean = true;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    ) { 
    this.testId = this.activatedRoute.snapshot.queryParams['testId'];
    this.from = this.activatedRoute.snapshot.queryParams['from'];
  }

  ngOnInit(): void { 
    if(this.testId)
    this.getTest(this.testId);
  }

  getTest(testId: any){
    this.store.dispatch(new GetTestForTesting(testId));
    this.test$.subscribe((res: TestForTestingDto) => {
      this.test = new TestForTestingDto();
      this.test.test.name = res.test.name;
      this.test.test.id = res.test.id;

      res.testQuestionList.forEach(element => {
        let q = new TestQuestionDto();
        q.name = element.name;
        q.id = element.id;
        q.testId = element.testId;
        q.questionType = element.questionType;
        element.answers.forEach(e =>{
          let a = new TestQuestionAnswerDto();
          a.id = e.id;
          a.idForView = e.idForView;
          a.isCorrect = e.isCorrect;
          a.name = e.name;
          a.testQuestionId = e.testQuestionId
          q.answers.push(a);
        });
        this.test.testQuestionList.push(q);
      });
      this.loading = false;
    });
  }

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }
  
  finishTest(){}
}
