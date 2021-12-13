import { PagedResponse } from "src/app/models/base";
import { UserTestDto, UserTestUserDto } from "src/app/models/userTests.model";

export interface UserTestState{
    userTestUsers: PagedResponse<UserTestUserDto>;
    userTests: PagedResponse<UserTestDto>;
}

export const initialUserTestState: UserTestState = {
    userTestUsers: { data:[], total:0, loading: true },
    userTests: { data:[], total:0, loading: true }
};