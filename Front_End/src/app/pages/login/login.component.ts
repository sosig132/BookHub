import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  public login: FormGroup;


  ngOnInit(): void {
    this.login = new FormGroup({
      username: new FormControl('', [
        Validators.required, Validators.minLength(4)]),
      password: new FormControl('', Validators.required),
    });
  }
  get username() { return this.login.get('username') as FormControl };
  get password() { return this.login.get('password') as FormControl };

  onSubmit() { }
}
