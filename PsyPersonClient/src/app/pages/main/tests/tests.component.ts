import { Component, OnInit, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { TestDto } from 'src/app/models/tests.models';
import { AppFilesService } from 'src/app/services/api/appFiles.serive';
import { TestService } from 'src/app/services/api/test.service';
import { GetTests } from 'src/app/store/actions/test.actions';
import { selectTestList } from 'src/app/store/selectors/test.selector';
import { AppState } from 'src/app/store/state/app.state';
import { CreateOrEditTestModalComponent } from './create-or-edit-test-modal/create-or-edit-test-modal.component';

@Component({
  selector: 'app-tests',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.css']
})
export class TestsComponent implements OnInit {

  @ViewChild('createOrEditTestModal', { static: true })
  createOrEditTestModal: CreateOrEditTestModalComponent = new CreateOrEditTestModalComponent(this.store,this.toastr,this.service, this.fileService);
  tests$: Observable<PagedResponse<TestDto> | any> = this.store.pipe(select(selectTestList));
  tableFilter: TableFilter = new TableFilter();

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService,
    private service: TestService,
    private fileService: AppFilesService
  ) {
      this.tableFilter.itemPerPage = 8;
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
        itemPerPage: rows as number
      };
      this.store.dispatch(new GetTests(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage
      };
      this.store.dispatch(new GetTests(request));
    }
    
  }

  create(){
    this.createOrEditTestModal.show();
  }

  createImgPath(filePath: string){
    if(filePath){
      let image = this.fileService.getPhoto(filePath);
      return image;
    }
    return '';
  }
}
