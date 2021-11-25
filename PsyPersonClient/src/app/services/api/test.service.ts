import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { CreateTestCRq, TestDto, TestQuestionDto, UpdateTestQuestionCRq } from "src/app/models/tests.models";
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

    create(test: CreateTestCRq) {
        return this.http.post(this.BaseURI + '/Tests/CreateTest', test);
    }

    update(test:TestDto){
        return this.http.put(this.BaseURI + '/Tests/UpdateTest',test);
    }

    getTestQuestions(request: PagedRequest | any){
        return this.http.get<PagedResponse<TestQuestionDto>>(this.BaseURI + '/TestQuestions/GetAll',{params: request});
    }

    createTestQuestion(question: TestQuestionDto){
        return this.http.post(this.BaseURI + '/TestQuestions/Create', question);
    }

    updateTestQuestion(question: UpdateTestQuestionCRq){
        return this.http.put(this.BaseURI + '/TestQuestions/Update',question);
    }
  }