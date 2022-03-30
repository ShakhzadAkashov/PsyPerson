import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, TableFilter } from 'src/app/models/base';
import { CheckTestingCRq, TestQuestionDto, TestResultStatusEnum, TestTypeEnum } from 'src/app/models/tests.models';
import { AnswerResultStatusEnum, CheckTestingResponseDto, TestingHistoryDto, TestingHistoryQuestionDto } from 'src/app/models/userTests.model';
import { TestService } from 'src/app/services/api/test.service';
import { GetTestingHistory } from 'src/app/store/actions/userTest.actions';
import { selectTestingHistory } from 'src/app/store/selectors/userTest.selector';
import { AppState } from 'src/app/store/state/app.state';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-testing-history',
  templateUrl: './user-testing-history.component.html',
  styleUrls: ['./user-testing-history.component.css']
})
export class UserTestingHistoryComponent implements OnInit {

  testingHistory$: Observable<TestingHistoryDto | any> = this.store.pipe(select(selectTestingHistory));
  userTestingHistoryId: string = '';
  userId: string = '';
  from: string = '';
  testType: number;
  check: boolean = false;
  tableFilter: TableFilter = new TableFilter();
  questionHistoryList: TestingHistoryQuestionDto[] = [];
  type = TestTypeEnum.SecondLevelDifficultTest;

  AnswerResultStatuses :{key: any, value: AnswerResultStatusEnum}[] = [
    {
      key: 'Низкий уровень',
      value: AnswerResultStatusEnum.Low
    },
    {
      key: 'Средний уровень',
      value: AnswerResultStatusEnum.Satisfactory
    },
    {
      key: 'Уровень-хорошо',
      value: AnswerResultStatusEnum.Good
    },
    {
      key: 'Уровень-отлично',
      value: AnswerResultStatusEnum.Excelent
    }
  ]

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private service: TestService,
    private toastr: ToastrService,
  ) { 
    this.userTestingHistoryId = this.activatedRoute.snapshot.queryParams['userTestingHistoryId'];
    this.from = this.activatedRoute.snapshot.queryParams['from'];
    this.testType = this.activatedRoute.snapshot.queryParams['testType'];
    this.check = this.activatedRoute.snapshot.queryParams['check'];
    this.userId = this.activatedRoute.snapshot.queryParams['userId'];
  }

  ngOnInit(): void {
  }

  onLazyLoad(event?: LazyLoadEvent){
    if(event)
    {
      const { first, rows } = event;
      const pageIndex = Math.ceil((first as number)/ (rows as number)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: rows as number,
        userTestingHistoryId: this.userTestingHistoryId
      };
      this.store.dispatch(new GetTestingHistory(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage,
        userTestingHistoryId: this.userTestingHistoryId
      };
      this.store.dispatch(new GetTestingHistory(request));
    }
  }

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }

  dropdownChange(event: any){
    let h = new TestingHistoryQuestionDto();
    h.id = event.questionId;
    h.name = event.questionName;
    h.customAnswer.id = event.customAnswer.id;
    h.customAnswer.name = event.customAnswer.name;
    h.customAnswer.userTestingHistoryId = event.customAnswer.userTestingHistoryId;
    h.customAnswer.testQuestionId = event.customAnswer.testQuestionId;
    h.customAnswer.answerStatus = event.status;
    h.customAnswer.answerScore = event.status.valueOf();

    this.questionHistoryList = this.questionHistoryList.filter(x => x.id !== h.id);
    this.questionHistoryList.push(h);

    console.log("event: " + event.customAnswer.name + h.customAnswer.answerStatus + h.customAnswer.answerScore);
  }

  checkTesting(history: TestingHistoryDto, type:any){
    let bal = 0.0;
    const command = new CheckTestingCRq();
    command.testForTesting.test.id = history.testId;
    command.testForTesting.test.name = history.testName;
    command.isChecked = true;
    command.userId = this.userId;
    command.userTestingHistoryId = this.userTestingHistoryId;
    command.testForTesting.test.testType = type.valueOf();

    this.questionHistoryList.forEach( e => {
      let q = new TestQuestionDto();
      q.name = e.name;
      q.id = e.id;
      q.customAnswerId = e.customAnswer.id;
      q.customAnswerScore = e.customAnswer.answerScore;
      q.customAnswerStatus = e.customAnswer.answerStatus;
      q.testId = history.testId;
      command.testForTesting.testQuestionList.push(q);
    });

    this.service.checkSecondLevelDifficultTypeTesting(command).toPromise().then((res:CheckTestingResponseDto)=>{
      if(res){
        bal = res.testScore;
        let status = res.status;
        let desc = res.description;
  
        this.alertTest(bal,status,desc);
      }
      else{
        this.toastr.error("Check Testing Failed",'Check failed.');
      } 
    },
    err => {
      this.toastr.error(err.error,'Check failed.');
      console.log(err)
    });

    console.log("history: " + history.testName);
    console.log("command: " + command.testForTesting.testQuestionList);
  }

  alertTest(bal:number, testStatus:any, desc:string){
    let Icon: string | any;
    let text: string = '';

    text = 'Тест был проверен! Набранные проценты:' + ' ' + bal.toFixed(1) + ' %';

    if (testStatus === TestResultStatusEnum.Excelent) {
      Icon = 'success';
    } else if (testStatus === TestResultStatusEnum.Good) {
      Icon = 'success';
    } else if (testStatus === TestResultStatusEnum.Satisfactory) {
      Icon = 'info';
    } else if (testStatus === TestResultStatusEnum.Low) {
      Icon = 'warning';
    }
    else {
      Icon = 'warning';
    }

    Swal.fire({
      title: desc,
      text: text,
      icon: Icon,
      // showCancelButton: true,
      confirmButtonText: 'Ок',
      // cancelButtonText: 'Пройти тест заново',
      confirmButtonColor: '#3085d6',
      //cancelButtonColor: '#7367F0',
      allowOutsideClick: false
    }).then((result) => {
      if (result.value) {
        this.goBack();
      } 
      // else if (result.dismiss === Swal.DismissReason.cancel) {
      //   location.reload();
      // }
    })
  }
}
