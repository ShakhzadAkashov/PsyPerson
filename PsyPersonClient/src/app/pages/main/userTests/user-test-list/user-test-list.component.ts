import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { TestResultStatusEnum } from 'src/app/models/tests.models';
import { UserTestDto } from 'src/app/models/userTests.model';
import { AppFilesService } from 'src/app/services/api/appFiles.serive';
import { TestService } from 'src/app/services/api/test.service';
import { GetUserTests } from 'src/app/store/actions/userTest.actions';
import { selectUserTests } from 'src/app/store/selectors/userTest.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-user-test-list',
  templateUrl: './user-test-list.component.html',
  styleUrls: ['./user-test-list.component.css']
})
export class UserTestListComponent implements OnInit {
  
  tests$: Observable<PagedResponse<UserTestDto> | any> = this.store.pipe(select(selectUserTests));
  tableFilter: TableFilter = new TableFilter();

  resultStatuses :{ [key: string]: any } = {
    Low: {
      label: 'Low',
      color: 'info'
    },
    Unknown: {
      label: 'Unknown',
      color: 'danger'
    },
    Excelent: {
      label: 'Excelent',
      color: 'success'
    },
    Satisfactory: {
      label: 'Satisfactory',
      color: 'warning'
    },
    Good: {
      label: 'Good',
      color: 'primary'
    }
  }

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
      this.store.dispatch(new GetUserTests(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage
      };
      this.store.dispatch(new GetUserTests(request));
    }
    
  }

  createImgPath(filePath: string){
    if(filePath){
      let image = this.fileService.getPhoto(filePath);
      return image;
    }
    return '';
  }

}
