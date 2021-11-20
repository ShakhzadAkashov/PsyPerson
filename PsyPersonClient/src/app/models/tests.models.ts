export class TestDto{
    id: string = '';
    name: string = '';
    description: string = '';
    imgPath: string = '';
    createdDate: Date = new Date;
}

export class CreateTestCRq{
    name: string = '';
    description: string = '';
    imgPath: string = '';
}

export class TestQuestionDto{
    id: string = '';
    name: string = '';
    questionType = TestQuestionEnum;
    createdDate: Date = new Date;
    testId: string = '';
    answers: TestQuestionAnswerDto[] = [];
    amountCorrectAnswers: number = 0;
}

export class TestQuestionAnswerDto{
    id: string = '';
    name: string = '';
    isCorrect: boolean = false;
    testQuestionId: string = '';
}

export enum TestQuestionEnum{
    SimpleQuestion = 0
}