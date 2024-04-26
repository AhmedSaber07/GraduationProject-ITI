import { UserService } from './../../../Services/user.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,  ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import {Router, RouterLink, RouterModule } from '@angular/router';
import { IUserRegister } from '../../../models/i-user-register';
import { TranslateModule } from '@ngx-translate/core';
import { DataSharingService } from '../../../../Shared/Services/data-sharing.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ CommonModule ,ReactiveFormsModule,RouterLink,RouterModule,TranslateModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  userRegister!:IUserRegister;
  title: string = "انشاء حساب ";
  faceImg = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTWmwYccpGN_2SFl5JJMSaCdmCGmLyaKEwEVw&usqp=CAU';
  googleImg = 'data:image/png;base64,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';
  userForm!: FormGroup;

constructor(private router:Router,private fb: FormBuilder, private _userService: UserService) {
  this.userForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.minLength(3)]],
    lastName: ['', [Validators.required, Validators.minLength(3)]],
    email: ['', [Validators.required, Validators.email,Validators.pattern(this.emailPattern)]],
    phone: ['', [Validators.required, Validators.pattern(this.phonePattern)]],
    password: ['', [Validators.required, Validators.minLength(8),Validators.pattern(this.passwordPattern)]],
    confirmPassword: ['', [Validators.required]],
  }, {
    validator: this.passwordMatchValidator
  });
}
  ngOnInit(): void {
  }

   emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;
   phonePattern = /^01[0125][0-9]{8}$/;
   passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
  get firstName() { return this.userForm.get('firstName'); };
  get lastName() { return this.userForm.get('lastName');}
  get email() { return this.userForm.get('email');}
  get phone() { return this.userForm.get('phone');}
  get password() { return this.userForm.get('password');}
  get confirmPassword() { return this.userForm.get('confirmPassword');}

 
  passwordMatchValidator(group: FormGroup) {
    const passwordControl = group.get('password');
    const confirmPasswordControl = group.get('confirmPassword');

    if (!passwordControl || !confirmPasswordControl) {
      return null;
    }

    if (passwordControl.value !== confirmPasswordControl.value) {
      confirmPasswordControl.setErrors({ mismatch: true });
    } else {
      confirmPasswordControl.setErrors(null);
    }
    return true;
  }



  onSubmit() {
    
    if (this.userForm.valid) {
      this.userRegister= {
        firstName:(this.userForm.get('firstName')?.value.toString()),
        lastName:(this.userForm.get('lastName')?.value.toString()),
        email:(this.userForm.get('email')?.value.toString()),
        phoneNumber:(this.userForm.get('phone')?.value.toString()),
        password:(this.userForm.get('password')?.value.toString())
      };
      this._userService.register(this.userRegister,"user").subscribe(
      (data)=>{
       // console.log(data);
      },
      (error)=>{
       // console.log(error);
         if(error.status === 200){
         // console.log(error.status);
          Swal.fire({
            title: "Account Created Successfully",
            text: "please check your email to confirm account",
            icon: "success"
          });
         
          this.router.navigate(['/login']); 
        }
        else if(error.status === 500){
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "This Email is Already Exist"
          });
        }
        else if(error.status === 501){
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "This Phone Number is Already Exist"
          });
        }
        else {
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Unknown Error"
          });
        }
      }
      );

    } else {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: 'يرجى ملء جميع الحقول بشكل صحيح.'
      });
    }
  }
}
