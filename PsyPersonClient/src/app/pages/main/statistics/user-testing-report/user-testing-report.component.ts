import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserTestingReportDto } from 'src/app/models/statistics.model';
import { UserDto } from 'src/app/models/users.models';
import { StatisticService } from 'src/app/services/api/statistic.service';
import { UserService } from 'src/app/services/api/user.service';
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
  user: UserDto = new UserDto();
  donutChart: any;
  status: any;
  emotionStatus: any;

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
    private service: StatisticService,
    private userService: UserService
  ) { 
    this.userId = this.activatedRoute.snapshot.queryParams['userId'];
    this.userName = this.activatedRoute.snapshot.queryParams['userName'];
    this.status = this.activatedRoute.snapshot.queryParams['status'];
    this.from = this.activatedRoute.snapshot.queryParams['from'];
  }

  ngOnInit(): void {
    this.multiAxisOptions = ChartHelper.getmultiAxisOptions();
    this.getData();
    this.getDonutChart();
  }

  getData(){
    this.userService.otherUserProfile(this.userId).toPromise().then((res: UserDto) => {
      this.user = res;
    });
    this.service.getUserTestingReport(this.userId).toPromise().then((res: UserTestingReportDto) =>{
      this.testingReport = res;
    });
  }

  getDonutChart() {
    let negative = 0;
    let positive = 0;

    switch(this.status){
      case "Low":{
        negative = 75;
        positive = 25;
        this.status = "0";
        break;
      }
      case "Satisfactory":{
        negative = 50;
        positive = 50;
        this.status = "1";
        break;
      }
      case "Good":{
        negative = 25;
        positive = 75;
        this.status = "2";
        break;
      }
      case "Excelent":{
        negative = 90;
        positive = 10;
        this.status = "3";
        break;
      }
      case "Unknown":{
        negative = 50;
        positive = 50;
        this.status = "4";
        break;
      }
      default: { 
        negative = 50;
        positive = 50; 
        this.status = "4";
        break; 
     } 
    }

    this.emotionStatus = this.resultStatuses[this.status].label

    this.donutChart = {
      labels: ['Позитив', 'Негатив'],
      datasets: [
        {
          data: [positive, negative],
          backgroundColor: [
            "#36A2EB",
            "#FF6384",
          ],
          hoverBackgroundColor: [
            "#36A2EB",
            "#FF6384",
          ]
        }
      ]
    };
  }

  goBack(){
    const from = '../' + this.from;
    this.router.navigate([from]);
  }
}
