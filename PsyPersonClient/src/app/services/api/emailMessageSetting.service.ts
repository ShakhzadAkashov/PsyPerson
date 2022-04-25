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
        return this.http.get<EmailMessageSettingDto>(this.BaseURI + '/EmailMessageSettings/GetSetting');
    }

    create(setting: EmailMessageSettingDto) {
        return this.http.post(this.BaseURI + '/EmailMessageSetting/CreateSetting', setting);
    }

    update() {
        return this.http.put(this.BaseURI + '/EmailMessageSetting/UpdateSetting', {});
    }

    remove(id:any){
        return this.http.delete(this.BaseURI + '/EmailMessageSetting/RemoveSetting', { body: { id: id } });
    }
}