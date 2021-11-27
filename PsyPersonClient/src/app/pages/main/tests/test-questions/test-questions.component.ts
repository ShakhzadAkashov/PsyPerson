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
import { CreateOrEditTestQuestionModalComponent } from './create-or-edit-test-question/create-or-edit-test-question.component';
import { CreateTestQuestionsFromFileModalComponent } from './create-test-questions-from-file/create-test-questions-from-file.component';

@Component({
  selector: 'app-test-questions',
  templateUrl: './test-questions.component.html',
  styleUrls: ['./test-questions.component.css']
})
export class TestQuestionsComponent implements OnInit {

  @ViewChild('createOrEditTestQuestionModal', { static: true })
  createOrEditTestQuestionModal: CreateOrEditTestQuestionModalComponent = new CreateOrEditTestQuestionModalComponent(this.store,this.toastr,this.service,this.activatedRoute);
  @ViewChild('createTestQuestionsFromFileModal', { static: true })
  createTestQuestionsFromFileModal: CreateTestQuestionsFromFileModalComponent = new CreateTestQuestionsFromFileModalComponent(this.toastr,this.service,this.activatedRoute);
  questions$: Observable<PagedResponse<TestQuestionDto> | any> = this.store.pipe(select(selectTestQuestionList));
  filterText='';
  tableFilter: TableFilter = new TableFilter();
  testId = '';
  from = '';

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:TestService,
    public activatedRoute: ActivatedRoute,
    private router: Router
  ) { 
    this.testId = this.activatedRoute.snapshot.queryParams['testId'];
    this.from = this.activatedRoute.snapshot.queryParams['from'];
  }

  ngOnInit(): void {
  }

  filterInput(event: any){}

  onLazyLoad(event?: LazyLoadEvent){
    if(event)
    {
      const { first, rows } = event;
      const pageIndex = Math.ceil((first as number)/ (rows as number)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: rows as number,
      };
      request.testId = this.testId ?? '';
      this.store.dispatch(new GetTestQuestions(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage
      };
      request.testId = this.testId ?? '';
      this.store.dispatch(new GetTestQuestions(request));
    }
  }

  createTestQuestion(){
    this.createOrEditTestQuestionModal.show();
  }

  createTestQuestionsFromFile(){
    this.createTestQuestionsFromFileModal.show();
  }

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }
}
