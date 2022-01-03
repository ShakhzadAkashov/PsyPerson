import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserDto } from 'src/app/models/users.models';
import { UserService } from 'src/app/services/api/user.service';
import { UserHelper } from 'src/app/shared/helpers/user.helper';
import { ChangePasswordModalComponent } from '../users/user/change-password-modal/change-password-modal.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  isCollapsed = false;
  UserHelper: any;
  visibleSidebar: boolean = false;
  user: UserDto = new UserDto();
  @ViewChild('changePasswordModal', { static: true }) changePasswordModal: 
  ChangePasswordModalComponent = new ChangePasswordModalComponent(this.service);
  
  constructor(private router: Router, private service: UserService) { 
    this.UserHelper = UserHelper;
  }

  ngOnInit(): void {
    this.getCurrentUserProfile();
  }

  getCurrentUserProfile(){
    this.service.currentUserProfile().toPromise().then(res => {
      this.user = res;
    },
    err =>{
      console.log(err);
    });
  }

  onLogout(){
    localStorage.removeItem('token');
    this.router.navigate(['/users/user/login']);
  }

  changePassword(){
    this.changePasswordModal.show(true);
  }
}
