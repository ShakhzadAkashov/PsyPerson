import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { SuggestionDto } from "src/app/models/suggestion.model";
import { TestResultStatusEnum } from "src/app/models/tests.models";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})

export class SuggestionService {

    private readonly BaseURI = environment.URI.BaseURI;

    constructor(private http: HttpClient) { }

    getAll(request: PagedRequest | any) {
        return this.http.get<PagedResponse<SuggestionDto>>(this.BaseURI + '/Suggestions/GetAll', { params: request });
    }

    create(suggestion: SuggestionDto) {
        return this.http.post(this.BaseURI + '/Suggestions/Create', suggestion);
    }

    update(suggestion: SuggestionDto) {
        return this.http.put(this.BaseURI + '/Suggestions/Update', suggestion);
    }

    getById(id: string){
        return this.http.get<SuggestionDto>(this.BaseURI + '/Suggestions/GetById',{params: {id: id}});
    }

    getByStatus(request: PagedRequest | any){
        return this.http.get<PagedResponse<SuggestionDto>>(this.BaseURI + '/Suggestions/GetByStatus',{params: request});
    }

    remove(id:any){
        return this.http.delete(this.BaseURI + '/Suggestions/Remove', { body: { id: id } });
    }
}
