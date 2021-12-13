import { createSelector } from "@ngrx/store";
import { AppState } from "../state/app.state";
import { UserTestState } from "../state/userTest.state";

const selectUserTest = (state:AppState) => state.userTests;

export const selectUserTestUsers = createSelector(
    selectUserTest,
    (state: UserTestState) => state.userTestUsers
);