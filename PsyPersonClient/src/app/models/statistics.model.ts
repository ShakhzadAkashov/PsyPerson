import { TestResultStatusEnum } from "./tests.models";

export class DatasetDto{
    label: string = '';
    fill: boolean = false;
    borderColor: string = '';
    yAxisID: string = '';
    tension: number = .4;
    data: number[] = [];
}

export class ChartDto{
    labels: string[] = [];
    datasets: DatasetDto[] = [];
}

export class StatisticDto{
    name: string = '';
    nameCode: string = '';
    data: ChartDto = new ChartDto();
}

export class UserTestingHistDto{
    testName: string = '';
    testedDate: Date = new Date;
    testScore: number = 0.0;
    resultStatus: string='';
}

export class UserTestingHistDescriptionsDto{
    testName: string = '';
    userTestId: string = '';
    historyDesriptionList: UserTestingHistDto[] = [];
}

export class UserTestingReportDto{
    statistic: StatisticDto = new StatisticDto();
    testingDescriptions: UserTestingHistDescriptionsDto[] = [];
}