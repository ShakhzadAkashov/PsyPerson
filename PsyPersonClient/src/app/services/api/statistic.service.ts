import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StatisticDto } from 'src/app/models/statistics.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StatisticService {

  private readonly BaseURI = environment.URI.BaseURI;
  
  constructor(private http:HttpClient) { }

  getStatisticsForManager(){
    return this.http.get<StatisticDto[]>(this.BaseURI + '/Statistics/StatisticsForManager');
  }

  getStatisticsForEmployee(){
    return this.http.get<StatisticDto>(this.BaseURI + '/Statistics/StatisticsForEmployee');
  }
}
