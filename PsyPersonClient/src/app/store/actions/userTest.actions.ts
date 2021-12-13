import { Action } from "@ngrx/store";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { UserTestUserDto } from "src/app/models/userTests.model";

export enum EUserTestActions{
    GetUserTestUsers = '[User Test] Get User Test Users',
    GetUserTestUsersSuccess = '[User Test] Get User Test Users Success',
}

export class GetUserTestUsers implements Action{
    public readonly type = EUserTestActions.GetUserTestUsers;
    constructor(public payload: PagedRequest){}
}

export class GetUserTestUsersSuccess implements Action{
    public readonly type = EUserTestActions.GetUserTestUsersSuccess;
    constructor(public payload: PagedResponse<UserTestUserDto>){}
}

export type UserTestActions = GetUserTestUsers | GetUserTestUsersSuccess;