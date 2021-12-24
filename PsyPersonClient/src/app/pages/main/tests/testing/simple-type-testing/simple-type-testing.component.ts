import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { CheckTestingCCRq, TestDto, TestForTestingDto, TestQuestionAnswerDto, TestQuestionDto, TestResultStatusEnum } from 'src/app/models/tests.models';
import { CheckTestingResponseDto } from 'src/app/models/userTests.model';
import { TestService } from 'src/app/services/api/test.service';
import { UserHelper } from 'src/app/shared/helpers/user.helper';
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
    private service: TestService,
    private toastr: ToastrService, 
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
    const command = new CheckTestingCCRq();
    command.testForTesting = this.test;
    command.userId = UserHelper.getCurrentUserId();
    this.service.checkSimpeTypeTesting(command).toPromise().then((res:CheckTestingResponseDto)=>{
      if(res){
        this.bal = res.testScore;
        let status = res.status;
        let desc = res.description;
  
        this.alertTest(this.bal,status,desc);
      }
      else{
        this.toastr.error("Check Testing Failed",'Check failed.');
      } 
    },
    err => {
      this.toastr.error(err.error,'Check failed.');
      console.log(err)
    });
  }

  alertTest(bal:number, testStatus:any, desc:string){
    let Icon: string | any;
    if(testStatus === TestResultStatusEnum.Excelent){
      Icon = 'success';
    }else if(testStatus === TestResultStatusEnum.Good){
      Icon = 'success';
    }else if(testStatus === TestResultStatusEnum.Satisfactory){
      Icon = 'info';
    }else if(testStatus === TestResultStatusEnum.Low){
      Icon = 'warning';
    }
    else{
      Icon = 'warning';
    }
    Swal.fire({
      title: desc,
      text: 'Тест был пройден на' + ' ' + bal.toFixed(1) +' %',
      icon: Icon,
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
