import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly apiUrl = environment.baseApiUrl + '/api';
  constructor(private readonly httpClient: HttpClient) {
  }
  get<T>(path: string, params = {}): Observable<any> {
    return this.httpClient.get<T>(`${this.apiUrl}/${path}`, { params });
  }

  post<T>(path: string, body = {}): Observable<any> {
    return this.httpClient.post<T>(`${this.apiUrl}/${path}`, body);
  }

  patch<T>(path: string, body = {}): Observable<any> {
    return this.httpClient.patch<T>(`${this.apiUrl}/${path}`, body);
  }

  put<T>(path: string, body = {}): Observable<any> {
    return this.httpClient.put<T>(`${this.apiUrl}/${path}`, body);
  }

  delete<T>(path: string): Observable<any> {
    return this.httpClient.delete<T>(`${this.apiUrl}/${path}`);
  }
}
