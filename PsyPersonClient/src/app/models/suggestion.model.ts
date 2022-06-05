import { TestResultStatusEnum } from "./tests.models";

export class SuggestionDto{
    id: string = '';
    name: string = '';
    description: string = '';
    rangeFrom: number = 0.0;
    rangeTo: number = 0.0;
    status = TestResultStatusEnum;
    selectionType = SuggestionSelectTypeEnum;
}

export enum SuggestionSelectTypeEnum{
    SelectByRange = 0,
    SelectByStatus = 1,
}
