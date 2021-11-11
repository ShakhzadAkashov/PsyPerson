import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedRequest, PagedResponse } from 'src/app/models/base';
import { UserDto } from 'src/app/models/users.models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly BaseURI = environment.URI.BaseURI;
  
  constructor(private http:HttpClient) { }

  login(formData:any){
    return this.http.post(this.BaseURI + '/ApplicationUser/Login',formData);
  }

  getAll(request: PagedRequest | any){
    return this.http.get<PagedResponse<UserDto>>(this.BaseURI + '/Users/GetAll',{params: request});
  }
}
