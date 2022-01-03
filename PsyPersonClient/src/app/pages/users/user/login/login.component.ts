import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserDto } from 'src/app/models/users.models';
import { UserService } from 'src/app/services/api/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel = {
    UserName: '',
    Password: ''
  }
  user: UserDto = new UserDto();

  constructor(private service: UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    if(localStorage.getItem('token') != null)
      this.router.navigateByUrl('/home/statistics');
  }

  onSubmit(form:NgForm){
    this.service.login(form.value).toPromise().then(
      (res: any) =>{
        localStorage.setItem('token',res.token);
        this.service.currentUserProfile().toPromise().then(
          (result:UserDto)=>{
          if(result.isBlocked == true){
            localStorage.removeItem('token');
            this.toastr.error('Blocked','User is blocked');
          }else{
            this.user = result;
            this.router.navigateByUrl('home');
            this.toastr.success("success",`Welcome ${this.user.userName}`,{timeOut:3000});
          }
        });
      },
      err =>{
        if(err.status == 400)
          this.toastr.error('Incorrect username or password.','Authentication failed');
        else
          console.log(err.error);
      }
    );
  }

}
