import { RouterReducerState } from "@ngrx/router-store";
import { initialRoleState, RoleState } from "./role.state";
import { initialTestState, TestState } from "./test.state";
import { initialUserState, UserState} from "./user.state";

export interface AppState{
    router?: RouterReducerState;
    users: UserState;
    roles: RoleState;
    tests: TestState;
}

export const initialAppState: AppState = {
    users: initialUserState,
    roles: initialRoleState,
    tests: initialTestState
}

export function getInitialState(): AppState{
    return initialAppState;
}