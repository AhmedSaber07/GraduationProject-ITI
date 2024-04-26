import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../Services/user.service';
import { IUserAddress } from '../../../models/iuser-address';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
@Component({
  selector: 'app-address-book',
  standalone: true,
  imports: [FormsModule,CommonModule,ReactiveFormsModule,TranslateModule],
  templateUrl: './address-book.component.html',
  styleUrl: './address-book.component.css'
})
export class AddressBookComponent implements OnInit{
  governoratesArabic!:string[];
  governoratesEnglish!:string[];
  contactForm!: FormGroup;
  address!:IUserAddress;
  constructor(private formBuilder: FormBuilder,private _userService:UserService,private router:Router){}
ngOnInit(): void {

  this._userService.GetAddress(this._userService.getEmail()).subscribe((res: IUserAddress) => {
   // console.log(res);
   // console.log(res.addressLine1);
    this.address =res;
    this.initForm();
  });

  this.governoratesArabic = [
    "القاهرة",
    "الإسكندرية",
    "البحيرة",
    "الغربية",
    "المنوفية",
    "الشرقية",
    "الدقهلية",
    "كفر الشيخ",
    "شمال سيناء",
    "الإسماعيلية",
    "بورسعيد",
    "السويس",
    "الشرقية",
    "جنوب سيناء",
    "بني سويف",
    "الفيوم",
    "المنيا",
    "أسيوط",
    "الوادي الجديد",
    "سوهاج",
    "قنا",
    "الأقصر",
    "أسوان",
    "البحر الأحمر"
  ];
  this.governoratesEnglish=[
    "Cairo",
  "Alexandria",
  "Beheira",
  "Gharbia",
  "Monufia",
  "Sharqia",
  "Dakahlia",
  "Kafr El-Sheikh",
  "North Sinai",
  "Ismailia",
  "Port Said",
  "Suez",
  "Sharqia",
  "South Sinai",
  "Beni Suef",
  "Faiyum",
  "Minya",
  "Assiut",
  "New Valley",
  "Sohag",
  "Qena",
  "Luxor",
  "Aswan",
  "Red Sea"
  ]
}
initForm(): void {
  this.contactForm = this.formBuilder.group({
    streetLine1: [this.address ? this.address.addressLine1 : '', Validators.required],
    streetLine2: [this.address ? this.address.addressLine2 : ''],
    country: [this.address ? this.address.country : '', Validators.required],
    city: [this.address ? this.address.city : '', Validators.required],
  });
}
// get firstName() {
//   return this.contactForm.get('firstName');
// }

// get lastName() {
//   return this.contactForm.get('lastName');
// }

// get phoneNumber() {
//   return this.contactForm.get('phoneNumber');
// }

get streetLine1() {
  return this.contactForm.get('streetLine1');
}

get streetLine2() {
  return this.contactForm.get('streetLine2');
}

get city() {
  return this.contactForm.get('city');
}

get country() {
  return this.contactForm.get('country');
}

onSubmit(): void {
  //console.log(this.contactForm);
  
  if (this.contactForm.valid) {
    this.address = {
      Email :  this._userService.getEmail(),
      addressLine1:this.streetLine1?.value,
      addressLine2:this.streetLine2?.value,
      city:this.city?.value,
      country:this.country?.value
    }
    //console.log(this.address);
    this._userService.AddAddress(this.address).subscribe(
      (data)=>{
       // console.log(data);
      },
      (error)=>{
       // console.log(this.address);
        if(error.status === 200){
          
          Swal.fire({
            text: "Address Added Successfully",
            icon: "success"
          });
          this.router.navigate(['/UserProfile']); 
        }
      }
      )
  }
}

}
