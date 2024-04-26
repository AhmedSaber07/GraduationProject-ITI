import {  Router, RouterModule } from '@angular/router';
import { Icategory } from '../../../Category/Models/icategory';
import { CategoryService } from './../../../Category/Services/category.service';
import { Component, OnInit} from '@angular/core';
import { DataSharingService } from '../../Services/data-sharing.service';
import { ShoppingcartService } from '../../../ShoppingCart/Services/shoppingcart.service';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../../User/Services/user.service';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule,TranslateModule,FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean = false;
  username: string = '';
  Categories:Icategory[]=[];
  countOFItemsInCart:number=0;
  getCategory!:Icategory;
  lang:string='ar';
  baseUrlForIamge:string='https://smhttp-ssl-73217.nexcesscdn.net/pub/media/wysiwyg/smartwave/porto/flags/';
  imagePath:string=`${this.baseUrlForIamge}en.png`;
  searchQuery: string = '';
  constructor(private _userService:UserService,private _dataSharingService:DataSharingService ,private _shoppingcartService:ShoppingcartService,private _categoryService:CategoryService,private router:Router){}
  ngOnInit(): void {

    this.updateAuthenticationStatus();
    
    this._userService.authenticationStatus.subscribe(() => {
      this.updateAuthenticationStatus();
    });
    
    this.getCategories();

    this._shoppingcartService.count$.subscribe((count) => {
      this.countOFItemsInCart = count;
    });
    this._dataSharingService.language$.subscribe(lang=>{
    this._shoppingcartService.getCart(lang).subscribe((res)=>{
      this.countOFItemsInCart = res.count;
      //console.log(this.countOFItemsInCart);
    })
  });
  
  }

  showCategories(categoryId:string)
  {
    this._dataSharingService.changeCategoryId(categoryId);
    this.router.navigate(['/ChildrenOFCategory', categoryId]);
  }

  ChangeLanguage() {
    this._dataSharingService.setLanguage(this.lang === 'ar' ? 'en' : 'ar');
    this.lang = localStorage.getItem('lang') || 'ar';
    this.imagePath = `${this.baseUrlForIamge}${this.lang === 'ar' ? 'en' : 'ar'}.png` ;
  }

  onSearchInput(): void {
    this._dataSharingService.setSearchQuery(this.searchQuery);
  }

  getCategories(): void {
    this._dataSharingService.language$.subscribe(lang=>{
    this._categoryService.getCategories(lang).subscribe(data => {
      this.Categories = data;
    });
  });
  }
getCategoryById(id:string):void{
  this._dataSharingService.changeCategoryId(id);
  this.router.navigate(['/ChildrenOFCategory',id]);  
}

updateAuthenticationStatus() {
  this.isLoggedIn = this._userService.isAuthenticated();
  if (this.isLoggedIn) {
    this.username = this._userService.getFullName(); 
    this._userService.fullName$.subscribe(fullName=>{
     if(fullName!=='')
      this.username = fullName;
    })
  }
}

logout() {
  this._userService.expireToken().subscribe(
    (data)=>{
    console.log(data);
    this.router.navigate(['/Home']);  
  },
  (error)=>{
    this._userService.logout();
    this.updateAuthenticationStatus();
    this.router.navigate(['/Home']);  
  })
}

}