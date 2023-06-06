import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UsersService } from '../../services/users/users.service';
import { UserLogin } from '../../data/interfaces/userlogin';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  public login: FormGroup;
  constructor(private authService: AuthService, private router: Router) { }
  public user: UserLogin = {
    username: '',
    password: ''
  };
  userResponse: any={
    displayName:'',
    email:'',
    id: '',
    role: '',
    isBanned: false,
    token: '',
username: ''
  }
  ngOnInit(): void {


    this.login = new FormGroup({
      username: new FormControl('', [
        Validators.required, Validators.minLength(4)]),
      password: new FormControl('', Validators.required),
    });
  }
  get username() { return this.login.get('username') as FormControl };
  get password() { return this.login.get('password') as FormControl };

  onSubmit() {
    if (this.login.valid) {
      this.authService.loginUser(this.user).subscribe({
        next: user => {
          console.log(user);
          this.userResponse = user;
          //put token in cookie
          localStorage.setItem('token', this.userResponse.token);
          //get token from cookie
          localStorage.setItem('banned', this.userResponse.isBanned);
          
          //navigate to home page
          this.router.navigate(['']);

        },
        error: err => {
          console.log(err),
          window.alert("Invalid username or password")
        },
      });

    }
  }
}
