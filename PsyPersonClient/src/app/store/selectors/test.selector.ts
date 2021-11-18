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