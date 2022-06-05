import { RouterReducerState } from "@ngrx/router-store";
import { initialRoleState, RoleState } from "./role.state";
import { initialSuggestionState, SuggestionState } from "./suggestion.state";
import { initialTestState, TestState } from "./test.state";
import { initialUserState, UserState} from "./user.state";
import { initialUserTestState, UserTestState } from "./userTest.state";

export interface AppState{
    router?: RouterReducerState;
    users: UserState;
    roles: RoleState;
    tests: TestState;
    userTests: UserTestState;
    suggestions: SuggestionState;
}

export const initialAppState: AppState = {
    users: initialUserState,
    roles: initialRoleState,
    tests: initialTestState,
    userTests: initialUserTestState,
    suggestions : initialSuggestionState
}

export function getInitialState(): AppState{
    return initialAppState;
}