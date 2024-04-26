import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink, RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { DataSharingService } from '../../../Shared/Services/data-sharing.service';
import { UserService } from '../../Services/user.service';
import Swal from 'sweetalert2';
import { IResetPassword } from '../../models/i-reset-password';
@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule , RouterModule,RouterLink,TranslateModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent implements OnInit{
  resetPasswordForm!: FormGroup;
  resetPasswordModel!:IResetPassword;
  passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
  email:string="";
  code:string="";
  constructor( private route: ActivatedRoute,private router:Router,private formBuilder: FormBuilder,private _userService: UserService){
  this.resetPasswordForm = this.formBuilder.group({
    newPassword: ['', [Validators.required, Validators.minLength(8),Validators.pattern(this.passwordPattern)]],
    conNewPassword: ['', [Validators.required]],
  }, {
    validator: this.passwordMatchValidator
  });
}
  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {
      this.email = params['email'],
      this.code = params['code']
    });
  }

  get newPassword() {
    return this.resetPasswordForm.get('newPassword');
  }

  get conNewPassword() {
    return this.resetPasswordForm.get('conNewPassword');
  }

  passwordMatchValidator(group: FormGroup) {
    const passwordControl = group.get('newPassword');
    const confirmPasswordControl = group.get('conNewPassword');

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


  onSubmit(){
    if (this.resetPasswordForm.valid) {
      this.resetPasswordModel= {
        email:this.email,
        newPassword:(this.resetPasswordForm.get('newPassword')?.value.toString())
      }
      //console.log(this.resetPasswordModel)
      this._userService.resetPassword(this.resetPasswordModel).subscribe(
      (data)=>{
        //console.log(data);
        Swal.fire({
          text: "Password Change Successfully",
          icon: "success"
        });
        this.router.navigate(['/login']); 
      },
      (error)=>{
       // console.log(error);
         if(error.status === 200){
          
          Swal.fire({
            text: "Password Change Successfully",
            icon: "success"
          });
         
          this.router.navigate(['/login']); 
        }
        else{
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: error.statusText
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
