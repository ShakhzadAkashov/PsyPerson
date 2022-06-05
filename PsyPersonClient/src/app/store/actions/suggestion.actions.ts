import { Action, createAction, props } from "@ngrx/store";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { SuggestionDto } from "src/app/models/suggestion.model";

export enum ESuggestionActions{
    GetSuggestions = '[Suggestion] Get Suggestions',
    GetSuggestionsSuccess = '[Suggestion] Get Suggestions Success',
    GetSuggestion = '[Suggestion] Get Suggestion',
    GetSuggestionSuccess = '[Suggestion] Get Suggestion Success',
    GetSuggestionsByStatus = '[Suggestion] Get Suggestions By Status',
    GetSuggestionsByStatusSuccess = '[Suggestion] Get UseSuggestions By Status Success',
}

export class GetSuggestions implements Action{
    public readonly type = ESuggestionActions.GetSuggestions;
    constructor(public payload: PagedRequest){}
}

export class GetSuggestionsSuccess implements Action{
    public readonly type = ESuggestionActions.GetSuggestionsSuccess;
    constructor(public payload: PagedResponse<SuggestionDto>){}
}

export class GetSuggestion implements Action{
    public readonly type = ESuggestionActions.GetSuggestion;
    constructor(public payload: string){}
}

export class GetSuggestionSuccess implements Action{
    public readonly type = ESuggestionActions.GetSuggestionSuccess;
    constructor(public payload: SuggestionDto){}
}

export class GetSuggestionsByStatus implements Action{
    public readonly type = ESuggestionActions.GetSuggestionsByStatus;
    constructor(public payload: PagedRequest){}
}

export class GetSuggestionsByStatusSuccess implements Action{
    public readonly type = ESuggestionActions.GetSuggestionsByStatusSuccess;
    constructor(public payload: PagedResponse<SuggestionDto>){}
}

export type SuggestionActions = GetSuggestions | GetSuggestionsSuccess | GetSuggestion | GetSuggestionSuccess
                                | GetSuggestionsByStatus | GetSuggestionsByStatusSuccess;