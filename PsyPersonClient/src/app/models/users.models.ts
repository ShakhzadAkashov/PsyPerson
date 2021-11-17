export class UserDto{
    id: string = '';
    userName: string = '';
    email: string = '';
    phoneNumber: string = '';
    firstName: string = '';
    lastName: string = '';
    patronymic: string = '';
    password: string = '';
    isBlocked: boolean = false;
    role: string = '';
    dateBirthday: Date = new Date;
}

export class AssignRoleToUserCommand{
    userId: string = '';
    roleId: string = '';
    roleName: string = '';
}