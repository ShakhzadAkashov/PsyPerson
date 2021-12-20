import { PagedResponse } from "src/app/models/base";
import { TestingHistoryDto, UserTestDetailDto, UserTestDto, UserTestUserDto } from "src/app/models/userTests.model";

export interface UserTestState{
    userTestUsers: PagedResponse<UserTestUserDto>;
    userTests: PagedResponse<UserTestDto>;
    userTestsDetails: PagedResponse<UserTestDetailDto>;
    testingHistory: TestingHistoryDto;
}

export const initialUserTestState: UserTestState = {
    userTestUsers: { data:[], total:0, loading: true },
    userTests: { data:[], total:0, loading: true },
    userTestsDetails: { data:[], total:0, loading: true },
    testingHistory: { data:[], total:0, loading: true, testName:'',testScore:0 }
};