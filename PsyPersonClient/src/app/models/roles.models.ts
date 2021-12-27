export class RoleDto{
    id: string = '';
    name: string = '';
    createdDate: Date = new Date;
    description: string = '';
    normalizedName: string = '';
}

export class RolePermissionsDto{
    roleId: string = '';
    roleName: string = '';
    roleClaims: CheckBoxDto[] = [];
}

export class CheckBoxDto{
    displayValue: string = '';
    isSelected: boolean = false;
}