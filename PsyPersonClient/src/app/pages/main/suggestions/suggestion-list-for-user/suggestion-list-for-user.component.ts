import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { SendEmailMessageC } from 'src/app/models/emailMessageSettings.models';
import { SuggestionDto } from 'src/app/models/suggestion.model';
import { TestResultStatusEnum } from 'src/app/models/tests.models';
import { UserTestUserDto } from 'src/app/models/userTests.model';
import { EmailMessageSettingService } from 'src/app/services/api/emailMessageSetting.service';
import { SuggestionService } from 'src/app/services/api/suggestion.service';
import { GetSuggestionsByStatus } from 'src/app/store/actions/suggestion.actions';
import { selectSuggestionList, selectSuggestionsByStatus } from 'src/app/store/selectors/suggestion.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-suggestion-list-for-user',
  templateUrl: './suggestion-list-for-user.component.html',
  styleUrls: ['./suggestion-list-for-user.component.css']
})
export class SuggestionListForUserComponent implements OnInit {

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:EmailMessageSettingService,
  ) {
    this.tableFilter = new TableFilter();
    this.tableFilter.first = 0;
    this.tableFilter.itemPerPage = 5;
  }
  
  @ViewChild('suggestionForUser', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  suggestions$: Observable<PagedResponse<SuggestionDto> | any> = this.store.pipe(select(selectSuggestionsByStatus));

  filterText='';
  tableFilter: TableFilter;
  active = false;
  status: any;
  answerMode: boolean = false;
  customSuggestionName: string = '';
  customSuggestionDesc: string = '';
  user: UserTestUserDto = new UserTestUserDto();

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
        name: this.filterText ?? '',
        status: this.status
      };
      this.store.dispatch(new GetSuggestionsByStatus(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage,
        name: this.filterText ?? '',
        status: this.status
      };
      this.store.dispatch(new GetSuggestionsByStatus(request));
    }
  }

  show(status: TestResultStatusEnum, user: UserTestUserDto): void {
    this.user = user;
    this.answerMode = false;
    this.status = status;
    this.filterText='';
    this.active = true;
    this.modal.show();
  }

  Send(sug?: SuggestionDto) {
    var message = new SendEmailMessageC();

    if(this.answerMode === true){
      message.emailMessage = this.customSuggestionDesc;
      message.letterHeader = this.customSuggestionName;
    }
    else{
      message.emailMessage = sug == null ? '' : sug.description;
      message.letterHeader = sug == null ? '' : sug.name;
    }

    message.isHTML = false;
    message.receiverMailAddress = this.user.email;
    message.receiverFullName = this.user.firstName + ' ' + this.user.lastName + ' ' + this.user.patronymic;

    this.service?.sendMessage(message)
      .pipe(finalize(() => { this.answerMode = false;}))
      .toPromise().then(
        (res: any) => {
          if(res){
            this.toastr?.success('Message Send Successfully!', 'Send Successful.');
            this.close();
            this.modalSave.emit(null);
          }else{
            this.toastr?.error("Message Send Failed",'Send failed.');
             this.modal.hide();
          }
        },
        err => {
          this.toastr?.error("Message Send Failed",'Send failed.');
           this.modal.hide();
          console.log(err)
        }
      );

    this.answerMode = false;
    this.active = false;
    // this.modal.hide();
    
    this.modalSave.emit(sug);
  }

  changeAnswerMode(){
    this.answerMode = !this.answerMode;
  }

  close(): void {
    this.answerMode = false;
    this.active = false;
    this.modal.hide();
  }
}
