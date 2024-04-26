import { ShoppingcartService } from './../../Services/shoppingcart.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-payment-confirm',
  standalone: true,
  imports: [],
  templateUrl: './payment-confirm.component.html',
  styleUrl: './payment-confirm.component.css'
})
export class PaymentConfirmComponent implements OnInit{
  transactionNumber:number=0;
constructor(private _shoppingcartService:ShoppingcartService) {
}
  ngOnInit(): void {
    this.transactionNumber = this._shoppingcartService.getTransactionNumber();
  }

}
