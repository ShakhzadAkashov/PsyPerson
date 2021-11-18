import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { TestDto } from "src/app/models/tests.model";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
  })
  export class TestService {
  
    private readonly BaseURI = environment.URI.BaseURI;
    
    constructor(private http:HttpClient) { }
  
    getAll(request: PagedRequest | any){
      return this.http.get<PagedResponse<TestDto>>(this.BaseURI + '/Tests/GetTests',{params: request});
    }
  }