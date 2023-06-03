import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { User } from '../../models/user.model';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  public register: FormGroup;

  users: User[] = [];
  user: User = {
    id: '',
    username: '',
    password: '',
    displayName: '',
    email: '',
    role: 1,
    created_at: '',
    updated_at: '',
    is_banned: false,
  };

  constructor(private usersService: UsersService) { }

  ngOnInit(): void {

    this.usersService.getAllUsers().subscribe({
      next: users => {
        console.log(users);
      },
      error: err => console.log(err),
    });

    

    this.register = new FormGroup({
      username: new FormControl('', [
        Validators.required, Validators.minLength(4)]),
      password: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.email, Validators.required]),
      displayName: new FormControl('', Validators.required),
    });
  }
  get username() { return this.register.get('username') as FormControl };
  get password() { return this.register.get('password') as FormControl };
  get email() { return this.register.get('email') as FormControl };
  get displayName() {return this.register.get('displayName') as FormControl }

  Register() {
    console.log(this.user);
    
    if (this.register.valid) {
      this.usersService.postUser(this.user).subscribe({
        next: user => {
          console.log(user);
        },
        error: err => console.log(err),
      });


    window.location.href='/login';
    }
  }
}
