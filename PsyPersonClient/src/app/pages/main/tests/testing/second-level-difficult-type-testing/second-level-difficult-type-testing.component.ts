import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { TestForTestingDto, TestQuestionDto, TestResultStatusEnum } from 'src/app/models/tests.models';
import { TestService } from 'src/app/services/api/test.service';
import { GetTestForTesting } from 'src/app/store/actions/test.actions';
import { selectTestForTesting } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-second-level-difficult-type-testing',
  templateUrl: './second-level-difficult-type-testing.component.html',
  styleUrls: ['./second-level-difficult-type-testing.component.css']
})
export class SecondLevelDifficultTypeTestingComponent implements OnInit {

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
