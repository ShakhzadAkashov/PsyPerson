import { PagedResponse } from "src/app/models/base";
import { RoleDto } from "src/app/models/roles.models";
import { UserDto } from "src/app/models/users.models";

export interface UserState{
    users: PagedResponse<UserDto>;
    userRoles: PagedResponse<RoleDto>;
    selectedUser: UserDto;
}

export const initialUserState: UserState = {
    users: { data:[], total:0, loading: true },
    userRoles: { data:[], total:0, loading: true },
    selectedUser: <UserDto>{}
};