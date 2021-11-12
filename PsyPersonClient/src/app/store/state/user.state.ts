import { PagedResponse } from "src/app/models/base";
import { UserDto } from "src/app/models/users.models";

export interface UserState{
    users: PagedResponse<UserDto>;
    selectedUser: UserDto;
}

export const initialUserState: UserState = {
    users: { data:[], total:0, loading: true },
    selectedUser: <UserDto>{}
};