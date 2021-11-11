import { Action, createAction, props } from "@ngrx/store";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { UserDto } from "src/app/models/users.models";

export enum EUserActions{
    GetUsers = '[User] Get Users',
    GetUsersSuccess = '[User] Get Users Success',
    GetUser = '[User] Get User',
    GetUserSuccess = '[User] Get User Success',
}

export class GetUsers implements Action{
    public readonly type = EUserActions.GetUsers;
    constructor(public payload: PagedRequest){}
}

export class GetUsersSuccess implements Action{
    public readonly type = EUserActions.GetUsersSuccess;
    constructor(public payload: PagedResponse<UserDto>){}
}

export class GetUser implements Action{
    public readonly type = EUserActions.GetUser;
    constructor(public payload: string){}
}

export class GetUserSuccess implements Action{
    public readonly type = EUserActions.GetUserSuccess;
    constructor(public payload: UserDto){}
}

export type UserActions = GetUsers | GetUsersSuccess | GetUser | GetUserSuccess;