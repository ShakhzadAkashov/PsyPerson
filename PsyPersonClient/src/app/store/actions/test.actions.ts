import { Action, createAction, props } from "@ngrx/store";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { TestDto, TestForTestingDto, TestQuestionDto } from "src/app/models/tests.models";

export enum ETestActions{
    GetTests = '[Test] Get Tests',
    GetTestsSuccess = '[Test] Get Tests Success',
    GetTest = '[Test] Get Test',
    GetTestSuccess = '[Test] Get Test Success',
    GetTestQuestions = '[Test] Get Test Questions',
    GetTestQuestionsSuccess = '[Test] Get Test Questions Success',
    GetTestQuestion = '[Test] Get Test Question',
    GetTestQuestionSuccess = '[Test] Get Test Question Success',
    GetTestForTesting = '[Test] Get Test For Testing',
    GetTestForTestingSuccess = '[Test] Get Test For Testing Success',
    GetTestsForLookupTable = '[Test] Get Tests For Lookup Table',
    GetTestsForLookupTableSuccess = '[Test] Get Tests For Lookup Table Success',
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

export class GetTestQuestions implements Action{
    public readonly type = ETestActions.GetTestQuestions;
    constructor(public payload: PagedRequest){}
}

export class GetTestQuestionsSuccess implements Action{
    public readonly type = ETestActions.GetTestQuestionsSuccess;
    constructor(public payload: PagedResponse<TestQuestionDto>){}
}

export class GetTestQuestion implements Action{
    public readonly type = ETestActions.GetTestQuestion;
    constructor(public payload: string){}
}

export class GetTestQuestionSuccess implements Action{
    public readonly type = ETestActions.GetTestQuestionSuccess;
    constructor(public payload: TestQuestionDto){}
}

export class GetTestForTesting implements Action{
    public readonly type = ETestActions.GetTestForTesting;
    constructor(public payload: string){}
}

export class GetTestForTestingSuccess implements Action{
    public readonly type = ETestActions.GetTestForTestingSuccess;
    constructor(public payload: TestForTestingDto){}
}

export class GetTestsForLookupTable implements Action{
    public readonly type = ETestActions.GetTestsForLookupTable;
    constructor(public payload: PagedRequest){}
}

export class GetTestsForLookupTableSuccess implements Action{
    public readonly type = ETestActions.GetTestsForLookupTableSuccess;
    constructor(public payload: PagedResponse<TestDto>){}
}

export type TestActions = GetTests | GetTestsSuccess | GetTest | GetTestSuccess 
                        | GetTestQuestions | GetTestQuestionsSuccess | GetTestQuestion | GetTestQuestionSuccess
                        | GetTestForTesting | GetTestForTestingSuccess | GetTestsForLookupTable | GetTestsForLookupTableSuccess;