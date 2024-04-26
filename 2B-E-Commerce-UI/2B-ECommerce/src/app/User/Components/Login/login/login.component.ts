import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { UserService } from '../../../Services/user.service';
import { IUserLogin } from '../../../models/i-user-login';
import { TranslateModule } from '@ngx-translate/core';
import { DataSharingService } from '../../../../Shared/Services/data-sharing.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule , RouterModule,RouterLink,TranslateModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit{
  title = 'تسجيل الدخول';
  image = 'https://picsum.photos/id/237/50/50';
  loginForm: FormGroup;
  userLogin!:IUserLogin;
  constructor(private formBuilder: FormBuilder,private _userServices:UserService,private router:Router) {
    this.loginForm = this.formBuilder.group({
      emailOrNumber: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }
  ngOnInit(): void {
  }

  get emailOrNumber() {
    return this.loginForm.get('emailOrNumber');
  }

  get password() {
    return this.loginForm.get('password');
  }

googleLogin(){
//console.log("ss");
this._userServices.externalLogin().subscribe(res=>{
  //console.log(res);
})

  } 

  onSubmit()  {
    if (this.loginForm.valid) {
      
      this.userLogin={
        userName : this.loginForm.get('emailOrNumber')?.value.toString(),
        password : this.loginForm.get('password')?.value.toString()
      }

      this._userServices.login(this.userLogin).subscribe(
        (data) => {
          //console.log(data);
          if (data) 
          {
            if (localStorage.getItem('redirect')) 
            {
              this.router.navigate([localStorage.getItem('redirect')]);
              localStorage.removeItem('redirect');
            }
            else 
             {
              this.router.navigate(['/Home'])
            }
          }
           else {
            Swal.fire({
              icon: "error",
              title: "Oops...",
              text: "Email Or Password Incorrect",
            });
          }
        },
        (err)=>{
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Email Or Password Incorrect",
          });
        }
      );
      
  }
}
}
