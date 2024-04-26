

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { IUserRegister } from '../models/i-user-register';
import { BehaviorSubject, Observable,tap } from 'rxjs';
import { IUserLogin } from '../models/i-user-login';
import { IResetPassword } from '../models/i-reset-password';
import { IUserAddress } from '../models/iuser-address';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  private registerApi = environment.apiUrl + '/UserAccount';
  private authenticationStatusSubject: BehaviorSubject<boolean>;
  private fullNameSubject = new BehaviorSubject<string>('');
  fullName$ = this.fullNameSubject.asObservable();

  constructor( private httpclient :HttpClient) {
    this.authenticationStatusSubject = new BehaviorSubject<boolean>(false);
  }

  get authenticationStatus(): Observable<boolean> {
    return this.authenticationStatusSubject.asObservable();
  }

getPhoneNumber():string{
  return localStorage.getItem('phone') ?? "";  
}

setPhoneNumber(phone: string): void {
  localStorage.setItem('phone', phone);
}

setFullName(fname:string,lname:string){
  localStorage.setItem('fullName',`${fname} ${lname}`);
//  console.log(`${fname} ${lname}`);
  this.fullNameSubject.next(`${fname} ${lname}`);
}

getFullName():string{
return localStorage.getItem('fullName') ?? "";
}

getEmail():string{
  return localStorage.getItem('email') ?? "";
}


ChangeName(email:string,fname:string,lname:string):Observable<any>
{
  return this.httpclient.post<any>(`${this.registerApi}/ChangeName?Email=${email}&FirstName=${fname}&LastName=${lname}`,{});
}

checkCode(email:string,code:number):Observable<boolean>{
  return this.httpclient.post<boolean>(`${this.registerApi}/CheckCode?code=${code}&email=${email}`,{});
}

sendCode(email:string):Observable<any>{
return this.httpclient.post<any>(`${this.registerApi}/SendCode?email=${email}`,{});
}


GetAddress(email:string):Observable<IUserAddress>{
  return this.httpclient.get<IUserAddress>(`${this.registerApi}/GetUserAddress?email=${email}`);
}

AddAddress(address:IUserAddress):Observable<any>{
  return this.httpclient.post<any>(`${this.registerApi}/AddAddress`,address);
}

DeleteAddress(email:string):Observable<any>{
  return this.httpclient.delete<any>(`${this.registerApi}/DeleteAddress?Email=${email}`,{});
}

changePhoneNumber(oldNumber:string,newNumber:string):Observable<any>{
  // const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  return this.httpclient.post<any>(`${this.registerApi}/UpdatePhone?oldPhone=${oldNumber}&newPhone=${newNumber}`,{});
}

changePassword(oldPassword:string,NewPassword:string):Observable<any>{
  return this.httpclient.post<any>(`${this.registerApi}/changepassword?Email=${this.getEmail()}&NewPassword=${NewPassword}&oldPassword=${oldPassword}`,{});
}

resetPassword(resetPassword:IResetPassword){
  return this.httpclient.post<any>(`${this.registerApi}/NewResetPassword`,resetPassword);
}

forgetPassword(email:string):Observable<any>{
  return this.httpclient.post<any>(`${this.registerApi}/forget-password?email=${email}`,{});
}

register(userData: IUserRegister,role:string): Observable<any> {
    
    return this.httpclient.post<any>(`${this.registerApi}/Register?role=${role}`,userData);
  }

login(userData: IUserLogin): Observable<any> {
    return this.httpclient.post<any>(`${this.registerApi}/login`, userData).pipe(
      tap(response => {
        if (response && response.token) {
         // console.log(response._user);
          localStorage.setItem('token', response.token);
          localStorage.setItem('fullName',`${response._user.firstName} ${response._user.lastName}`);
          localStorage.setItem('email',response._user.email);
          localStorage.setItem('phone',response._user.phoneNumber);
          this.authenticationStatusSubject.next(true);
        }
      })
    );
  }

externalLogin():Observable<any>{
  return this.httpclient.get<any>(`${this.registerApi}/ExternalLogin`);
}


  expireToken():Observable<any>{
  return this.httpclient.post<any>(`${this.registerApi}/Logout`, {});
}

logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('fullName');
    localStorage.removeItem('email');
    localStorage.removeItem('phone');
    this.authenticationStatusSubject.next(false);
  }

isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

}
