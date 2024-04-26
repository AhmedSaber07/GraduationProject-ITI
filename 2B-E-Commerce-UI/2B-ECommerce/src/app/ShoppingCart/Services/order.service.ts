import { ICreateOrder } from '../Models/icreate-order';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { IOrderViewList } from '../Models/iorder-view-list';
import { IOrderItems } from '../Models/iorder-items';

@Injectable({
  providedIn: 'root'
})
export class OrderServiceService {
  private OrderApi = environment.apiUrl + '/Order';
  constructor(private httpClient:HttpClient) { }

createOrder(order:ICreateOrder):Observable<any>{
return this.httpClient.post<any>(`${this.OrderApi}?email=${order.email}&transactionId=${order.transactionId}&sessionId=${order.sessionId}`,{});
}

GetOrdersForUser(email:string,lang:string):Observable<IOrderViewList>{
  const headers = new HttpHeaders().set('Accept-Language',lang);
  return this.httpClient.get<IOrderViewList>(`${this.OrderApi}/GetUserOrders?email=${email}`,{ headers: headers });
}

GetOrderDetailsByOrderNumber(orderNumber:number,lang:string):Observable<any>{
  const headers = new HttpHeaders().set('Accept-Language',lang);
  return this.httpClient.get<any>(`${this.OrderApi}/GetItemsOfOrder?OrderNumber=${orderNumber}`,{ headers: headers });
}
}

