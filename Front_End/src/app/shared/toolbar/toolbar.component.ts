import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Role } from '../../data/enums/role';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {
  constructor(private readonly authService: AuthService, private readonly jwtHelper: JwtHelperService) { }

  role: string;

  ngOnInit(): void {
    console.log(this.jwtHelper.decodeToken())
    this.role = this.jwtHelper.decodeToken().role;

  }

  logout() {

    this.authService.logout();
  }

  ifAdmin() {
    
    return this.role ==='Admin';   
  }
}
