import { TestDto, TestQuestionAnswerDto, TestResultStatusEnum } from "./tests.models";
import { UserDto } from "./users.models";
import { PagedResponse } from "./base";

export class UserTestingHistoryDto{
    id: string = '';
    testScore: number = 0;
    resultStatus = TestResultStatusEnum;
    testedDate: Date = new Date();
    userTestId: string = '';
    userTest: UserTestDto = new UserTestDto();
    isChecked?: boolean;
}

export class UserTestDto{
    id: string = '';
    isActive: boolean = false;
    isTested: boolean = false;
    assignedDate: Date = new Date();
    userId: string = '';
    testId: string = '';
    test: TestDto = new TestDto();
    user: UserDto = new UserDto();
    userTestingHistoryList: UserTestingHistoryDto[] = [];
    lastUserTestingHistoryDto: UserTestingHistoryDto = new UserTestingHistoryDto();
}

export class UserTestUserDto extends UserDto{
    status: string = '';
    amountAllUserTests: number = 0;
    amountTestedUserTests: number = 0;
    amountPendingUserTests: number = 0;
}

export class CheckTestingResponseDto{
    testScore: number = 0.0;
    status = TestResultStatusEnum;
    description: string = '';
}

export class UserTestDetailDto extends UserTestDto{
    status: string = '';
}

// export class TestingHistoryQuestionAnswerDto extends TestQuestionAnswerDto{
//     isMarked: boolean = false;
// }

export class TestingHistoryQuestionAnswerDto {
    id: string = '';
    name: string = '';
    isCorrect: boolean = false;
    testQuestionId: string = '';
    idForView: number = 0;
    score: number = 0;
    isMarked: boolean = false;
}

export class TestingHistoryQuestionDto{
    id: string = '';
    name: string = '';
    answers: TestingHistoryQuestionAnswerDto[] = [];
    customAnswer: TestingHistoryCustomQuestionAnswerDto = new TestingHistoryCustomQuestionAnswerDto();
}

export class TestingHistoryDto implements PagedResponse<TestingHistoryQuestionDto>{
    data: TestingHistoryQuestionDto[] = [];
    total = 0;
    loading = false;
    testName: string = '';
    testScore: number = 0;
    testId: string = '';
}

export class TestingHistoryCustomQuestionAnswerDto{
    id: string = '';
    name: string = '';
    answerScore: number = 0.0;
    answerStatus = AnswerResultStatusEnum;
    userTestingHistoryId: string = '';
    testQuestionId: string = '';
}

export enum AnswerResultStatusEnum{
    Low = 25,
    Satisfactory = 50,
    Good = 75,
    Excelent = 100,
    Unknown = 0,
}