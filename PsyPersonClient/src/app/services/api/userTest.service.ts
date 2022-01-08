import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { TestingHistoryDto, UserTestDetailDto, UserTestDto, UserTestingHistoryDto, UserTestUserDto } from "src/app/models/userTests.model";
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

    getUserTests(request: PagedRequest | any){
        return this.http.get<PagedResponse<UserTestDto>>(this.BaseURI + '/UserTests/UserTests',{params: request});
    }

    create(userId: string, testId: string) {
        return this.http.post(this.BaseURI + '/UserTests/CreateUserTest', {userId: userId, testId: testId});
    }

    getUserTestsDetails(request: PagedRequest | any){
        return this.http.get<PagedResponse<UserTestDetailDto>>(this.BaseURI + '/UserTests/UserTestsDetails',{params: request});
    }

    getTestingHistory(request: PagedRequest | any){
        return this.http.get<TestingHistoryDto>(this.BaseURI + '/Testings/GetTestingHistory',{params: request});
    }

    getUserTestingsForCheck(request: PagedRequest | any){
        return this.http.get<PagedResponse<UserTestingHistoryDto>>(this.BaseURI + '/Testings/GetUserTestingForCheck',{params: request});
    }

    reAssignTest(userTestId: string) {
        return this.http.post(this.BaseURI + '/UserTests/ReAssignTest', {userTestId: userTestId});
    }

    cancelTest(userTestId: string) {
        return this.http.post(this.BaseURI + '/UserTests/CancelTest', {userTestId: userTestId});
    }
}