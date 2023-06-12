import { Injectable } from '@angular/core';
import { Observable, delay, of, tap } from 'rxjs';
import { User } from '../../data/interfaces/user';
import { UserLogin } from '../../data/interfaces/userlogin';
import { ApiService } from '../api/api.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private readonly apiService: ApiService, private readonly jwtHelper: JwtHelperService, private readonly router: Router) { }

  private readonly route = 'User';

  postUser(user: User): Observable<User> {
    user.id = '00000000-0000-0000-0000-000000000000';
    return this.apiService.post(this.route + '/register', user);
  }

  loginUser(user: UserLogin): Observable<User> {
    return this.apiService.post(this.route + '/login', user);
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  public isAdmin(): boolean {
    const token = localStorage.getItem('token') as string;
    const decodedToken = this.jwtHelper.decodeToken(token);
    return decodedToken.role === 'Admin';
  }


  public logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['login']);
  }

}
