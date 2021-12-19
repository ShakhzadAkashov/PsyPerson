import { TestDto, TestResultStatusEnum } from "./tests.models";
import { UserDto } from "./users.models";

export class UserTestingHistoryDto{
    id: string = '';
    testScore: number = 0;
    resultStatus = TestResultStatusEnum;
    testedDate: Date = new Date();
    userTestId: string = '';
}

export class UserTestDto{
    id: string = '';
    isActive: boolean = false;
    isTested: boolean = false;
    assignedDate: Date = new Date();
    userId: string = '';
    testId: string = '';
    test: TestDto = new TestDto();
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