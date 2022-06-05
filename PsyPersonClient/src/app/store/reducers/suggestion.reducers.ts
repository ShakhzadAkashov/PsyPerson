import { ESuggestionActions, SuggestionActions } from "../actions/suggestion.actions";
import { initialSuggestionState, SuggestionState } from "../state/suggestion.state";

export const suggestionReducers = (
    state = initialSuggestionState,
    action: SuggestionActions
): SuggestionState => {
    switch(action.type){
        case ESuggestionActions.GetSuggestionsSuccess: {
            return {
                ...state,
                suggestions:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }
        case ESuggestionActions.GetSuggestionSuccess: {
            return {
                ...state,
                selectedSuggestion: action.payload
            };
        }

        case ESuggestionActions.GetSuggestionsByStatusSuccess: {
            return {
                ...state,
                suggestionsByStatus:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }

        default:
            return state;
    }
}