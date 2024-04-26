import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import { DataSharingService } from '../../../Shared/Services/data-sharing.service';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-confirm-code',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterModule,RouterLink,TranslateModule],
  templateUrl: './confirm-code.component.html',
  styleUrls: ['./confirm-code.component.css']
})
export class ConfirmCodeComponent implements OnInit, OnDestroy {
  digit1: string = '';
  digit2: string = '';
  digit3: string = '';
  digit4: string = '';
  email: string = '';
  timerSubscription!: Subscription;
  timerActive: boolean = true;
  remainingTime: number = 60;
  private timerInterval: any;

  constructor(private route: ActivatedRoute, private _userServices: UserService, private router: Router,private _dataSharing:DataSharingService) { }

  ngOnDestroy(): void {
    // clearInterval(this.timerInterval);
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  ngOnInit(): void {
    this.email = String((this.route.snapshot.paramMap.get('email')));
    this.startTimer();
  }

  isNumberKey(event: KeyboardEvent) {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      event.preventDefault();
    }
  }

  onInputChange(value: string, nextInputId: string | null) {
    if (value.length === 1 && nextInputId) {
      const nextInput = document.getElementById(nextInputId) as HTMLInputElement;
      if (nextInput) {
        nextInput.focus();
      }
    }

    if (this.digit1 && this.digit2 && this.digit3 && this.digit4) {
      this.VerifyCode();
    }
  }

  startTimer() {
    this.timerActive = false;
    this.timerSubscription = this._dataSharing.startTimer(60).subscribe(
      time=>{
        this.remainingTime = time;
        if(time===0)
        {
          this.timerActive = true;
          this.timerSubscription.unsubscribe();
        }
      }
    )
    // this.timerInterval = setInterval(() => {
    //   this.remainingTime--;
    //   if (this.remainingTime === 0) {
    //     clearInterval(this.timerInterval);
    //     this.timerActive = false;
    //   }
    // }, 1000);
  }

  resetTimer() {
    this._userServices.sendCode(this.email).subscribe(
      // (data) => {
      //   Swal.fire({
      //     title: "Check Your Email",
      //     text: "we send verification code to your email",
      //     icon: "info"
      //   });
      // },
      // (err) => {
      //   Swal.fire({
      //     title: "Check Your Email",
      //     text: "we send verification code to your email",
      //     icon: "info"
      //   });
      //   if (err.status !== 200) {
      //     Swal.fire({
      //       icon: "error",
      //       title: "Oops...",
      //       text: "This Email is Not Found"
      //     });
      //   }
      // }
    );
    clearInterval(this.timerInterval);
    this.remainingTime = 60; 
    this.startTimer();
  }

  VerifyCode() {
    const code = this.digit1 + this.digit2 + this.digit3 + this.digit4;
    this._userServices.checkCode(this.email, Number(code)).subscribe(
      (res) => {
        if (res) {
          this.router.navigate(['/ResetPassword'], { queryParams: { email: this.email, code: code } });
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Invalid code!'
          });
        }
      });
  }
}
