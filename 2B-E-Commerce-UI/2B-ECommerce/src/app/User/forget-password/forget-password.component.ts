import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { DataSharingService } from '../../Shared/Services/data-sharing.service';
import { UserService } from '../Services/user.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-forget-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule , RouterModule,RouterLink,TranslateModule],
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css'
})
export class ForgetPasswordComponent implements OnInit {
  direction: string = 'rtl';
  forgetPasswordForm!: FormGroup;
  constructor(private formBuilder: FormBuilder,private _dataSharingService: DataSharingService,private _userServices:UserService,private router:Router){

    this.forgetPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required]],
    });
  }
  ngOnInit(): void {
    this._dataSharingService.languageChanged.subscribe(lang => {
      this.direction = lang === 'ar' ? 'rtl' : 'ltr';
    });
  }

  get email() {
    return this.forgetPasswordForm.get('email');
  }


onSubmit(){
  if (this.forgetPasswordForm.valid) {
    this._userServices.forgetPassword(this.email?.value).subscribe(
      (data) => {
        console.log('check your email');
      },
      (err)=>{
        if(err.status===200)
        {
          console.log('check your email');
        }
        else{
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "This Email is Not Found"
        });
      }
      }
    );
  }
  }
}
