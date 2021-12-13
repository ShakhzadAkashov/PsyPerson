import { createSelector } from "@ngrx/store";
import { AppState } from "../state/app.state";
import { TestState } from "../state/test.state";
import { UserState} from "../state/user.state";

const selectTests = (state:AppState) => state.tests;

export const selectTestList = createSelector(
    selectTests,
    (state: TestState) => state.tests
);

export const selectselectedTest = createSelector(
    selectTests,
    (state: TestState) => state.selectedTest
);

export const selectTestQuestionList = createSelector(
    selectTests,
    (state: TestState) => state.testQuestions
);

export const selectTestQuestion = createSelector(
    selectTests,
    (state: TestState) => state.selectedTestQuestion
);

export const selectTestForTesting = createSelector(
    selectTests,
    (state: TestState) => state.testForTesting
);

export const selectTestsForLookupTable = createSelector(
    selectTests,
    (state: TestState) => state.testsForLookupTable
);