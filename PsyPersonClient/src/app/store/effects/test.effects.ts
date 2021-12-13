import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { select, Store } from "@ngrx/store";
import { EMPTY, of } from "rxjs";
import { catchError, map, switchMap, withLatestFrom } from "rxjs/operators";
import { TestService } from "src/app/services/api/test.service";
import { ETestActions, GetTest, GetTestForTesting, GetTestForTestingSuccess, GetTestQuestion, GetTestQuestions, GetTestQuestionsSuccess, GetTestQuestionSuccess, GetTests, GetTestsForLookupTable, GetTestsForLookupTableSuccess, GetTestsSuccess, GetTestSuccess } from "../actions/test.actions";
import { selectRoleList } from "../selectors/role.selector";
import { selectTestList, selectTestQuestionList } from "../selectors/test.selector";
import { AppState } from "../state/app.state";

@Injectable()
export class TestEffects{
    constructor(
        private service: TestService, 
        private store: Store<AppState>, 
        private actions$: Actions){}

    getTest$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetTest>(ETestActions.GetTest),
        map(action => action.payload),
        withLatestFrom(this.store.pipe(select(selectTestList))),
        switchMap(([id, tests]) => {
        const selectedTest = tests.data.filter(role => role.id === id)[0];
        return of(new GetTestSuccess(selectedTest));
        })  
    ));

    getTests$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetTests>(ETestActions.GetTests),
        switchMap((u) => this.service.getAll(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((tests) => new GetTestsSuccess(tests)),
            catchError((error) => {console.log('GetTests | error ',error); throw error})
        ))
    ));

    getTestQuestion$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetTestQuestion>(ETestActions.GetTestQuestion),
        map(action => action.payload),
        withLatestFrom(this.store.pipe(select(selectTestQuestionList))),
        switchMap(([id, question]) => {
        const selectedQuestion = question.data.filter(q => q.id === id)[0];
        return of(new GetTestQuestionSuccess(selectedQuestion));
        })  
    ));

    getTestQuestions$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetTestQuestions>(ETestActions.GetTestQuestions),
        switchMap((u) => this.service.getTestQuestions(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((questions) => new GetTestQuestionsSuccess(questions)),
            catchError((error) => {console.log('GetTestQuestions | error ',error); throw error})
        ))
    ));

    getTestForTesting$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetTestForTesting>(ETestActions.GetTestForTesting),
        switchMap((u) => this.service.getTestForTesting(u.payload).pipe(
            map((questions) => new GetTestForTestingSuccess(questions)),
            catchError((error) => {console.log('GetTestForTesting | error ',error); throw error})
        ))
    ));

    getTestsForLookupTable$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetTestsForLookupTable>(ETestActions.GetTestsForLookupTable),
        switchMap((u) => this.service.TestsForLookupTable(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((tests) => new GetTestsForLookupTableSuccess(tests)),
            catchError((error) => {console.log('GetTestsForLookupTable | error ',error); throw error})
        ))
    ));
}
