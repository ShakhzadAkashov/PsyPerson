import { RouterReducerState } from "@ngrx/router-store";
import { initialUserState, UserState} from "./user.state";

export interface AppState{
    router?: RouterReducerState;
    users: UserState;
}

export const initialAppState: AppState = {
    users: initialUserState
}

export function getInitialState(): AppState{
    return initialAppState;
}