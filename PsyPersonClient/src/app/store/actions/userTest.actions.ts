import { Action } from "@ngrx/store";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { UserTestDto, UserTestUserDto } from "src/app/models/userTests.model";

export enum EUserTestActions{
    GetUserTestUsers = '[User Test] Get User Test Users',
    GetUserTestUsersSuccess = '[User Test] Get User Test Users Success',
    GetUserTests = '[User Test] Get User Tests',
    GetUserTestsSuccess = '[User Test] Get User Tests Success',
}

export class GetUserTestUsers implements Action{
    public readonly type = EUserTestActions.GetUserTestUsers;
    constructor(public payload: PagedRequest){}
}

export class GetUserTestUsersSuccess implements Action{
    public readonly type = EUserTestActions.GetUserTestUsersSuccess;
    constructor(public payload: PagedResponse<UserTestUserDto>){}
}

export class GetUserTests implements Action{
    public readonly type = EUserTestActions.GetUserTests;
    constructor(public payload: PagedRequest){}
}

export class GetUserTestsSuccess implements Action{
    public readonly type = EUserTestActions.GetUserTestsSuccess;
    constructor(public payload: PagedResponse<UserTestDto>){}
}

export type UserTestActions = GetUserTestUsers | GetUserTestUsersSuccess | GetUserTests | GetUserTestsSuccess;