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

export class ChangePasswordDto{
    userId?: string | null;
    newPassword: string = '';
    IsOwner: boolean = false;
}

export class BlockAndUnBlockUserResponseDto{
    isBlocked: boolean = false;
    result: boolean = false;
}

export class ForgotPasswordDto{
    email: string = '';
    clientURI: string = '';
}

export class ResetPasswordDto{
    password: string = '';
    confirmPassword: string = '';
    email: string = '';
    token: string = '';
}