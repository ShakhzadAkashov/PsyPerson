import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EmailMessageSettingDto } from "src/app/models/emailMessageSettings.models";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})
export class EmailMessageSettingService {

    private readonly BaseURI = environment.URI.BaseURI;

    constructor(private http: HttpClient) { }

    get() {
        return this.http.get<EmailMessageSettingDto>(this.BaseURI + '/EmailMessages/GetSetting');
    }

    create(setting: EmailMessageSettingDto) {
        return this.http.post(this.BaseURI + '/EmailMessages/CreateSetting', setting);
    }

    update(setting: EmailMessageSettingDto) {
        return this.http.put(this.BaseURI + '/EmailMessages/UpdateSetting', setting);
    }

    remove(id:any){
        return this.http.delete(this.BaseURI + '/EmailMessages/RemoveSetting', { body: { id: id } });
    }
}