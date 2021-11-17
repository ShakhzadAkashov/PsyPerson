import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { RoleDto } from "src/app/models/roles.models";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})

export class RoleService{
    
    private readonly BaseURI = environment.URI.BaseURI;
    
    constructor(private http:HttpClient){}

    getAll(request: PagedRequest | any){
        return this.http.get<PagedResponse<RoleDto>>(this.BaseURI + '/ApplicationRoles/GetAll',{params: request});
    }

    create(role: RoleDto){
        return this.http.post(this.BaseURI + '/ApplicationRoles/CreateRole',role);
    }

    update(role:RoleDto){
        return this.http.put(this.BaseURI + '/ApplicationRoles/UpdateRole',role);
    }

    removeRole(id:any){
        return this.http.delete(this.BaseURI + '/ApplicationRoles/Remove',{body: {roleId:id}});
    }
}