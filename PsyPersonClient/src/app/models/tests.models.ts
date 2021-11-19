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