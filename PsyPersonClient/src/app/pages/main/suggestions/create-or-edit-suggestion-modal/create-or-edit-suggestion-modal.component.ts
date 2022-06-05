import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { SuggestionDto, SuggestionSelectTypeEnum } from 'src/app/models/suggestion.model';
import { TestResultStatusEnum } from 'src/app/models/tests.models';
import { SuggestionService } from 'src/app/services/api/suggestion.service';
import { GetSuggestion } from 'src/app/store/actions/suggestion.actions';
import { selectselectedSuggestion } from 'src/app/store/selectors/suggestion.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-create-or-edit-suggestion-modal',
  templateUrl: './create-or-edit-suggestion-modal.component.html',
  styleUrls: ['./create-or-edit-suggestion-modal.component.css']
})
export class CreateOrEditSuggestionModalComponent implements OnInit {

  selectionTypes :{key: any, value: SuggestionSelectTypeEnum}[] = [
    {
      key: 'Выборка по диапозону',
      value: SuggestionSelectTypeEnum.SelectByRange
    },
    {
      key: 'Выборка по статусу',
      value: SuggestionSelectTypeEnum.SelectByStatus
    }
  ];

  testResultStatuses :{key: any, value: TestResultStatusEnum}[] = [
    {
      key: 'Низкий уровень',
      value: TestResultStatusEnum.Low
    },
    {
      key: 'Средний уровень',
      value: TestResultStatusEnum.Satisfactory
    },
    {
      key: 'Уровень-хороший ',
      value: TestResultStatusEnum.Good
    },
    {
      key: 'Уровень-отличный',
      value: TestResultStatusEnum.Excelent
    }
  ];

  @ViewChild('createOrEditModal', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<SuggestionDto> = new EventEmitter<SuggestionDto>();
  suggestion$: Observable<SuggestionDto> = this.store.pipe(select(selectselectedSuggestion));

  active = false;
  saving = false;
  edit = false;
  suggestion: SuggestionDto = new SuggestionDto();

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:SuggestionService, 
    ) { }

  ngOnInit(): void {
  }

  show(suggestion?: SuggestionDto): void { 
    this.edit = false;
    
    if (!suggestion) {
        this.suggestion = new SuggestionDto();

        this.active = true;
        this.modal.show();
    } else {
        this.store.dispatch(new GetSuggestion(suggestion.id));
        this.suggestion$.subscribe(res => {
          let r = res; 
          this.suggestion.id = r.id;
          this.suggestion.name = r.name;
          this.suggestion.description = r.description;
          this.suggestion.rangeFrom = r.rangeFrom;
          this.suggestion.rangeTo = r.rangeTo;
          this.suggestion.status = r.status;
          this.suggestion.selectionType = r.selectionType;
        });

        this.edit = true;
        this.active = true;
        this.modal.show();
    }
  }

  save(): void {
    this.saving = true;

    var appSuggestion = new SuggestionDto();

    appSuggestion.name = this.suggestion.name;
    appSuggestion.description = this.suggestion.description;
    appSuggestion.rangeFrom = this.suggestion.rangeFrom;
    appSuggestion.rangeTo = this.suggestion.rangeTo;
    appSuggestion.status = this.suggestion.status;
    appSuggestion.selectionType = this.suggestion.selectionType;
    appSuggestion.id = this.suggestion.id;

    if(this.edit == true){
      this.service?.update(appSuggestion)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(res =>{
        this.toastr?.success('Saved!', 'Suggestion Saved successful.');
        this.close();
        this.modalSave.emit(this.suggestion);
      });
    }else{
      this.service?.create(appSuggestion)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(
        (res: any) => {
          if(res){
            this.toastr?.success('New suggestion created!', 'Created successful.');
            this.close();
            this.modalSave.emit(this.suggestion);
          }else{
            this.toastr?.error("Create Suggestion Failed",'Created failed.');
          }
        },
        err => {
          this.toastr?.error("Create Suggestion Failed",'Created failed.');
          console.log(err)
        }
      );
    }
  }

  close(): void {
    this.active = false;
    this.modal.hide();
  }

}
