import { createReducer, on } from "@ngrx/store";
import { UserDto } from "src/app/models/users.models";
import { EUserActions, GetUsersSuccess,UserActions } from "../actions/user.actions";
import { initialUserState, UserState } from "../state/user.state";

export const userReducers = (
    state = initialUserState,
    action: UserActions
): UserState => {
    switch(action.type){
        case EUserActions.GetUsersSuccess: {
            return {
                ...state,
                users:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }
        case EUserActions.GetUserSuccess: {
            return {
                ...state,
                selectedUser: action.payload
            };
        }

        case EUserActions.GetUserRolesSuccess: {
            return {
                ...state,
                userRoles:{
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