import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { IReview } from '../Models/ireview';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private productUrl:string=environment.apiUrl+'/Product';
  private reviewUrl:string=environment.apiUrl+'/Review';
  constructor(private httpClient:HttpClient,private translateService: TranslateService) { 
  }

  getProductByid(productId:string,lang:string):Observable<any>{  
  const headers = new HttpHeaders().set('Accept-Language',lang);
  return this.httpClient.get<any>(`${this.productUrl}/${productId}`,{ headers: headers });
}

addReview(review:IReview):Observable<any>{
  return this.httpClient.post<any>(`${this.reviewUrl}`,review);
}

}
