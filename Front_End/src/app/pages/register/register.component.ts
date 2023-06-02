import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  public register: FormGroup;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {

    this.register = new FormGroup({
      username: new FormControl('', [
        Validators.required, Validators.minLength(4)]),
      password: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.email, Validators.required]),
      display_name: new FormControl('', Validators.required),
    });
  }
  get username() { return this.register.get('username') as FormControl };
  get password() { return this.register.get('password') as FormControl };
  get email() { return this.register.get('email') as FormControl };
  get display_name() {return this.register.get('display_name') as FormControl }

  Register() {
    
    if (this.register.valid) {
      const registrationData = this.register.value;
      
      /*this.http.post('/api', registrationData).subscribe((response) => {
        console.log('Registration successful', response);
        this.register.reset();
      },
        (error) => { console.error('Registration failed', error); });*/



    }
  }
}
