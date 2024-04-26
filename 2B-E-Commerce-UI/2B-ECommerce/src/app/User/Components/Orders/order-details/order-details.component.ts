import { OrderServiceService } from './../../../../ShoppingCart/Services/order.service';
import { Component, OnInit } from '@angular/core';
import { IOrderItems } from '../../../../ShoppingCart/Models/iorder-items';
import { ActivatedRoute } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { DataSharingService } from '../../../../Shared/Services/data-sharing.service';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [TranslateModule],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css'
})
export class OrderDetailsComponent implements OnInit {
  orderItems!:IOrderItems[] 
orderNumber!:number;
constructor(private  _orderService:OrderServiceService,private route:ActivatedRoute,private _dataSharingService:DataSharingService) {
}
  ngOnInit(): void {

    this.orderNumber =Number((this.route.snapshot.paramMap.get('orderNumber')));
    this._dataSharingService.language$.subscribe(lang=>{
    this._orderService.GetOrderDetailsByOrderNumber(this.orderNumber,lang).subscribe((res)=>{
      this.orderItems = res.entities;
      //console.log(this.orderItems);  
    });
  });
  
  }
}
