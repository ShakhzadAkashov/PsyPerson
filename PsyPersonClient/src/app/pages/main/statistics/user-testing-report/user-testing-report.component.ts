import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserTestingReportDto } from 'src/app/models/statistics.model';
import { StatisticService } from 'src/app/services/api/statistic.service';
import { ChartHelper } from 'src/app/shared/helpers/chart.helper';

@Component({
  selector: 'app-user-testing-report',
  templateUrl: './user-testing-report.component.html',
  styleUrls: ['./user-testing-report.component.css']
})
export class UserTestingReportComponent implements OnInit {

  userId = '';
  userName = '';
  from = '';
  multiAxisOptions: any;
  testingReport: UserTestingReportDto = new UserTestingReportDto();

  resultStatuses :{ [key: string]: any } = {
    "0": {
      label: 'Низкое'
    },
    "4": {
      label: 'Неизвестно'
    },
    "3": {
      label: 'Отличное'
    },
    "1": {
      label: 'Среднее'
    },
    "2": {
      label: 'Xорошee'
    }
  }

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private service: StatisticService
  ) { 
    this.userId = this.activatedRoute.snapshot.queryParams['userId'];
    this.userName = this.activatedRoute.snapshot.queryParams['userName'];
    this.from = this.activatedRoute.snapshot.queryParams['from'];
  }

  ngOnInit(): void {
    this.multiAxisOptions = ChartHelper.getmultiAxisOptions();
    this.getData();
  }

  getData(){
    this.service.getUserTestingReport(this.userId).toPromise().then((res: UserTestingReportDto) =>{
      this.testingReport = res;
    });
  }

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }
}
