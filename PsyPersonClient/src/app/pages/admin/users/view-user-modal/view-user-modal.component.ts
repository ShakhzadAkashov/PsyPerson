import { Component, OnInit, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { UserDto } from 'src/app/models/users.models';
import { GetUser } from 'src/app/store/actions/user.actions';
import { selectselectedUser } from 'src/app/store/selectors/user.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-view-user-modal',
  templateUrl: './view-user-modal.component.html',
  styleUrls: ['./view-user-modal.component.css']
})
export class ViewUserModalComponent implements OnInit {

  @ViewChild('viewModal', { static: true }) modal!: ModalDirective; 
  user$: Observable<UserDto> = this.store.pipe(select(selectselectedUser));

  active = false;
  saving = false;

  user: UserDto = new UserDto();

  constructor(private store: Store<AppState>,) { }

  ngOnInit(): void {
  }

  show(id: any): void {
    this.store.dispatch(new GetUser(id));
    this.user$.subscribe(res => {
      this.user = res;
    });

    this.active = true;
    this.modal.show();
  }

  close(): void {
      this.active = false;
      this.modal.hide();
  }
}
