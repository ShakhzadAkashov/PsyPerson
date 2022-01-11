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