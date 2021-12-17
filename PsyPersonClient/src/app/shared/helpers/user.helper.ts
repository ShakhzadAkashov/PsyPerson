import { Injectable } from "@angular/core";

@Injectable()

export class UserHelper {
    constructor() { }

    static getCurrentUserId() {
        var payLoad = JSON.parse(window.atob(localStorage.getItem('token')!.split('.')[1]));
        var userId = payLoad.UserID;
        return userId;
    }
}