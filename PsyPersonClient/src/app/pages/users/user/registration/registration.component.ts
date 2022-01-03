import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/api/user.service';

@Component({
  selector: 'app-regitration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.email, Validators.required]],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validators: this.comparePasswords })
  });

  comparePasswords(fb: FormGroup) {
    let confirmPswdCtrl = fb.get('ConfirmPassword');
    if (confirmPswdCtrl?.errors == null || 'passwordMismatch' in confirmPswdCtrl.errors) {
      if (fb.get("Password")?.value != confirmPswdCtrl?.value)
        confirmPswdCtrl?.setErrors({ passwordMismatch: true });
      else
        confirmPswdCtrl?.setErrors(null);
    }
  }

  constructor(
    private fb:FormBuilder, 
    private router: Router,
    private service: UserService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    if(localStorage.getItem('token') != null)
      this.router.navigateByUrl('/home/statistics');
  }

  onSubmit(){
    var formData = {
      userName: this.formModel.value.UserName,
      email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password
    };
    this.service.register(formData).toPromise().then(
      (res: any) => {
        if(res.succeeded){
          this.formModel.reset();
          this.toastr.success('New user created!', 'Registration successful.');
        }else{
          res.errors.forEach((element:any) => {
            switch(element.code)
            {
              case 'DublicateUserName':
                this.toastr.error('UserName is already taken','Registration failed.');
                break;
              default:
                this.toastr.error(element.description,'Registration failed.');
                break;
            }
          });
        }
      },
      err => {
        console.log(err.errors)
      }
    );
  }
}
