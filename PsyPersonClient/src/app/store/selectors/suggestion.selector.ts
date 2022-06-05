import { createSelector } from "@ngrx/store";
import { AppState } from "../state/app.state";
import { SuggestionState } from "../state/suggestion.state";
import { UserState} from "../state/user.state";

const selectSuggestions = (state:AppState) => state.suggestions;

export const selectSuggestionList = createSelector(
    selectSuggestions,
    (state: SuggestionState) => state.suggestions
);

export const selectselectedSuggestion = createSelector(
    selectSuggestions,
    (state: SuggestionState) => state.selectedSuggestion
);

export const selectSuggestionsByStatus = createSelector(
    selectSuggestions,
    (state: SuggestionState) => state.suggestionsByStatus
);