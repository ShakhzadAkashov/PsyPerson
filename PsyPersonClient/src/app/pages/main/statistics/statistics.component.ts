import { Component, OnInit } from '@angular/core';
import { StatisticDto } from 'src/app/models/statistics.model';
import { StatisticService } from 'src/app/services/api/statistic.service';
import { ChartHelper } from 'src/app/shared/helpers/chart.helper';
import { UserHelper } from 'src/app/shared/helpers/user.helper';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {

  forManagerData_Users: StatisticDto = new StatisticDto();
  forManagerData_Tests: StatisticDto = new StatisticDto();
  forEmployeeData: StatisticDto = new StatisticDto();
  multiAxisOptions: any;
  userHelper: any;

  constructor(private service: StatisticService) {
    this.userHelper = UserHelper;
  }

  ngOnInit(): void { 
    this.multiAxisOptions = ChartHelper.getmultiAxisOptions();
    this.getData();
  }

  getData(){
    if(this.userHelper.UserHasPermission("Permissions.Statistics.ForManagers")){
      this.service.getStatisticsForManager().toPromise().then((res: StatisticDto[]) => {
        this.forManagerData_Users = res.filter(x => x.nameCode == "WEEKLY_USERS")[0];
        this.forManagerData_Tests = res.filter(x => x.nameCode == "WEEKLY_TESTS")[0];
      });
    }
    if(this.userHelper.UserHasPermission("Permissions.Statistics.ForEmployees"))
    this.service.getStatisticsForEmployee().toPromise().then((res: StatisticDto) =>{
      this.forEmployeeData = res;
    });
  }
}
