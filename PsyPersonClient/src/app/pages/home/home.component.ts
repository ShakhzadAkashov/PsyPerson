import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserHelper } from 'src/app/shared/helpers/user.helper';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  isCollapsed = false;
  UserHelper: any;
  
  constructor(private router: Router) { 
    this.UserHelper = UserHelper;
  }

  ngOnInit(): void {
  }

  onLogout(){
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }
}
