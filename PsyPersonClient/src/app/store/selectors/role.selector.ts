import { createSelector } from "@ngrx/store";
import { AppState } from "../state/app.state";
import { RoleState } from "../state/role.state";

const selectRoles = (state:AppState) => state.roles;

export const selectRoleList = createSelector(
    selectRoles,
    (state: RoleState) => state.roles
);

export const selectselectedRole = createSelector(
    selectRoles,
    (state: RoleState) => state.selectedRole
);