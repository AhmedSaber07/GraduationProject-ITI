
import { Routes } from '@angular/router';
import { ProductListComponent } from './Product/Components/product-list/product-list.component';
import { ProductDetailsComponent } from './Product/Components/product-details/product-details.component';
import { DisplayProductsByCategoryIdComponent } from './Category/Components/display-products-by-category-id/display-products-by-category-id.component';
import { ShoppingCartComponent } from './ShoppingCart/Components/shopping-cart/shopping-cart.component';
import { LoginComponent } from './User/Components/Login/login/login.component';
import { RegisterComponent } from './User/Components/Sing Up/register/register.component';
import { MyAccountComponent } from './User/Components/My Account/my-account/my-account.component';
import { MyOrdersComponent } from './User/Components/Orders/my-orders/my-orders.component';
import { PaymentConfirmComponent } from './ShoppingCart/Components/payment-confirm/payment-confirm.component';
import { AccountInformationComponent } from './User/Components/Account Information/account-information/account-information.component';
import { AddressBookComponent } from './User/Components/Address Book/address-book/address-book.component';
import { UserProfileComponent } from './User/Components/UserProfile/user-profile/user-profile.component';
import { UserinfoContainerComponent } from './User/Components/userinfo-container/userinfo-container.component';
import { ResetPasswordComponent } from './User/Components/reset-password/reset-password.component';
import { ForgetPasswordComponent } from './User/Components/forget-password/forget-password.component';
import { authGuard } from './User/Guards/auth.guard';
import { NotFoundComponent } from './Shared/Components/not-found/not-found.component';
import { ConfirmCodeComponent } from './User/Components/confirm-code/confirm-code.component';
import { OrderDetailsComponent } from './User/Components/Orders/order-details/order-details.component';

export const routes: Routes = [
    {path:'',redirectTo:'Home',pathMatch:'full'},
    {path:'Home',component:ProductListComponent,title:'Home Page'},
    {path:'ProductDetails/:id',component:ProductDetailsComponent,title:'Product Details Page'},
    {path:'ChildrenOFCategory/:id',component:DisplayProductsByCategoryIdComponent,title:'Category Children Page'},
    {path:'ShoppingCart',component:ShoppingCartComponent,title:'Shopping Cart Page'},
    {path :'login', component:LoginComponent, title :'Log  In'},
    {path :'regester', component:RegisterComponent , title :'Regester'},
    {path :'ResetPassword', component:ResetPasswordComponent , title :'Reset Password'},
    {path :'ForgetPassword', component:ForgetPasswordComponent , title :'Forget Password'},
    {path :'CodeConfirm/:email', component:ConfirmCodeComponent , title :'Code Confirm Page'},
    {path:'ConfirmPayment',component:PaymentConfirmComponent,title:'Confirm Payment',canActivate:[authGuard]},
    {path:'UserProfile',component:UserinfoContainerComponent,
    canActivate:[authGuard],
    children:[
        { path: '', redirectTo: 'MyAccount', pathMatch: 'full' },
        {path :'MyAccount', component:MyAccountComponent, title :'My Account Page'},
        {path :'MyOrders', component:MyOrdersComponent, title :'My Orders Page'},
        {path :'orderDetails/:orderNumber', component:OrderDetailsComponent, title :'OrderDetails Page'},
        {path :'AccountInformation', component:AccountInformationComponent, title :'Account Information Page'},
        {path :'AddressBook', component:AddressBookComponent, title :'AddressBook Page'},
        {path :'UserInfo', component:UserProfileComponent, title :'UserProfile Page'},
    ],
},
{path:'**',component:NotFoundComponent,title:'NotFound Page'},
];
