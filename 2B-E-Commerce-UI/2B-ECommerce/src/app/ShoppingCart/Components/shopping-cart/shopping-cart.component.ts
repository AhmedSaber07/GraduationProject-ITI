import { ICreateOrder } from './../../Models/icreate-order';

import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShoppingcartService } from '../../Services/shoppingcart.service';
import { DiplayProductDescriptionPipe } from '../../../Product/Pipes/diplay-product-description.pipe';
import { CustomDisplayPricePipe } from '../../../Shared/Pipes/custom-display-price.pipe';
import {  Router } from '@angular/router';
import { UserService } from '../../../User/Services/user.service';
import { GetItems } from '../../Models/get-items';
import { FormsModule } from '@angular/forms';
import { OrderServiceService } from '../../Services/order.service';
import Swal from 'sweetalert2';
import { TranslateModule } from '@ngx-translate/core';
import { DataSharingService } from '../../../Shared/Services/data-sharing.service';
@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [DiplayProductDescriptionPipe,CustomDisplayPricePipe,FormsModule,TranslateModule],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.css'
})
export class ShoppingCartComponent implements OnInit{
  total:number=0;
  getItems:GetItems[]=[];
  createOrder!:ICreateOrder;
@ViewChild('paymentRef',{static:true}) paymentRef!:ElementRef;

  constructor(private _dataSharingService:DataSharingService,private _orderService:OrderServiceService,private _shoppingCartService:ShoppingcartService,private router:Router,private _userService:UserService){}
  ngOnInit() {
    window.paypal.Buttons({
      createOrder:(data:any,actions:any)=>{
        if(this._userService.isAuthenticated())
        {
        return actions.order.create({
          purchase_units:[
            {
              amount:{
                value:this.total.toString(),
                currency_code:'USD'
              }
            }
          ]
        })
      }
      else{
        localStorage.setItem('redirect','/ShoppingCart');
        this.router.navigate(['/login']);
      }
      },
      onApprove:(data:any,actions:any)=>{
        return actions.order.capture().then((details:any)=>{
          if(details.status==='COMPLETED')
          {
            // this._shoppingCartService.clearCart();
            this.createOrder={
              email:this._userService.getEmail(),
              transactionId:details.id,
              sessionId:this._shoppingCartService.getSessionId()
            }
            this._shoppingCartService.removeSessionId();
            this._shoppingCartService.updateCartCount();
            //clear cart
            //console.log(this.createOrder);
            this._orderService.createOrder(this.createOrder).subscribe((res)=>{
              //console.log(res);
              Swal.fire({
                title: "Done",
                text: "Order Created Successfully",
                icon: "success"
              });
            })
             this.router.navigate(['Home']);
            // this._shoppingCartService.setTransactionNumber(details.id);
            // console.log(details.id);
            // console.log(details);
            // this.router.navigate(['ConfirmPayment']);
          }
          
        });
      },
      onError:(error:any)=>{
      //  console.log(error);
      }
    }).render(this.paymentRef.nativeElement);

    this.loadCart();
}

loadCart() {
  this._dataSharingService.language$.subscribe(lang=>{
  this._shoppingCartService.getCart(lang).subscribe(data => {
    this.total = data.totalPrice;
    this.getItems = data.entities;
  })});;
}

removeFromCart(productId: string) {
    this._shoppingCartService.removeFromCart(productId).subscribe(() => {
      this.getItems = this.getItems.filter(item => item.product.id !== productId);
      this.loadCart();
      this._shoppingCartService.updateCartCount();
    });
  }

incrementQuantity(productId: string) {
  this._shoppingCartService.increaseQuantity(productId).subscribe(()=>{
    this.loadCart();
  });
}

decrementQuantity(productId: string) {
  this._shoppingCartService.decreaseQuantity(productId).subscribe(()=>{
    this.loadCart();
  });
  }

  calculateItemPrice(item: GetItems): number {
    return item.product.price * item.quantity;
  }
  
BackToHome(){
  return this.router.navigate(['/Home']);
}

}

