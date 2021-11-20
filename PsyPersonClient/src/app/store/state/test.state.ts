import { PagedResponse } from "src/app/models/base";
import { TestDto, TestQuestionDto } from "src/app/models/tests.models";

export interface TestState{
    tests: PagedResponse<TestDto>;
    testQuestions: PagedResponse<TestQuestionDto>;
    selectedTest: TestDto;
    selectedTestQuestion: TestQuestionDto;
}

export const initialTestState: TestState = {
    tests: { data:[], total:0, loading: true },
    testQuestions: { data:[], total:0, loading: true },
    selectedTest: <TestDto>{},
    selectedTestQuestion: <TestQuestionDto>{}
};