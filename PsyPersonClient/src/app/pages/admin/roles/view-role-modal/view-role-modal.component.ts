import { Component, OnInit, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { RoleDto } from 'src/app/models/roles.models';
import { GetRole } from 'src/app/store/actions/role.actions';
import { selectselectedRole } from 'src/app/store/selectors/role.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-view-role-modal',
  templateUrl: './view-role-modal.component.html',
  styleUrls: ['./view-role-modal.component.css']
})
export class ViewRoleModalComponent implements OnInit {

  @ViewChild('viewModal', { static: true }) modal!: ModalDirective; 
  role$: Observable<RoleDto> = this.store.pipe(select(selectselectedRole));

  active = false;
  saving = false;

  role: RoleDto = new RoleDto();

  constructor(private store: Store<AppState>,) { }

  ngOnInit(): void {
  }

  show(id: any): void {
    this.store.dispatch(new GetRole(id));
    this.role$.subscribe(res => {
      this.role = res;
    });

    this.active = true;
    this.modal.show();
  }

  close(): void {
      this.active = false;
      this.modal.hide();
  }

}
