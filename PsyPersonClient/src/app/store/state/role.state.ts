import { PagedResponse } from "src/app/models/base";
import { RoleDto } from "src/app/models/roles.models";

export interface RoleState{
    roles: PagedResponse<RoleDto>;
    selectedRole: RoleDto;
}

export const initialRoleState: RoleState = {
    roles: { data:[], total:0, loading: true },
    selectedRole: <RoleDto>{}
};