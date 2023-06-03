import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { User, UserLogin } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseApiUrl + '/api/User/byUsername/fasfas');
      
  }
  postUser(user: User): Observable<User> {
    user.id = '00000000-0000-0000-0000-000000000000';
    return this.http.post<User>(this.baseApiUrl + '/api/User/register', user);
  }

  loginUser(user:UserLogin): Observable<User> {
    return this.http.post<User>(this.baseApiUrl + '/api/User/login', user);
  }
}
