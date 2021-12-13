import { UserDto } from "./users.models";

export class UserTestingHistoryDto{
    id: string = '';
    testScore: number = 0;
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
    userTestingHistoryList: UserTestingHistoryDto[] = [];
}

export class UserTestUserDto extends UserDto{
    userTestList: UserTestDto[] = [];
    status: string = '';
}