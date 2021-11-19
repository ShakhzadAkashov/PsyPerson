import { PagedResponse } from "src/app/models/base";
import { TestDto } from "src/app/models/tests.models";

export interface TestState{
    tests: PagedResponse<TestDto>;
    selectedTest: TestDto;
}

export const initialTestState: TestState = {
    tests: { data:[], total:0, loading: true },
    selectedTest: <TestDto>{}
};