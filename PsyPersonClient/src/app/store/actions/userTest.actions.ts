import { Action } from "@ngrx/store";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { TestingHistoryDto, UserTestDetailDto, UserTestDto, UserTestingHistoryDto, UserTestUserDto } from "src/app/models/userTests.model";

export enum EUserTestActions{
    GetUserTestUsers = '[User Test] Get User Test Users',
    GetUserTestUsersSuccess = '[User Test] Get User Test Users Success',
    GetUserTests = '[User Test] Get User Tests',
    GetUserTestsSuccess = '[User Test] Get User Tests Success',
    GetUserTestsDetails = '[User Test] Get User Tests Details',
    GetUserTestsDetailsSuccess = '[User Test] Get User Tests Details Success',
    GetTestingHistory = '[User Test] Get Testing History',
    GetTestingHistorySuccess = '[User Test] Get Testing History Success',
    GetUserTestingListForCheck = '[User Test] Get User Testing List For Check',
    GetUserTestingListForCheckSuccess = '[User Test] Get User Testing List For Check Success',
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

export class GetUserTestsDetails implements Action{
    public readonly type = EUserTestActions.GetUserTestsDetails;
    constructor(public payload: PagedRequest){}
}

export class GetUserTestsDetailsSuccess implements Action{
    public readonly type = EUserTestActions.GetUserTestsDetailsSuccess;
    constructor(public payload: PagedResponse<UserTestDetailDto>){}
}

export class GetTestingHistory implements Action{
    public readonly type = EUserTestActions.GetTestingHistory;
    constructor(public payload: PagedRequest){}
}

export class GetTestingHistorySuccess implements Action{
    public readonly type = EUserTestActions.GetTestingHistorySuccess;
    constructor(public payload: TestingHistoryDto){}
}

export class GetUserTestingListForCheck implements Action{
    public readonly type = EUserTestActions.GetUserTestingListForCheck;
    constructor(public payload: PagedRequest){}
}

export class GetUserTestingListForCheckSuccess implements Action{
    public readonly type = EUserTestActions.GetUserTestingListForCheckSuccess;
    constructor(public payload: PagedResponse<UserTestingHistoryDto>){}
}

export type UserTestActions = GetUserTestUsers | GetUserTestUsersSuccess | GetUserTests | GetUserTestsSuccess
                            | GetUserTestsDetails | GetUserTestsDetailsSuccess | GetTestingHistory | GetTestingHistorySuccess
                            | GetUserTestingListForCheck | GetUserTestingListForCheckSuccess;