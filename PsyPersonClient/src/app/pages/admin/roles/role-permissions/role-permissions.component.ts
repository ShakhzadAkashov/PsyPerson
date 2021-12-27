import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { CheckBoxDto, RolePermissionsDto } from 'src/app/models/roles.models';
import { RoleService } from 'src/app/services/api/role.service';
import { GetRolePermissions } from 'src/app/store/actions/role.actions';
import { selectRolePermissions } from 'src/app/store/selectors/role.selector';
import { AppState } from 'src/app/store/state/app.state';

@Component({
  selector: 'app-role-permissions',
  templateUrl: './role-permissions.component.html',
  styleUrls: ['./role-permissions.component.css']
})
export class RolePermissionsComponent implements OnInit {

  rolePermissions$: Observable<RolePermissionsDto> = this.store.pipe(select(selectRolePermissions));

  saving: boolean = false;
  roleId: string = '';
  from: string = '';
  loading: boolean = false;
  filterText: string = '';
  rolePermissions: RolePermissionsDto = new RolePermissionsDto();

  constructor(
    private store: Store<AppState>, 
    private service:RoleService, 
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
  ) {
    const roleId = this.activatedRoute.snapshot.queryParams['roleId'];
    if(roleId) this.roleId = roleId;

    const from = this.activatedRoute.snapshot.queryParams['from'];
    if(from) this.from = from;
   }

  ngOnInit(): void {
    this.getRolePermissions(this.roleId);
  }

  getRolePermissions(roleId: string){
    this.store.dispatch(new GetRolePermissions(roleId));
    this.loading = true;

    this.rolePermissions$.subscribe(result => {
      let res = result;
      this.rolePermissions.roleName = res.roleName;
      this.rolePermissions.roleId = res.roleId;

      this.rolePermissions.roleClaims = [];
      res.roleClaims.forEach(e => {
        let claim = new CheckBoxDto();
        claim.displayValue = e.displayValue;
        claim.isSelected = e.isSelected

        this.rolePermissions.roleClaims.push(claim);
        this.loading = false;
      });
    });
  }

  assignPermissions(){
    this.saving = true;

    this.service.assignPermissionsToRole(this.rolePermissions)
    .pipe(finalize(() => { this.saving = false;}))
    .toPromise().then(
      (res: any) => {
        if(res == true){
          this.toastr.success('Permissions Assigned!', 'Assigned successful.');
        }else{
          this.toastr.error("Assign Permissions Failed",'Assign failed.');
        }
      },
      err => {
        this.toastr.error(err,'Assigned failed.');
        console.log(err)
      }
    );
  }

  filterInput(event: any){}

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }
}
