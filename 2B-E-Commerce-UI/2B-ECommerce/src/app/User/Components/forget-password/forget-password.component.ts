

import { CommonModule } from '@angular/common';
import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import Swal from 'sweetalert2';
import { DataSharingService } from '../../../Shared/Services/data-sharing.service';
import { UserService } from '../../Services/user.service';
@Component({
  selector: 'app-forget-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,FormsModule , RouterModule,RouterLink,TranslateModule],
  templateUrl: './forget-password.component.html',
  styleUrl: './forget-password.component.css'
})
export class ForgetPasswordComponent implements OnInit {
  
  // @ViewChild('codeModal') codeModal!: ElementRef;
  forgetPasswordForm!: FormGroup;
  code: string = '';
  constructor(private formBuilder: FormBuilder,private _userServices:UserService,private router:Router){

    this.forgetPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required]],
    });
  }
  ngOnInit(): void {
  }

  get email() {
    return this.forgetPasswordForm.get('email');
  }


onSubmit(){
  if (this.forgetPasswordForm.valid) {
    this._userServices.sendCode(this.email?.value).subscribe(
      (data) => {
        Swal.fire({
          title: "Check Your Email",
          text: "we send verification code to your email",
          icon: "info"
        });
        this.router.navigate(['/CodeConfirm',this.email?.value])
      },
      (err)=>{
        Swal.fire({
          title: "Check Your Email",
          text: "we send verification code to your email",
          icon: "info"
        });
        if(err.status===200)
        {
          //console.log('check your email');
          this.router.navigate(['/CodeConfirm',this.email?.value])
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
  // verifyCode() {
  //   console.log(this.email?.value);
  //   console.log(Number(this.code));
    
  // this._userServices.checkCode(this.email?.value,Number(this.code)).subscribe((res)=>{
  //   if (res) { 
  //     this.router.navigate(['/ResetPassword'],{ queryParams: { email: this.email?.value , code : this.code} });
  //   } else {
  //     Swal.fire({
  //       icon: 'error',
  //       title: 'Oops...',
  //       text: 'Invalid code!',
  //     });
  //   }
  // })

  // }

  // showCodeModal() {
  //   this.renderer.addClass(this.codeModal.nativeElement, 'show'); // Add 'show' class to display the modal
  //   this.renderer.setStyle(this.codeModal.nativeElement, 'display', 'block'); // Set display style to 'block'
  // }

  // hideCodeModal() {
  //   // Hide the modal
  //   this.renderer.removeClass(this.codeModal.nativeElement, 'show'); // Remove 'show' class to hide the modal
  //   this.renderer.setStyle(this.codeModal.nativeElement, 'display', 'none'); // Set display style to 'none'
  // }


}
