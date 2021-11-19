import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})
export class AppFilesService {

    private readonly BaseURI = environment.URI.BaseURI;

    constructor(private http: HttpClient) { }

    uploadFile(formData: FormData){
        return this.http.post(this.BaseURI + '/AppFiles/Upload', formData, {reportProgress: true, observe: 'events'});
    }

    getPhoto(filePath: string) : any {
        return this.BaseURI + '/AppFiles/GetPhoto?filePath='+filePath;
    }
}