import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { CheckTestingCRq, CreateTestCRq, TestDto, TestForTestingDto, TestQuestionDto, UpdateTestQuestionCRq } from "src/app/models/tests.models";
import { CheckTestingResponseDto } from "src/app/models/userTests.model";
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

    createTestQuestions(question: TestQuestionDto[]){
        return this.http.post(this.BaseURI + '/TestQuestions/CreateQuestions', { testQuestions: question });
    }

    updateTestQuestion(question: UpdateTestQuestionCRq){
        return this.http.put(this.BaseURI + '/TestQuestions/Update',question);
    }

    uploadTestQuestionsFromFile(formData: FormData){
        return this.http.post(this.BaseURI + '/TestQuestions/UploadFromFile', formData);
    }

    getTestForTesting(testId: string | any){
        return this.http.get<TestForTestingDto>(this.BaseURI + '/Testings/GetTestForTesting',{params: {testId: testId}});
    }

    checkSimpeTypeTesting(command:CheckTestingCRq){
        return this.http.post<CheckTestingResponseDto>(this.BaseURI + '/Testings/CheckSimpleTypeTest',command);
    }

    TestsForLookupTable(request: PagedRequest | any){
        return this.http.get<PagedResponse<TestDto>>(this.BaseURI + '/Tests/TestsForLookupTable',{params: request});
    }

    getById(id: string){
        return this.http.get<TestQuestionDto>(this.BaseURI + '/TestQuestions/GetById',{params: {id: id}});
    }
    
    checkFirstLevelDifficultTypeTesting(command:CheckTestingCRq){
        return this.http.post<CheckTestingResponseDto>(this.BaseURI + '/Testings/CheckFirstLevelDifficultTypeTesting',command);
    }

    checkSecondLevelDifficultTypeTesting(command:CheckTestingCRq){
        return this.http.post<CheckTestingResponseDto>(this.BaseURI + '/Testings/CheckSecondLevelDifficultTypeTesting',command);
    }
  }