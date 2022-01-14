import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserHelper } from 'src/app/shared/helpers/user.helper';
import { UserService } from '../api/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private service: UserService){}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (localStorage.getItem('token') != null) {
      let permission = next.data['permission'] as string;
      if (permission) {
        let userPersmissions = UserHelper.getUserPermissions();
        if (userPersmissions) {
          for(var i = 0; i< userPersmissions.length; i++)
          {
            if (userPersmissions[i] === permission) {
              return true;
            }
          }

          this.router.navigate(['/forbidden']);
          return false;
        }
        else {
          this.router.navigate(['/forbidden']);
          return false;
        }
      }

      return true;
    }
    else {
      this.router.navigate(['/users/user/login']);
      return false;
    }
  }
}
