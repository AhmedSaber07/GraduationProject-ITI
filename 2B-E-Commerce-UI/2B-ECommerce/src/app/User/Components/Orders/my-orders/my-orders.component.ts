import { NumberFormatStyle } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { OrderServiceService } from '../../../../ShoppingCart/Services/order.service';
import { UserService } from '../../../Services/user.service';
import { IOrderViewList } from '../../../../ShoppingCart/Models/iorder-view-list';
import { DateFormatPipe } from '../../../Pipes/date-format.pipe';
import { CustomDisplayPricePipe } from '../../../../Shared/Pipes/custom-display-price.pipe';
import { RouterLink } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { DataSharingService } from '../../../../Shared/Services/data-sharing.service';

@Component({
  selector: 'app-my-orders',
  standalone: true,
  imports: [DateFormatPipe,CustomDisplayPricePipe,RouterLink,TranslateModule],
  templateUrl: './my-orders.component.html',
  styleUrl: './my-orders.component.css'
})
export class MyOrdersComponent implements OnInit{
  hasOrders: boolean = false;
  orders!:IOrderViewList;
  order: any[] = [
    {
      orderNumber: 20004636,
      date: '20/02/2034',
      recipient: 'Ahmed',
      total: 1860,
      status: 'قيد الانتظار',
      action: 'تفاصيل الطلب'
    }
   
  ];

  constructor(private _orderService:OrderServiceService,private _userSerice:UserService,private _dataSharingService:DataSharingService) {
  
    this.hasOrders = this.order.length > 0;
  }
  ngOnInit(): void {
    this._dataSharingService.language$.subscribe(lang=>{
    this._orderService.GetOrdersForUser(this._userSerice.getEmail(),lang).subscribe((res)=>{
      this.orders = res;
      //console.log(this.orders);
    })
  })
  }


}

