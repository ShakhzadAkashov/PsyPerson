import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { select, Store } from "@ngrx/store";
import { EMPTY, of } from "rxjs";
import { catchError, map, switchMap, withLatestFrom } from "rxjs/operators";
import { RoleService } from "src/app/services/api/role.service";
import { ERoleActions, GetRole, GetRoles, GetRolesSuccess, GetRoleSuccess } from "../actions/role.actions";
import { selectRoleList } from "../selectors/role.selector";
import { AppState } from "../state/app.state";

@Injectable()
export class RoleEffects{
    constructor(
        private service: RoleService, 
        private store: Store<AppState>, 
        private actions$: Actions){}

    getRole$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetRole>(ERoleActions.GetRole),
        map(action => action.payload),
        withLatestFrom(this.store.pipe(select(selectRoleList))),
        switchMap(([id, roles]) => {
        const selectedRole = roles.data.filter(user => user.id === id)[0];
        return of(new GetRoleSuccess(selectedRole));
        })  
    ));

    getRoles$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetRoles>(ERoleActions.GetRoles),
        switchMap((u) => this.service.getAll(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((users) => new GetRolesSuccess(users)),
            catchError((error) => {console.log('GetRoles | error ',error); throw error})
        ))
    ));
}