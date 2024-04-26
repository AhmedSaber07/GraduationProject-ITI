import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { UserService } from '../../../Services/user.service';
import Swal from 'sweetalert2';
import { TranslateModule } from '@ngx-translate/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-account-information',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule,TranslateModule],
  templateUrl: './account-information.component.html',
  styleUrl: './account-information.component.css',
})
export class AccountInformationComponent implements OnInit {
  showInputs: boolean = false;
  formgroup!: FormGroup;
  email: string="";
  errorDescription:string="";
  currentPhone: string="";
  firstName:string="";
  lastName:string="";
  fullName:string="";
  checkBoxPass: boolean = false;
  checkBoxPhone: boolean = false;
  constructor(private fb: FormBuilder,private _userService:UserService,private router:Router,private route: ActivatedRoute) {
  }
  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {
      // Set the initial values of checkboxes based on query parameters
      this.checkBoxPass= params['checkbox1'] === 'true';
      this.checkBoxPhone = params['checkbox2'] === 'true';
      this.fullName = this._userService.getFullName();
      this.firstName = this.fullName.split(' ')[0];
      this.lastName = this.fullName.split(' ')[1];
      this.currentPhone = this._userService.getPhoneNumber();
      this.email = this._userService.getEmail();
      // Initialize form with initial values
      this.initForm();
    });
  }

  initForm(): void {
    this.formgroup = this.fb.group({
      firstName:[this.firstName,[Validators.required,Validators.minLength(3)]],
      lastName:[this.lastName,[Validators.required,Validators.minLength(3)]],
      changePassword: [this.checkBoxPass],
      changePhoneNumber: [this.checkBoxPhone],
      email: [
        '',
        [
          Validators.required,
          Validators.email,
          Validators.pattern(this.emailPattern),
        ],
      ],
      currentPassword: ['', [Validators.required]],
      newPassword: ['', [Validators.required, Validators.minLength(8),Validators.pattern(this.passwordPattern)]],
      password: ['', [Validators.required]],
      phoneNumber: [
        '',
        [Validators.required, Validators.pattern(this.phonePattern)],
      ],
      confirmPassword: ['', [Validators.required]]
    }, 
    {
      validator: this.passwordMatchValidator
    });
  
    this.formgroup.get('changePassword')?.valueChanges.subscribe(value => {
      if (value) {
        this.formgroup.get('changePhoneNumber')?.setValue(false);
      }
    });

    this.formgroup.get('changePhoneNumber')?.valueChanges.subscribe(value => {
      if (value) {
        this.formgroup.get('changePassword')?.setValue(false);
      }
    });

  }

  emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;
  passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
  phonePattern = /^01[0125][0-9]{8}$/;

get FirstName(){
  return this.formgroup.get('firstName');
}
get LastName(){
  return this.formgroup.get('lastName');
}
get Email() {
    return this.formgroup.get('email');
  }
get CurrentPassword() {
    return this.formgroup.get('currentPassword');
  }
get NewPassword() {
    return this.formgroup.get('newPassword');
  }
get ConfirmPassword() {
    return this.formgroup.get('confirmPassword');
  }
