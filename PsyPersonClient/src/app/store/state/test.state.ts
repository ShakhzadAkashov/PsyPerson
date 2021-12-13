import { PagedResponse } from "src/app/models/base";
import { TestDto, TestForTestingDto, TestQuestionDto } from "src/app/models/tests.models";

export interface TestState{
    tests: PagedResponse<TestDto>;
    testQuestions: PagedResponse<TestQuestionDto>;
    selectedTest: TestDto;
    selectedTestQuestion: TestQuestionDto;
    testForTesting: TestForTestingDto;
    testsForLookupTable: PagedResponse<TestDto>;
}

export const initialTestState: TestState = {
    tests: { data:[], total:0, loading: true },
    testQuestions: { data:[], total:0, loading: true },
    selectedTest: <TestDto>{},
    selectedTestQuestion: <TestQuestionDto>{},
    testForTesting: <TestForTestingDto>{test:new TestDto, testQuestionList:[]},
    testsForLookupTable: { data:[], total:0, loading: true },
};