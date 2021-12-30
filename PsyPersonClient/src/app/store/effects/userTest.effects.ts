import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { Store} from "@ngrx/store";
import { UserTestService } from "src/app/services/api/userTest.service";
import { EUserTestActions, GetTestingHistory, GetTestingHistorySuccess, GetUserTestingListForCheck, GetUserTestingListForCheckSuccess, GetUserTests, GetUserTestsDetails, GetUserTestsDetailsSuccess, GetUserTestsSuccess, GetUserTestUsers, GetUserTestUsersSuccess } from "../actions/userTest.actions";
import { AppState } from "../state/app.state";
import { catchError, map, switchMap, withLatestFrom } from "rxjs/operators";
import { TestService } from "src/app/services/api/test.service";

@Injectable()
export class UserTestEffects{
    constructor(
        private service: UserTestService,
        private store: Store<AppState>, 
        private actions$: Actions){}

    getUserTestUsers$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetUserTestUsers>(EUserTestActions.GetUserTestUsers),
        switchMap((u) => this.service.getUserTestUsers(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((users) => new GetUserTestUsersSuccess(users)),
            catchError((error) => {console.log('GetUserTestUsers | error ',error); throw error})
        ))
    ));

    getUserTests$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetUserTests>(EUserTestActions.GetUserTests),
        switchMap((u) => this.service.getUserTests(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((users) => new GetUserTestsSuccess(users)),
            catchError((error) => {console.log('GetUserTests | error ',error); throw error})
        ))
    ));

    getUserTestsDetails$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetUserTestsDetails>(EUserTestActions.GetUserTestsDetails),
        switchMap((u) => this.service.getUserTestsDetails(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((users) => new GetUserTestsDetailsSuccess(users)),
            catchError((error) => {console.log('GetUserTestsDetails | error ',error); throw error})
        ))
    ));

    getTestingHistory$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetTestingHistory>(EUserTestActions.GetTestingHistory),
        switchMap((u) => this.service.getTestingHistory(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((testing) => new GetTestingHistorySuccess(testing)),
            catchError((error) => {console.log('GetTestingHistory | error ',error); throw error})
        ))
    ));

    getUserTestingListForCheck$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetUserTestingListForCheck>(EUserTestActions.GetUserTestingListForCheck),
        switchMap((u) => this.service.getUserTestingsForCheck(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((testing) => new GetUserTestingListForCheckSuccess(testing)),
            catchError((error) => {console.log('GetUserTestingListForCheck | error ',error); throw error})
        ))
    ));
}