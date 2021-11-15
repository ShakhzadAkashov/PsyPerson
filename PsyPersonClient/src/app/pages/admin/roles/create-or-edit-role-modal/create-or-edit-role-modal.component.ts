import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { RoleDto } from 'src/app/models/roles.models';
import { RoleService } from 'src/app/services/api/role.service';
import { GetRole } from 'src/app/store/actions/role.actions';
import { selectselectedRole } from 'src/app/store/selectors/role.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-create-or-edit-role-modal',
  templateUrl: './create-or-edit-role-modal.component.html',
  styleUrls: ['./create-or-edit-role-modal.component.css']
})
export class CreateOrEditRoleModalComponent implements OnInit {

  @ViewChild('createOrEditModal', { static: true }) modal!: ModalDirective;
  @Output() modalSave: EventEmitter<RoleDto> = new EventEmitter<RoleDto>();
  role$: Observable<RoleDto> = this.store.pipe(select(selectselectedRole));

  active = false;
  saving = false;
  edit = false;

  role: RoleDto = new RoleDto();

  constructor(
    private store: Store<AppState>,
    private toastr: ToastrService, 
    private service:RoleService, 
  ) { }

  ngOnInit(): void {
  }

  show(role?: RoleDto): void { 
    this.edit = false;
    
    if (!role) {
        this.role = new RoleDto();

        this.active = true;
        this.modal.show();
    } else {
        this.store.dispatch(new GetRole(role.id));
        this.role$.subscribe(res => {
          let r = res; 
          this.role.id = r.id;
          this.role.name = r.name;
          this.role.description = r.description;
        });

        this.edit = true;
        this.active = true;
        this.modal.show();
    }
  }

  save(): void {
    this.saving = true;

    var appRole = new RoleDto();

    appRole.id = this.role.id;
    appRole.name = this.role.name;
    appRole.description = this.role.description;
    appRole.createdDate = this.role.createdDate;
    appRole.normalizedName = this.role.normalizedName;

    if(this.edit == true){
      this.service.update(appRole)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(res =>{
        this.toastr.success('Saved!', 'Role Saved successful.');
        this.close();
        this.modalSave.emit(appRole);
      });
    }else{
      this.service?.create(appRole)
      .pipe(finalize(() => { this.saving = false;}))
      .toPromise().then(
        (res: any) => {
          if(res.succeeded){
            this.toastr?.success('New role created!', 'Created successful.');
            this.close();
            this.modalSave.emit(appRole);
          }else{
            res.errors.forEach((element:any) => {
              switch(element.code)
              {
                case 'DuplicateRoleName':
                  this.toastr?.error('Role is already taken','Created failed.');
                  break;
                default:
                  this.toastr?.error(element.description,'Created failed.');
                  break;
              }
            });
          }
        },
        err => {
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
