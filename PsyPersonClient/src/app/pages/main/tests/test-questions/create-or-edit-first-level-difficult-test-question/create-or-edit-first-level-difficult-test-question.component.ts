import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { TestQuestionAnswerDto, TestQuestionDto, UpdateTestQuestionCRq } from 'src/app/models/tests.models';
import { TestService } from 'src/app/services/api/test.service';
import { EditorHelper } from 'src/app/shared/helpers/editor.helpers';
import { GetTestQuestion } from 'src/app/store/actions/test.actions';
import { selectTestQuestion } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-create-or-edit-first-level-difficult-test-question',
  templateUrl: './create-or-edit-first-level-difficult-test-question.component.html',
  styleUrls: ['./create-or-edit-first-level-difficult-test-question.component.css']
})
export class CreateOrEditFirstLevelDifficultTestQuestionComponent implements OnInit {

  testQuestion$: Observable<TestQuestionDto> = this.store.pipe(select(selectTestQuestion));

  active = false;
  saving = false;
  edit = false;

  testQuestion: TestQuestionDto = new TestQuestionDto();
  testId = '';
  testQuestionId = undefined;
  from = '';
  editorConfig:any;

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:TestService,
    public activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    const testId = this.activatedRoute.snapshot.queryParams['testId'];
    if(testId) this.testId = testId;

    const testQuestionId = this.activatedRoute.snapshot.queryParams['testQuestionId'];
    if(testQuestionId) this.testQuestionId = testQuestionId;

    const from = this.activatedRoute.snapshot.queryParams['from'];
    if(from) this.from = from;

    this.editorConfig = EditorHelper.EditorConfig();
  }

  ngOnInit(): void {
    this.show(this.testQuestionId);
  }

  show(testQuestionId?: string): void { 
    this.edit = false;
    
    if (!testQuestionId) {
        this.testQuestion = new TestQuestionDto();
        this.testQuestion.testId = this.testId;

        this.active = true;
    } else {
        this.store.dispatch(new GetTestQuestion(testQuestionId));
        this.testQuestion$.subscribe(async res => {
          let r = res; 
          this.testQuestion.id = r.id;
          this.testQuestion.name = r.name;
          this.testQuestion.testId = this.testId;
          this.testQuestion.answers = [];
          r?.answers?.forEach(element => {
            let a = new TestQuestionAnswerDto();
            a.id = element.id;
            a.idForView = element.idForView;
            a.isCorrect = element.isCorrect;
            a.name = element.name;
            a.score = element.score;
            a.testQuestionId = element.testQuestionId;
            this.testQuestion.answers.push(a);
          });
        });

        this.edit = true;
        this.active = true;
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
        this.goBack();
      });
    }else{
      this.service.createTestQuestion(this.testQuestion)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(
        (res: any) => {
          if(res){
            this.toastr.success('New Test Question created!', 'Created successful.');
            this.goBack();
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

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from], { queryParams: { testId: this.testId, from: 'home/main/tests', type:1} });
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
    testQuestionAnswer.isCorrect = true;
    this.testQuestion.answers.push(testQuestionAnswer);
  }

  deleteTestQuestionAnswer(item: TestQuestionAnswerDto){
    this.testQuestion.answers = this.testQuestion.answers.filter(obj => obj.idForView !== item.idForView);
  }
}
