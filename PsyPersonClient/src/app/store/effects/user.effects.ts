import { Injectable } from "@angular/core";
import { Actions, createEffect, Effect, ofType } from "@ngrx/effects";
import { createEffects } from "@ngrx/effects/src/effects_module";
import { select, Store } from "@ngrx/store";
import { EMPTY, of } from "rxjs";
import { catchError, map, switchMap, withLatestFrom } from "rxjs/operators";
import { PagedRequest } from "src/app/models/base";
import { UserService } from "src/app/services/api/user.service";
import { EUserActions, GetUser, GetUsers, GetUsersSuccess, GetUserSuccess } from "../actions/user.actions";
import { selectUserList } from "../selectors/user.selector";
import { AppState } from "../state/app.state";

@Injectable()
export class UserEffects{
    constructor(
        private service: UserService, 
        private store: Store<AppState>, 
        private actions$: Actions){}

    getUser$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetUser>(EUserActions.GetUser),
        map(action => action.payload),
        withLatestFrom(this.store.pipe(select(selectUserList))),
        switchMap(([id, users]) => {
        const selectedUser = users.data.filter(user => user.id === id)[0];
        return of(new GetUserSuccess(selectedUser));
        })  
    ));

    // getUsers$ = createEffect(() => 
    // this.actions$.pipe(
    //     ofType<GetUsers>(EUserActions.GetUsers),
    //     switchMap((u) => this.service.getAll(u.payload).pipe(map(r => {r.loading = true; return r} ))),
    //     switchMap((u) => of(new GetUsersSuccess(u))),catchError(() => EMPTY)
    // ));
    getUsers$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetUsers>(EUserActions.GetUsers),
        switchMap((u) => this.service.getAll(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((users) => new GetUsersSuccess(users)),
            catchError((error) => {console.log('GetUsers | error ',error); throw error})
        ))
    ));
}