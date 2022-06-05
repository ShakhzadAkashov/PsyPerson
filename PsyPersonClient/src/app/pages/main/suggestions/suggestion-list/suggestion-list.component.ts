import { Component, OnInit, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { SuggestionDto } from 'src/app/models/suggestion.model';
import { SuggestionService } from 'src/app/services/api/suggestion.service';
import { GetSuggestions } from 'src/app/store/actions/suggestion.actions';
import { selectSuggestionList } from 'src/app/store/selectors/suggestion.selector';
import { AppState } from 'src/app/store/state/app.state';
import Swal from 'sweetalert2';
import { CreateOrEditSuggestionModalComponent } from '../create-or-edit-suggestion-modal/create-or-edit-suggestion-modal.component';

@Component({
  selector: 'app-suggestion-list',
  templateUrl: './suggestion-list.component.html',
  styleUrls: ['./suggestion-list.component.css']
})
export class SuggestionListComponent implements OnInit {

  @ViewChild('createOrEditSuggestionModal', { static: true })
  createOrEditSuggestionModal: CreateOrEditSuggestionModalComponent = new CreateOrEditSuggestionModalComponent(this.store,this.toastr,this.service);
  suggestions$: Observable<PagedResponse<SuggestionDto> | any> = this.store.pipe(select(selectSuggestionList));
  filterText='';
  tableFilter: TableFilter = new TableFilter();

  resultStatuses :{ [key: string]: any } = {
    0: {
      label: 'Низкое'
    },
    4: {
      label: 'Неизвестно'
    },
    3: {
      label: 'Отличное'
    },
    1: {
      label: 'Среднее'
    },
    2: {
      label: 'Xорошee'
    }
  };

  selectionTypes :{ [key: string]: any }  = {
    0: {
      label: 'Выборка по диапозону'
    },
    1: {
      label: 'Выборка по статусу'
    }
  };

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:SuggestionService,
    ) {}

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
      this.store.dispatch(new GetSuggestions(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage,
        name: this.filterText ?? ''
      };
      this.store.dispatch(new GetSuggestions(request));
    }
    
  }

  create(){
    this.createOrEditSuggestionModal.show();
  }

  remove(suggestion:SuggestionDto)
  {
    Swal.fire({
      title: 'Удаление продложки',
      text: 'Вы Уверены ?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Ок',
      cancelButtonText: 'Отмена',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#7367F0',
    }).then((result) => {
      if (result.value) {
        this.service.remove(suggestion.id).toPromise().then(
          (res: any) => {
            if(res){
              this.toastr.success(`Suggestion ${suggestion.name} Removed!`, 'Removed successful.');
              this.onLazyLoad();
            }else{
              res.errors.forEach((element:any) => {
                switch(element.code)
                {
                  default:
                    this.toastr.error(element.description,'Remove failed.');
                    break;
                }
              });
            }
          },
          err => {
            this.toastr.error(err,'Remove failed.');
            console.log(err)
          }
        );
      } 
    })
  }
}
