
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ICartItem } from '../Models/i-cart-item';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShoppingcartService 
{
  private shoppingCartApi = environment.apiUrl + '/Cart';
  private tranasctionId!:number;

  private countSubject = new BehaviorSubject<number>(0);
  count$ = this.countSubject.asObservable();
  
  constructor(private httpClient:HttpClient) {

   }

  
 updateCartCount():void{
     this.httpClient.get<any>(`${this.shoppingCartApi}/${this.getSessionId()}`).subscribe(
      (res) => {
        this.countSubject.next(res.count);
        //return res.count;
      })
  }


setTransactionNumber(t:number)
{
this.tranasctionId = t;
}
getTransactionNumber(){
  return this.tranasctionId;
}

DeleteCart():Observable<any>{
  return this.httpClient.delete<any>(`${this.shoppingCartApi}/DeleteCart/${this.getSessionId()}`);
}

addToCart(data: ICartItem): Observable<any> {
  return this.httpClient.post<any>(`${this.shoppingCartApi}/${this.getSessionId()}?productid=${data.productid}&quantity=${data.quantity}`, {}).pipe(
  )
}

removeFromCart(productId: string): Observable<any> {
  return this.httpClient.put<any>(`${this.shoppingCartApi}/RemoveItem/${this.getSessionId()}?productId=${productId}`, {}).pipe(
  );
}

increaseQuantity(productId: string): Observable<any> {
  return this.httpClient.put<any>(`${this.shoppingCartApi}/IncreaseQuantity/${this.getSessionId()}?productId=${productId}`, {});
}

decreaseQuantity(productId: string): Observable<any> {
  return this.httpClient.put<any>(`${this.shoppingCartApi}/DecreaseQuantity/${this.getSessionId()}?productId=${productId}`, {});
}

getCart(lang:string):Observable<any>{
  const headers = new HttpHeaders().set('Accept-Language',lang);
  return this.httpClient.get<any>(`${this.shoppingCartApi}/${this.getSessionId()}`,{ headers: headers })
}


generateGuid(): string {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    const r = Math.random() * 16 | 0;
    const v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}



getSessionId(): string {
  const existSessionId = localStorage.getItem('sessionId');
  if (existSessionId) {
    return existSessionId;
  } else {
    const newSessionId = this.generateGuid();
    localStorage.setItem('sessionId', newSessionId);
    return newSessionId;
  }
}

removeSessionId(){
  localStorage.removeItem('sessionId');
}
}
