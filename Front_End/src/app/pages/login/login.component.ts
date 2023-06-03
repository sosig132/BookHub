import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UsersService } from '../../services/users.service';
import { User, UserLogin } from '../../models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  public login: FormGroup;
  constructor(private usersService: UsersService) { }
  public user: UserLogin = {
    username: '',
    password: ''
  };

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
      this.usersService.loginUser(this.user).subscribe({
        next: user => {
          console.log(user);

        },
        error: err => {
          console.log(err),
          window.alert("Invalid username or password")
        },
      });

    }
  }
}
