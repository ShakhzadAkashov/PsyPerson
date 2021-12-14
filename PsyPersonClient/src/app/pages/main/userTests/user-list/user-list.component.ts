import { Component, OnInit, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { Observable } from 'rxjs';
import { PagedRequest, PagedResponse, TableFilter } from 'src/app/models/base';
import { TestDto } from 'src/app/models/tests.models';
import { UserTestDto, UserTestUserDto } from 'src/app/models/userTests.model';
import { UserTestService } from 'src/app/services/api/userTest.service';
import { GetUserTestUsers } from 'src/app/store/actions/userTest.actions';
import { selectUserTestUsers } from 'src/app/store/selectors/userTest.selector';
import { AppState } from 'src/app/store/state/app.state';
import { TestLookupTableModalComponent } from '../../common/test-lookup-table-modal/test-lookup-table-modal.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  @ViewChild('testLookupTableModal', { static: true }) testLookupTableModal: TestLookupTableModalComponent = new TestLookupTableModalComponent(this.store);
  users$: Observable<PagedResponse<UserTestUserDto> | any> = this.store.pipe(select(selectUserTestUsers));
  filterText='';
  tableFilter: TableFilter = new TableFilter();

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:UserTestService,
    ) {}

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
        itemPerPage: rows as number
      };
      this.store.dispatch(new GetUserTestUsers(request));
    }else
    {
      const pageIndex = Math.ceil((this.tableFilter.first)/ (this.tableFilter.itemPerPage)) + 1;  
      let request: PagedRequest = {
        page: pageIndex,
        itemPerPage: this.tableFilter.itemPerPage
      };
      this.store.dispatch(new GetUserTestUsers(request));
    }
  }

  filterArray(arr: UserTestDto[], isTested: boolean){
    if(isTested)
      return arr.filter(x => x.isTested === true).length;
    else
      return arr.filter(x => x.isTested === false).length;
  }

  openSelectTestModal(userId: string){
    this.testLookupTableModal.show(userId);
  }

  selectTest(response: any){
    this.service.create(response.userId, response.testId).toPromise().then(
      (res: any) => {
        if(res){
          this.toastr.success('New Test Assigned to User!', 'Assigned successful.');
          this.onLazyLoad();
        }else{
          res.errors.forEach((element:any) => {
            this.toastr.error(element.description,'Assign failed.');
          });
        }
      },
      err => {
        this.toastr.error(err,'Assigned failed.');
        console.log(err)
      }
    );
  }
}
