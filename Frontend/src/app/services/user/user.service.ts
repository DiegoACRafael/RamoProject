import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { AuthRequest } from 'src/app/Model/Interfaces/user/auth/AuthRequest';
import { AuthResponse } from 'src/app/Model/Interfaces/user/auth/AuthResponse';
import { SignupUserRequest } from 'src/app/Model/Interfaces/user/SignupUserRequest';
import { SignupUserResponse } from 'src/app/Model/Interfaces/user/SignupUserResponse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private API_URL = environment.API_URL;

  constructor(private http: HttpClient, private cookie: CookieService) { }

  signupUser(requestDatas: SignupUserRequest): Observable<SignupUserResponse> {
    return this.http.post<SignupUserResponse>(`${this.API_URL}/api/Auth/v1/register`, requestDatas);
  }

  authUser(requestDatas: AuthRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.API_URL}/api/Auth/v1/login`, requestDatas);
  }

  isloggedIn(): boolean{
    const JWT_TOKEN = this.cookie.get('USER_INFO');
    return JWT_TOKEN ? true : false;
  }
}
