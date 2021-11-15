import { RouterReducerState } from "@ngrx/router-store";
import { initialRoleState, RoleState } from "./role.state";
import { initialUserState, UserState} from "./user.state";

export interface AppState{
    router?: RouterReducerState;
    users: UserState;
    roles: RoleState;
}

export const initialAppState: AppState = {
    users: initialUserState,
    roles: initialRoleState
}

export function getInitialState(): AppState{
    return initialAppState;
}