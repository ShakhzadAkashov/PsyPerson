import { AnswerResultStatusEnum } from "./userTests.model";

export class TestDto{
    id: string = '';
    name: string = '';
    description: string = '';
    imgPath: string = '';
    createdDate: Date = new Date;
    testType = TestTypeEnum;
    amountTestQuestions: number = 0;
    testResultList: TestResultDto[] =[];
}

export class CreateTestCRq{
    name: string = '';
    description: string = '';
    imgPath: string = '';
    testType = TestTypeEnum;
}

export class TestQuestionDto{
    id: string = '';
    name: string = '';
    createdDate: Date = new Date;
    testId: string = '';
    answers: TestQuestionAnswerDto[] = [];
    amountCorrectAnswers: number = 0;
    selectedAnswer?: TestQuestionAnswerDto;
    customAnswer: string = '';
    idForView: number = 0;
    customAnswerScore?: number = 0;
    customAnswerStatus?: AnswerResultStatusEnum | any;
    customAnswerId?: string;
}

export class TestQuestionAnswerDto{
    id: string = '';
    name: string = '';
    isCorrect: boolean = false;
    testQuestionId: string = '';
    idForView: number = 0;
    score: number = 0;
}
export class UpdateTestQuestionCRq{
    id: string = '';
    name: string = '';
    answers: TestQuestionAnswerDto[] = [];
}

export class TestForTestingDto{
    test:TestDto = new TestDto();
    testQuestionList: TestQuestionDto[] = [];
}

export enum TestTypeEnum{
    SimpleTest = 0,
    FirstLevelDifficultTest = 1,
    SecondLevelDifficultTest = 2
}

export class CheckTestingCRq{
    testForTesting:TestForTestingDto = new TestForTestingDto();
    userId: string = '';
    isChecked?: boolean;
    userTestingHistoryId?: string;
}

export class TestResultDto{
    id: string = '';
    name: string = '';
    rangeFrom: number = 0.0;
    rangeTo: number = 0.0;
    status = TestResultStatusEnum;
    testId: string = '';
    idForView: number = 0;
}

export enum TestResultStatusEnum{
    Low = 0,
    Satisfactory = 1,
    Good = 2,
    Excelent = 3,
    Unknown = 4
}