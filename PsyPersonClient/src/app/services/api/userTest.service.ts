import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { UserTestUserDto } from "src/app/models/userTests.model";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
  })
  export class UserTestService {
  
    private readonly BaseURI = environment.URI.BaseURI;
    
    constructor(private http:HttpClient) { }

    getUserTestUsers(request: PagedRequest | any){
        return this.http.get<PagedResponse<UserTestUserDto>>(this.BaseURI + '/UserTests/Users',{params: request});
    }
}