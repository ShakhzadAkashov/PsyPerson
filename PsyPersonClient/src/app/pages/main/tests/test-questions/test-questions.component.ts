import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { TestQuestionDto } from 'src/app/models/tests.models';
import { TestService } from 'src/app/services/api/test.service';
import { GetTestQuestions } from 'src/app/store/actions/test.actions';
import { selectTestQuestionList } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';
import Swal from 'sweetalert2';
import { CreateOrEditSimpleTypeTestQuestionModalComponent } from './create-or-edit-simple-type-test-question-modal/create-or-edit-simple-type-test-question-modal.component';
import { CreateTestQuestionsFromFileModalComponent } from './create-test-questions-from-file/create-test-questions-from-file.component';

@Component({
  selector: 'app-test-questions',
  templateUrl: './test-questions.component.html',
  styleUrls: ['./test-questions.component.css']
})
export class TestQuestionsComponent implements OnInit {

  @ViewChild('createOrEditSimpleTypeTestQuestionModal', { static: true })
  createOrEditSimpleTypeTestQuestionModal: CreateOrEditSimpleTypeTestQuestionModalComponent = new CreateOrEditSimpleTypeTestQuestionModalComponent(this.store,this.toastr,this.service,this.activatedRoute);
  @ViewChild('createTestQuestionsFromFileModal', { static: true })
  createTestQuestionsFromFileModal: CreateTestQuestionsFromFileModalComponent = new CreateTestQuestionsFromFileModalComponent(this.toastr,this.service,this.activatedRoute);
  questions$: Observable<PagedResponse<TestQuestionDto> | any> = this.store.pipe(select(selectTestQuestionList));
  filterText='';
  tableFilter: TableFilter = new TableFilter();
  testId = '';
  from = '';
  type: number;

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:TestService,
    public activatedRoute: ActivatedRoute,
    private router: Router
  ) { 
    this.testId = this.activatedRoute.snapshot.queryParams['testId'];
    this.from = this.activatedRoute.snapshot.queryParams['from'];
    this.type = this.activatedRoute.snapshot.queryParams['type'];
  }

  ngOnInit(): void {
  }

  filterInput(event: any){
    if (event.key === 'Enter' || event.keyCode === 13){
      this.onLazyLoad();
    }
  }

  onLazyLoad(event?: LazyLoadEvent){
    if(event)
    {
      const { first, rows } = event;
      const pageIndex = Math.ceil((first as number)/ (rows as number)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: rows as number,
        name: this.filterText ?? ''
      };
      request.testId = this.testId ?? '';
      this.store.dispatch(new GetTestQuestions(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage,
        name: this.filterText ?? ''
      };
      request.testId = this.testId ?? '';
      this.store.dispatch(new GetTestQuestions(request));
    }
  }

  createSimpleTypeTestQuestion(){
    this.createOrEditSimpleTypeTestQuestionModal.show();
  }

  createF1DTypeTestQuestion(testQuestionId?: string){
    this.router.navigate(['../home/main/createOrEditL1DTypeTestQuestion'], { queryParams: { testId: this.testId, testQuestionId: testQuestionId, from: 'home/main/testQuestions'} });
  }

  createF2DTypeTestQuestion(testQuestionId?: string){
    this.router.navigate(['../home/main/createOrEditL2DTypeTestQuestion'], { queryParams: { testId: this.testId, testQuestionId: testQuestionId, from: 'home/main/testQuestions'} });
  }

  createTestQuestionsFromFile(){
    this.createTestQuestionsFromFileModal.show();
  }

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }

  remove(testQuestion:TestQuestionDto)
  {
    Swal.fire({
      title: 'Удаление тестого вопроса',
      text: 'Вы Уверены ?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Ок',
      cancelButtonText: 'Отмена',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#7367F0',
    }).then((result) => {
      if (result.value) {
        this.service.removeTestQuestion(testQuestion.id).toPromise().then(
          (res: any) => {
            if(res){
              this.toastr.success(`Test Question ${testQuestion.name} Removed!`, 'Removed successful.');
              this.onLazyLoad();
            }else{
              this.toastr.error('Remove Test Question Failed','Remove failed.');
            }
          },
          err => {
            this.toastr.error(err.error,'Remove failed.');
            console.log(err)
          }
        );
      } 
    })
  }
}
