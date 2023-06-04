import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { User } from '../../data/interfaces/user';
import { UserLogin } from '../../data/interfaces/userlogin';
import { ApiService } from '../api/api.service';
import { JwtHelperService } from '@auth0/angular-jwt'; 



@Injectable({
  providedIn: 'root'
})
export class UsersService {
  constructor(private readonly apiService: ApiService, private readonly jwtHelper: JwtHelperService) { }

  private readonly route = 'User';

  getAllUsers(): Observable<User[]> {
    return this.apiService.get(this.route);
      
  }
  
}
