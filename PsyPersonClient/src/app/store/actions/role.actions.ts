import { Action, createAction, props } from "@ngrx/store";
import { PagedRequest, PagedResponse } from "src/app/models/base";
import { RoleDto, RolePermissionsDto } from "src/app/models/roles.models";

export enum ERoleActions{
    GetRoles = '[Role] Get Roles',
    GetRolesSuccess = '[Role] Get Roles Success',
    GetRole = '[Role] Get Role',
    GetRoleSuccess = '[Role] Get Role Success',
    GetRolePermissions = '[Role] Get Role Permissions',
    GetRolePermissionsSuccess = '[Role] Get Role Permissions Success',
}

export class GetRoles implements Action{
    public readonly type = ERoleActions.GetRoles;
    constructor(public payload: PagedRequest){}
}

export class GetRolesSuccess implements Action{
    public readonly type = ERoleActions.GetRolesSuccess;
    constructor(public payload: PagedResponse<RoleDto>){}
}

export class GetRole implements Action{
    public readonly type = ERoleActions.GetRole;
    constructor(public payload: string){}
}

export class GetRoleSuccess implements Action{
    public readonly type = ERoleActions.GetRoleSuccess;
    constructor(public payload: RoleDto){}
}

export class GetRolePermissions implements Action{
    public readonly type = ERoleActions.GetRolePermissions;
    constructor(public payload: string){}
}

export class GetRolePermissionsSuccess implements Action{
    public readonly type = ERoleActions.GetRolePermissionsSuccess;
    constructor(public payload: RolePermissionsDto){}
}

export type RoleActions = GetRoles | GetRolesSuccess | GetRole | GetRoleSuccess
                        | GetRolePermissions | GetRolePermissionsSuccess;