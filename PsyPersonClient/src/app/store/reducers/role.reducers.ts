import { ERoleActions, RoleActions } from "../actions/role.actions";
import { initialRoleState, RoleState } from "../state/role.state";

export const roleReducers = (
    state = initialRoleState,
    action: RoleActions
): RoleState => {
    switch(action.type){
        case ERoleActions.GetRolesSuccess: {
            return {
                ...state,
                roles:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }
        case ERoleActions.GetRoleSuccess: {
            return {
                ...state,
                selectedRole: action.payload
            };
        }

        default:
            return state;
    }
}