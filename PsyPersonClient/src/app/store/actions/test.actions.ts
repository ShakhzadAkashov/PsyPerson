import { Action, createAction, props } from "@ngrx/store";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { TestDto } from "src/app/models/tests.models";

export enum ETestActions{
    GetTests = '[Test] Get Tests',
    GetTestsSuccess = '[Test] Get Tests Success',
    GetTest = '[Test] Get Test',
    GetTestSuccess = '[Test] Get Test Success'
}

export class GetTests implements Action{
    public readonly type = ETestActions.GetTests;
    constructor(public payload: PagedRequest){}
}

export class GetTestsSuccess implements Action{
    public readonly type = ETestActions.GetTestsSuccess;
    constructor(public payload: PagedResponse<TestDto>){}
}

export class GetTest implements Action{
    public readonly type = ETestActions.GetTest;
    constructor(public payload: string){}
}

export class GetTestSuccess implements Action{
    public readonly type = ETestActions.GetTestSuccess;
    constructor(public payload: TestDto){}
}

export type TestActions = GetTests | GetTestsSuccess | GetTest | GetTestSuccess;