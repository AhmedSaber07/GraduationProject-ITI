import { Component, OnInit } from '@angular/core';
import { UserProfileComponent } from "../../UserProfile/user-profile/user-profile.component";
import { UserService } from '../../../Services/user.service';
import { Router, RouterModule } from '@angular/router';
import { IUserAddress } from '../../../models/iuser-address';
import { TranslateModule } from '@ngx-translate/core';

@Component({
    selector: 'app-my-account',
    standalone: true,
    templateUrl: './my-account.component.html',
    styleUrl: './my-account.component.css',
    imports: [UserProfileComponent,RouterModule,TranslateModule]
})
export class MyAccountComponent implements OnInit{
email:string="";
fullName:string="";
phoneNumber:string="";
AddressExist:boolean=false;
address!:IUserAddress;
constructor(private _userService:UserService,private router: Router){
}
    ngOnInit(): void {
        this.email = this._userService.getEmail();
        this.fullName = this._userService.getFullName();
        this.phoneNumber = this._userService.getPhoneNumber();
        this._userService.GetAddress(this.email).subscribe((res: IUserAddress) => {
           // console.log(res);
            //console.log(res.addressLine1);
            this.address =res;
            if (res.addressLine1 !== null) {   
                this.AddressExist = true;
                this.address = res;
            }
        });
    }

    changePasswordClicked(): void {
        this.router.navigate(['/UserProfile/AccountInformation'], { queryParams: { checkbox1: 'true' } });
      }
    
      changePhoneNumberClicked(): void {
        this.router.navigate(['/UserProfile/AccountInformation'], { queryParams: { checkbox2: 'true' } });
      }


}
