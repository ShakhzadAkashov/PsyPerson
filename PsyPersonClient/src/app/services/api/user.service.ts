import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

}
