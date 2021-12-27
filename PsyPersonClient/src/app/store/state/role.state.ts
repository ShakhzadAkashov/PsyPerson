import { PagedResponse } from "src/app/models/base";
import { RoleDto, RolePermissionsDto } from "src/app/models/roles.models";

export interface RoleState{
    roles: PagedResponse<RoleDto>;
    selectedRole: RoleDto;
    rolePermissions: RolePermissionsDto;
}

export const initialRoleState: RoleState = {
    roles: { data:[], total:0, loading: true },
    selectedRole: <RoleDto>{},
    rolePermissions: {roleId:'', roleName:'', roleClaims:[]}
};