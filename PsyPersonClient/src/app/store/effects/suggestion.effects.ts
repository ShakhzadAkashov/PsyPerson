import { Injectable } from "@angular/core";
import { Actions, createEffect, Effect, ofType } from "@ngrx/effects";
import { createEffects } from "@ngrx/effects/src/effects_module";
import { select, Store } from "@ngrx/store";
import { EMPTY, of } from "rxjs";
import { catchError, map, switchMap, withLatestFrom } from "rxjs/operators";
import { PagedRequest } from "src/app/models/base";
import { SuggestionService } from "src/app/services/api/suggestion.service";
import { UserService } from "src/app/services/api/user.service";
import { ESuggestionActions, GetSuggestion, GetSuggestions, GetSuggestionsByStatus, GetSuggestionsByStatusSuccess, GetSuggestionsSuccess, GetSuggestionSuccess } from "../actions/suggestion.actions";
import { EUserActions, GetUser, GetUserRoles, GetUserRolesSuccess, GetUsers, GetUsersSuccess, GetUserSuccess } from "../actions/user.actions";
import { selectSuggestionList } from "../selectors/suggestion.selector";
import { selectUserList } from "../selectors/user.selector";
import { AppState } from "../state/app.state";

@Injectable()
export class SuggestionEffects{
    constructor(
        private service: SuggestionService, 
        private store: Store<AppState>, 
        private actions$: Actions){}

    getSuggestion$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetSuggestion>(ESuggestionActions.GetSuggestion),
        map(action => action.payload),
        withLatestFrom(this.store.pipe(select(selectSuggestionList))),
        switchMap(([id, suggestions]) => {
        const selectedSuggestion = suggestions.data.filter(suggestions => suggestions.id === id)[0];
        return of(new GetSuggestionSuccess(selectedSuggestion));
        })  
    ));

    getSuggestions$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetSuggestions>(ESuggestionActions.GetSuggestions),
        switchMap((u) => this.service.getAll(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((s) => new GetSuggestionsSuccess(s)),
            catchError((error) => {console.log('GetSuggestions | error ',error); throw error})
        ))
    ));

    getSuggestionByStatus$ = createEffect(() => 
    this.actions$.pipe(
        ofType<GetSuggestionsByStatus>(ESuggestionActions.GetSuggestionsByStatus),
        switchMap((u) => this.service.getByStatus(u.payload).pipe(
            map(r => {r.loading = false; return r} ),
            map((s) => new GetSuggestionsByStatusSuccess(s)),
            catchError((error) => {console.log('GetSuggestionsByStatus | error ',error); throw error})
        ))
    ));
}