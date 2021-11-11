export interface UserDto{
    id: string;
    userName: string;
    email: string;
    phoneNumber: string;
    firstName: string;
    lastName: string;
    patronymic: string;
    password: string;
    isBlocked: boolean;
    role: string;
    dateBirthday: Date;
}