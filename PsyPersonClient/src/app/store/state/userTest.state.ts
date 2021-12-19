import { PagedResponse } from "src/app/models/base";
import { UserTestDetailDto, UserTestDto, UserTestUserDto } from "src/app/models/userTests.model";

export interface UserTestState{
    userTestUsers: PagedResponse<UserTestUserDto>;
    userTests: PagedResponse<UserTestDto>;
    userTestsDetails: PagedResponse<UserTestDetailDto>;
}

export const initialUserTestState: UserTestState = {
    userTestUsers: { data:[], total:0, loading: true },
    userTests: { data:[], total:0, loading: true },
    userTestsDetails: { data:[], total:0, loading: true }
};