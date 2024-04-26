
import { DataSharingService } from './../../../Shared/Services/data-sharing.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ShoppingcartService } from '../../../ShoppingCart/Services/shoppingcart.service';
import { IdisplayProduct } from '../../Models/idisplay-product';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../../Category/Services/category.service';
import { ICartItem } from '../../../ShoppingCart/Models/i-cart-item';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule,TranslateModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {

  Products: IdisplayProduct[] = [];
  Displayprds: IdisplayProduct[] = [];
  stars: number[] = [1, 2, 3, 4, 5];
  currentIndex: number = 0;
  searchQuery: string = '';
  addItem!:ICartItem;

  constructor(
    private _dataSharingService:DataSharingService,
    private _shoppingCartService: ShoppingcartService,
    private _categortService:CategoryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.updateDisplayedProducts();
    this._dataSharingService.searchQuery.subscribe(query => {
      this.searchQuery = query;
      this.updateDisplayedProducts();
    });

    setInterval(() => {
      this.rotateProducts();
      this.updateDisplayedProducts();
    }, 5000);
    this._dataSharingService.language$.subscribe(lang=>{
    this._categortService.getCategriesWithProducts(lang).subscribe((data)=>{
      this.Products = data;
    //  console.log(this.Products);
    });
    })

  }

  showProductDetails(productId: string) {
    this.router.navigate(['/ProductDetails', productId]);
  }

  addToCart(productId:string) {
    this.addItem={
      productid:productId,
      quantity:1
    };
    this._shoppingCartService.addToCart(this.addItem).subscribe((res)=>{
     //console.log(res);
     this._shoppingCartService.updateCartCount();
     //alert(res.message);
    });
  }

  rotateProducts() {
    if (this.currentIndex === this.Displayprds.length - 1) {
      this.currentIndex = 0;
    } else {
      this.currentIndex++;
    }
  }

  updateDisplayedProducts() {
    this.Displayprds = [];
    for (const category of this.Products) {
      const categoryProducts = category.products;
      const rotatedCategoryProducts = [];

      const filteredProducts = categoryProducts.filter(product =>
        product.name.toLowerCase().includes(this.searchQuery.toLowerCase())
      );

      for (let i = 0; i < (filteredProducts.length >5?5:filteredProducts.length) ; i++) {
        const index = (this.currentIndex + i) % filteredProducts.length;
        rotatedCategoryProducts.push(filteredProducts[index]);
      }
      this.Displayprds.push({ name: category.name, products: rotatedCategoryProducts });
    }
  }
}
