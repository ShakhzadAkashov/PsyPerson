import { PagedResponse } from "src/app/models/base";
import { UserTestUserDto } from "src/app/models/userTests.model";

export interface UserTestState{
    userTestUsers: PagedResponse<UserTestUserDto>;
}

export const initialUserTestState: UserTestState = {
    userTestUsers: { data:[], total:0, loading: true }
};