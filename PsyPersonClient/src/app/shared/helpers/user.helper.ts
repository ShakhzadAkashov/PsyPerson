import { Injectable } from "@angular/core";

@Injectable()

export class UserHelper {
    constructor() { }

    static getCurrentUserId() {
        var payLoad = JSON.parse(window.atob(localStorage.getItem('token')!.split('.')[1]));
        var userId = payLoad.UserID;
        return userId;
    }

    static getUserRoles(){
        var payLoad = JSON.parse(window.atob(localStorage.getItem('token')!.split('.')[1]));
        var userRoles = payLoad.role;
        return userRoles;
    }

    static getUserPermissions(){
        var payLoad = JSON.parse(window.atob(localStorage.getItem('token')!.split('.')[1]));
        var permissions = payLoad.Permission;
        return permissions;
    }

    static UserHasPermission(permissionName: string): boolean{
        var payLoad = JSON.parse(window.atob(localStorage.getItem('token')!.split('.')[1]));
        var permissions = payLoad.Permission;

        for (let i = 0; i < permissions.length; i++) {
            if(permissions[i] === permissionName)
                return true;
        }

        return false;
    }
}