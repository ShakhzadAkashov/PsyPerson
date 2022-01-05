import { Component, OnInit, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { UserTestDetailDto } from 'src/app/models/userTests.model';
import { UserTestService } from 'src/app/services/api/userTest.service';
import { GetUserTestsDetails } from 'src/app/store/actions/userTest.actions';
import { selectUserTestsDetails } from 'src/app/store/selectors/userTest.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-user-tests-details-modal',
  templateUrl: './user-tests-details-modal.component.html',
  styleUrls: ['./user-tests-details-modal.component.css']
})
export class UserTestsDetailsModalComponent implements OnInit {

  @ViewChild('userTestsDetailsModal', { static: true }) modal!: ModalDirective;
  userTestsDetails$: Observable<PagedResponse<UserTestDetailDto> | any> = this.store.pipe(select(selectUserTestsDetails));

  tableFilter: TableFilter = new TableFilter();
  userId: string = '';

  resultStatuses :{ [key: number]: any } = {
    0: {
      label: 'Low'
    },
    4: {
      label: 'Unknown'
    },
    3: {
      label: 'Excelent'
    },
    1: {
      label: 'Satisfactory'
    },
    2: {
      label: 'Good'
    }
  }

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:UserTestService,
  ) { }

  ngOnInit(): void {
  }

  show(userId: any): void {
    this.userId = userId;
    this.onLazyLoad();

    this.modal.show();
  }

  onLazyLoad(event?: LazyLoadEvent){
    if(event)
    {
      const { first, rows } = event;
      const pageIndex = Math.ceil((first as number)/ (rows as number)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: rows as number,
        userId: this.userId
      };
      this.store.dispatch(new GetUserTestsDetails(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage,
        userId: this.userId
      };
      this.store.dispatch(new GetUserTestsDetails(request));
    }
  }

  close(): void {
    this.modal.hide();
  }

  reAssignTest(userTestId: any){
    this.service.reAssignTest(userTestId).toPromise().then(
      (res: any) => {
        if(res){
          this.toastr.success('Test ReAssigned to User!', 'ReAssigned successful.');
          this.onLazyLoad();
        }else{
          res.errors.forEach((element:any) => {
            this.toastr.error(element.description,'ReAssign failed.');
          });
        }
      },
      err => {
        this.toastr.error(err,'ReAssigned failed.');
        console.log(err)
      }
    );
  }
}
