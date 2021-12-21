import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { TestQuestionAnswerDto, TestQuestionDto, TestQuestionTypeEnum, UpdateTestQuestionCRq } from 'src/app/models/tests.models';
import { TestService } from 'src/app/services/api/test.service';
import { GetTestQuestion } from 'src/app/store/actions/test.actions';
import { selectTestQuestion } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-create-or-edit-simple-type-test-question-modal',
  templateUrl: './create-or-edit-simple-type-test-question-modal.component.html',
  styleUrls: ['./create-or-edit-simple-type-test-question-modal.component.css']
})
export class CreateOrEditSimpleTypeTestQuestionModalComponent implements OnInit {

  @ViewChild('createOrEditModal', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<TestQuestionDto> = new EventEmitter<TestQuestionDto>();
  testQuestion$: Observable<TestQuestionDto> = this.store.pipe(select(selectTestQuestion));

  active = false;
  saving = false;
  edit = false;

  testQuestion: TestQuestionDto = new TestQuestionDto();
  testId = '';

  testQuestionTypes :{key: any, value: TestQuestionTypeEnum}[] = [
    {
      key: 'Simple Question',
      value: TestQuestionTypeEnum.SimpleQuestion
    }
  ]

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:TestService,
    public activatedRoute: ActivatedRoute,
  ) {
    this.testId = this.activatedRoute.snapshot.queryParams['testId'];
  }

  ngOnInit(): void {
  }

  show(testQuestion?: TestQuestionDto): void { 
    this.edit = false;
    
    if (!testQuestion) {
        this.testQuestion = new TestQuestionDto();
        this.testQuestion.testId = this.testId;

        this.active = true;
        this.modal.show();
    } else {
        this.store.dispatch(new GetTestQuestion(testQuestion.id));
        this.testQuestion$.subscribe(res => {
          let r = res; 
          this.testQuestion.id = r.id;
          this.testQuestion.name = r.name;
          this.testQuestion.questionType = r.questionType;
          this.testQuestion.testId = this.testId;
          this.testQuestion.answers = [];
          r.answers.forEach(element => {
            let a = new TestQuestionAnswerDto();
            a.id = element.id;
            a.idForView = element.idForView;
            a.isCorrect = element.isCorrect;
            a.name = element.name;
            a.testQuestionId = element.testQuestionId;
            this.testQuestion.answers.push(a);
          });
        });

        this.edit = true;
        this.active = true;
        this.modal.show();
    }
  }

  save(): void {
    this.saving = true;

    if(this.edit == true){
      let command = new UpdateTestQuestionCRq();
      command.id = this.testQuestion.id;
      command.name = this.testQuestion.name;
      command.answers = this.testQuestion.answers;

      this.service.updateTestQuestion(command)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(res =>{
        this.toastr.success('Saved!', 'Test Question Saved successful.');
        this.close();
        this.modalSave.emit(this.testQuestion);
      });
    }else{
      this.service.createTestQuestion(this.testQuestion)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(
        (res: any) => {
          if(res){
            this.toastr.success('New Test Question created!', 'Created successful.');
            this.close();
            this.modalSave.emit(this.testQuestion);
          }else{
            this.toastr.error("Create Test Question Failed",'Created failed.');
          }
        },
        err => {
          this.toastr.error("Create Test Question Failed",'Created failed.');
          console.log(err)
        }
      );
    }
  }

  close(): void {
    this.active = false;
    this.modal.hide();
  }

  addTestQuestuionAnswer(){
    var testQuestionAnswer = new TestQuestionAnswerDto();
    let max = 0;
    this.testQuestion.answers.forEach(obj => {
      if(obj.idForView > max){
        max = obj.idForView
      }
    });
    testQuestionAnswer.idForView = max + 1;
    testQuestionAnswer.id = '00000000-0000-0000-0000-000000000000';
    testQuestionAnswer.testQuestionId = '00000000-0000-0000-0000-000000000000';
    this.testQuestion.answers.push(testQuestionAnswer);
  }

  deleteTestQuestionAnswer(item: TestQuestionAnswerDto){
    this.testQuestion.answers = this.testQuestion.answers.filter(obj => obj.idForView !== item.idForView);
  }
}
