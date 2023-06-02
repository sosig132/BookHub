import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  getAllUsers() {
    this.http.get(this.baseApiUrl+ '/api/user');
      
  }
  postUser(user) {
    
  }
}