get Phone() {
    return this.formgroup.get('phoneNumber');
  }

  passwordMatchValidator(group: FormGroup): Promise<any> | Observable<any> {
    const passwordControl = group.get('newPassword');
    const confirmPasswordControl = group.get('confirmPassword');
  
    if (!passwordControl || !confirmPasswordControl) {
      return Promise.resolve(null);
    }
  
    if (passwordControl.value !== confirmPasswordControl.value) {
      confirmPasswordControl.setErrors({ mismatch: true });
      return Promise.resolve({ mismatch: true });
    } else {
      confirmPasswordControl.setErrors(null);
      return Promise.resolve(null);
    }
  }
  

  onSubmit() {
    const changePasswordChecked = this.formgroup.get('changePassword')?.value;
    const changePhoneNumberChecked = this.formgroup.get('changePhoneNumber')?.value;
    if(changePasswordChecked || changePhoneNumberChecked)
    {
    if(changePasswordChecked){
      if(this.CurrentPassword?.value===""||this.NewPassword?.value===""||this.ConfirmPassword?.value==="")
      {
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "Complete Data",
        });
      }
      else if(this.NewPassword?.value!==this.ConfirmPassword?.value)
      {
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "Password Not Matched!",
        });
      }
      else if (!this.passwordPattern.test(this.NewPassword?.value)||!this.passwordPattern.test(this.ConfirmPassword?.value)) {
        Swal.fire({
          icon: "error",
          title: "Invalid password Number",
          text: "Please enter a valid password number",
        });
      }
      else{
      this._userService.changePassword(this.CurrentPassword?.value,this.NewPassword?.value).subscribe(
        (data)=>{
          //console.log(data);
          Swal.fire({
            text: "Password Change Successfully",
            icon: "success"
          });
          this.router.navigate(['/UserProfile']); 
        },
        (error)=>{
         // console.log(error);
          if(error.error.length>0)
          {
          for(let err of error.error)
          {
          this.errorDescription+=err.description + "\n";
          }
          Swal.fire({
              icon: "error",
              text: this.errorDescription
            });    
          }
          else{
           if(error.status === 200){
            
            Swal.fire({
              text: "Password Change Successfully",
              icon: "success"
            });
           
            this.router.navigate(['/UserProfile']); 
          }
          else if(error.status===400){
            Swal.fire({
              icon: "error",
              title: "Oops...",
              text: "Current Password is Incorrect"
            });
          }
          else{
            Swal.fire({
              icon: "error",
              title: "Oops...",
              text: error.statusText
            });
          }
        }
        }
        );
    }
  }
   else if(changePhoneNumberChecked){
      if(this.Phone?.value==="")
      {
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "Enter New Phone Number",
        });
      }
      else if (!this.phonePattern.test(this.Phone?.value)) {
        Swal.fire({
          icon: "error",
          title: "Invalid Phone Number",
          text: "Please enter a valid phone number",
        });
      }
      else{
      this._userService.changePhoneNumber(this.currentPhone,this.Phone?.value).subscribe(
        (res)=>{
       // console.log(res);
      },
      (err)=>{
      //  console.log(err);
        if(err.status === 200){
            this._userService.setPhoneNumber(this.Phone?.value);
          Swal.fire({
            text: "Phone Number Change Successfully",
            icon: "success"
          });
          this.router.navigate(['/UserProfile']); 
      }
      else if(err.status === 400){
        //console.log(err);
      Swal.fire({
        text: "This phone is already taken",
        icon: "error"
      });
  }
    }
      );
      }
    }
  }
  if(this.FirstName?.value!=this.firstName||this.LastName?.value!=this.lastName)
{
  if(this.FirstName?.value===''||this.LastName?.value==='')
    {
      Swal.fire({
        icon: "error",
        text: "Full Name is Required",
      });
    }
  else if(this.FirstName?.value.length<3||this.LastName?.value.length<3)
    {
      Swal.fire({
        icon: "error",
        text: "Invalid Full Name",
      });
    }
    else{
    this._userService.ChangeName(this.email,this.FirstName?.value,this.LastName?.value).subscribe(
      (res)=>{
       // console.log(res);
         this._userService.setFullName(this.FirstName?.value,this.LastName?.value);
         Swal.fire({
          text: "Name Change Successfully",
          icon: "success"
        });
        this.router.navigate(['/UserProfile']); 
      },
      (err)=>{
        if(err.status===200)
          {
            this._userService.setFullName(this.FirstName?.value,this.LastName?.value);
            Swal.fire({
              text: "Name Change Successfully",
              icon: "success"
            });
            this.router.navigate(['/UserProfile']); 
          }
      }
    )
  }
}
  }
}