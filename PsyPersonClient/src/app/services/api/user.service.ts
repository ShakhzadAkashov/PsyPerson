import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PagedRequest, PagedResponse } from 'src/app/models/base';
import { RoleDto } from 'src/app/models/roles.models';
import { AssignRoleToUserCommand, BlockAndUnBlockUserResponseDto, ChangePasswordDto, UserDto } from 'src/app/models/users.models';
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

  create(user:UserDto){
    return this.http.post(this.BaseURI + '/Users/CreateUser',user);
  }

  update(user:UserDto){
    return this.http.put(this.BaseURI + '/Users/UpdateUser',user);
  }

  getUserRoles(request: PagedRequest | any){
    return this.http.get<PagedResponse<RoleDto>>(this.BaseURI + '/Users/GetUserRoles',{params: request});
  }

  assingRoleToUser(command: AssignRoleToUserCommand){
    return this.http.post(this.BaseURI + '/Users/AssingRole', command);
  }

  removeUser(id:any){
    return this.http.delete(this.BaseURI + '/Users/Remove',{body: {userId:id}});
  }

  removeRoleFromUser(userId:any, roleId:any){
    return this.http.delete(this.BaseURI + '/Users/RemoveRoleFromUser',{body: {userId:userId, roleId: roleId}});
  }

  currentUserProfile(){
    return this.http.get<UserDto>(this.BaseURI + '/ApplicationUser/CurrentUserProfile');
  }

  changePassword(command:ChangePasswordDto){
    return this.http.post(this.BaseURI + '/Users/ChangePassword',command);
  }

  register(formData: any){
    return this.http.post(this.BaseURI + '/ApplicationUser/Register', formData);
  }

  blockAndUnBlockUser(userId: string){
    return this.http.post<BlockAndUnBlockUserResponseDto>(this.BaseURI + '/Users/BlockAndUnBlockUser', { userId:userId });
  }
}
