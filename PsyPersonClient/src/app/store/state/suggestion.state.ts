import { PagedResponse } from "src/app/models/base";
import { RoleDto } from "src/app/models/roles.models";
import { SuggestionDto } from "src/app/models/suggestion.model";
import { UserDto } from "src/app/models/users.models";

export interface SuggestionState{
    suggestions: PagedResponse<SuggestionDto>;
    suggestionsByStatus: PagedResponse<SuggestionDto>;
    selectedSuggestion: SuggestionDto;
}

export const initialSuggestionState: SuggestionState = {
    suggestions: { data:[], total:0, loading: true },
    suggestionsByStatus: { data:[], total:0, loading: true },
    selectedSuggestion: <SuggestionDto>{}
};