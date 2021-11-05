import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-regitration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.email, Validators.required]],
    //FullName: [''],
    // Role: ['',Validators.required],
    Passwords: this.fb.group({
      Password:['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword:['', Validators.required]
    },{validators: this.comparePasswords})
  });

  comparePasswords(fb:FormGroup){
    let confirmPswdCtrl = fb.get('ConfirmPassword');
    if(confirmPswdCtrl?.errors == null || 'passwordMismatch' in confirmPswdCtrl.errors){
      if(fb.get("Password")?.value != confirmPswdCtrl?.value)
        confirmPswdCtrl?.setErrors({passwordMismatch:true });
      else
        confirmPswdCtrl?.setErrors(null);
    }
  }

  constructor(private fb:FormBuilder, private router: Router,) { }

  ngOnInit(): void {
    if(localStorage.getItem('token') != null)
      this.router.navigateByUrl('/home/statistics');
  }

  onSubmit(){}
}
