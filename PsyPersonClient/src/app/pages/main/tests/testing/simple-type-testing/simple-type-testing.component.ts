import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { CheckSimpleTypeTestingCCRq, TestDto, TestForTestingDto, TestQuestionAnswerDto, TestQuestionDto } from 'src/app/models/tests.models';
import { TestService } from 'src/app/services/api/test.service';
import { GetTest, GetTestForTesting } from 'src/app/store/actions/test.actions';
import { selectselectedTest, selectTestForTesting } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';
import Swal from 'sweetalert2'

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
    private service: TestService
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
      this.test.test.testType = res.test.testType;

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
  
  finishTest(){
    this.bal = 0.0;
    let testStatus = '';
    const command = new CheckSimpleTypeTestingCCRq();
    command.testForTesting = this.test;
    this.service.checkSimpeTypeTesting(command).toPromise().then((res:number | any)=>{
      this.bal = res;
      
      if(this.bal >=90){
        testStatus = 'EXCELLENT';
      }else if(this.bal >= 70){
        testStatus = 'GOOD';
      }else if(this.bal >= 50){
        testStatus = 'SATISFACTORY';
      }else{
        testStatus = "BAD";
      }

      this.alertTest(this.bal,testStatus);
    });
  }

  alertTest(bal:number, testStatus: string){
    let status: string = '';
    let Icon: string = '';
    if(testStatus == "EXCELLENT"){
      status = 'Отлично - вы можете продолжать работу!';
      Icon = 'success';
    }else if(testStatus == "GOOD"){
      status = 'Стабильно - приглашаем вас на чашечку чая или кофе!';
      Icon = 'success';
    }else if(testStatus == "SATISFACTORY"){
      status = 'Удовлетворительно - тут уже без печенек никуда!';
      Icon = 'info';
    }else{
      status = 'Необходим отдых!';
      Icon = 'warning';
    }
    Swal.fire({
      title: status,
      text: 'Тест был пройден на' + ' ' + bal.toFixed(1) +' %',
      icon: 'info',
      showCancelButton: true,
      confirmButtonText: 'Ок',
      cancelButtonText: 'Пройти тест заново',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#7367F0',
      allowOutsideClick: false
    }).then((result) => {
      if (result.value) {
        this.goBack();
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        location.reload();
      }
    })
  }
}
