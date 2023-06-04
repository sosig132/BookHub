import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { UsersService } from '../../services/users/users.service';
import { User } from '../../data/interfaces/user';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';

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

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    

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
      this.authService.postUser(this.user).subscribe({
        next: user => {
          console.log(user);
          this.router.navigate(['/login']);
        },
        error: err => {
          if (err["error"] == 'Username already exists') window.alert("Username already exists");
          if (err["error"] == 'Email already exists') window.alert("Email already exists");
        }
      });


    
    }
  }
}
