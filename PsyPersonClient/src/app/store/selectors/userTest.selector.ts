import { createSelector } from "@ngrx/store";
import { AppState } from "../state/app.state";
import { UserTestState } from "../state/userTest.state";

const selectUserTest = (state:AppState) => state.userTests;

export const selectUserTestUsers = createSelector(
    selectUserTest,
    (state: UserTestState) => state.userTestUsers
);

export const selectUserTests = createSelector(
    selectUserTest,
    (state: UserTestState) => state.userTests
);

export const selectUserTestsDetails = createSelector(
    selectUserTest,
    (state: UserTestState) => state.userTestsDetails
);

export const selectTestingHistory = createSelector(
    selectUserTest,
    (state: UserTestState) => state.testingHistory
);