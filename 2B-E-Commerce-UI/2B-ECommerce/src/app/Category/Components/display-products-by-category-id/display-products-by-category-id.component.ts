import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../Services/category.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Iproduct } from '../../../Product/Models/iproduct';
import { CommonModule } from '@angular/common';
import { DataSharingService } from '../../../Shared/Services/data-sharing.service';
import { FormsModule } from '@angular/forms';
import { ShoppingcartService } from '../../../ShoppingCart/Services/shoppingcart.service';
import { Icategory } from '../../Models/icategory';
import { PaginatorModule } from 'primeng/paginator';
import { ICartItem } from '../../../ShoppingCart/Models/i-cart-item';
import { TranslateModule } from '@ngx-translate/core';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgxPaginationModule } from 'ngx-pagination';
@Component({
  selector: 'app-display-products-by-category-id',
  standalone: true,
  imports: [CommonModule,FormsModule,TranslateModule,PaginatorModule,MatPaginatorModule,NgxPaginationModule],
  templateUrl: './display-products-by-category-id.component.html',
  styleUrl: './display-products-by-category-id.component.css'
})
export class DisplayProductsByCategoryIdComponent implements OnInit {
categoryId:string="";
sortBy: string = 'price';
FilterdProduct!:Iproduct[];
categoryViewList!:Icategory[];
addItem!:ICartItem;
stars: number[] = [1, 2, 3, 4, 5];

pageSize = 2;
currentPage = 1;


constructor(private _dataSharingService: DataSharingService,
            private router: Router,private _categoryService:CategoryService,
            private _shoppingCartService:ShoppingcartService,
            private route: ActivatedRoute){}
ngOnInit(): void {
    
    this.categoryId =(this.route.snapshot.paramMap.get('id'))?.toString() ?? "";
    //console.log(this.categoryId);
    
    this._dataSharingService.currentCategoryId.subscribe(categoryId => {
      if(categoryId!=="")
      this.categoryId = categoryId; 

     // console.log(this.categoryId);
      this.getProductsByCategoryId(this.categoryId);
      this.getCategoriesById(this.categoryId);
    })
    
  
  
  }

showProductDetails(productId: string) {
    this.router.navigate(['/ProductDetails', productId]);
    }

showProducts(Id:string)
    {
      this.router.navigate(['/ChildrenOFCategory', Id]);
      this.getProductsByCategoryId(Id);
      this.getCategoriesById(Id);
    }


sortProducts() {
    if (this.sortBy === 'price') {
      this.FilterdProduct.sort((a, b) => a.price - b.price);
    } else if (this.sortBy === 'quantity') {
      this.FilterdProduct.sort((a, b) => a.stockQuantity - b.stockQuantity);
    }
  }

addToCart(productId:string) {
  this.addItem={
    productid:productId,
    quantity:1
  };
  this._shoppingCartService.addToCart(this.addItem).subscribe((res)=>{
//   console.log(res);
   this._shoppingCartService.updateCartCount();
  // alert(res.message);
  });
}




getProductsByCategoryId(categoryId:string){
  this._dataSharingService.language$.subscribe(lang=>{
  this._categoryService.getProductsByCategoryId(categoryId,lang).subscribe((prds)=>{ 
    this.FilterdProduct = prds;
   // console.log(this.FilterdProduct);
    // console.log(items);
    // console.log(pageSize);
    
  });
});
}


getCategoriesById(categoryId:string){
  this._dataSharingService.language$.subscribe(lang=>{
  this._categoryService.getCategoriesById(categoryId,lang).subscribe((data)=>{
   // console.log(data);
    this.categoryViewList = data;
    //console.log(this.categoryViewList);
  });
});
}

onPageSizeChange(event: any) {
  const value = event.target.value;
  this.pageSize = parseInt(value, 10);
  this.getProductsByCategoryId(this.categoryId);
}

shouldShowPaginator(): boolean {
  return this.FilterdProduct.length > this.pageSize;
}

}
