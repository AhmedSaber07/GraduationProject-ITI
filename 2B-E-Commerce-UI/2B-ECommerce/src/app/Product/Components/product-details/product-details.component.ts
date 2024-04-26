import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CustomDisplayPricePipe } from '../../../Shared/Pipes/custom-display-price.pipe';
import { DiplayProductDescriptionPipe } from '../../Pipes/diplay-product-description.pipe';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../Services/product.service';
import { Iproduct } from '../../Models/iproduct';
import { ICartItem } from '../../../ShoppingCart/Models/i-cart-item';
import { ShoppingcartService } from '../../../ShoppingCart/Services/shoppingcart.service';
import { DataSharingService } from '../../../Shared/Services/data-sharing.service';
import { TranslateModule } from '@ngx-translate/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IReview } from '../../Models/ireview';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule,CustomDisplayPricePipe,DiplayProductDescriptionPipe,TranslateModule,ReactiveFormsModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {
productId:string=" ";
selectedImage: string | undefined;
product:Iproduct | undefined;
stars: number[];
addItem!:ICartItem;
ReviewForm!: FormGroup;
productReview!:IReview;
constructor(private router: Router,private fb: FormBuilder,private route: ActivatedRoute,private _productService:ProductService,private _shoppingCartService: ShoppingcartService,private dataSharingService: DataSharingService){
  this.ReviewForm = this.fb.group({
    Quality:['',[Validators.required]],
    Price:['',[Validators.required]],
    Value:['',[Validators.required]],
    Nickname: ['', [Validators.required]],
    Summary: ['', [Validators.required]],
    Review: ['', [Validators.required]]
  });
  this.stars = Array(5).fill(0).map((_, i) => i + 1);
}
// updateRating(ratingType: string, value: number) {
//   this.ReviewForm.patchValue({ [ratingType]: value });
// }


ngOnInit() {

  this.dataSharingService.language$.subscribe(lang=>{
  this.productId =(this.route.snapshot.paramMap.get('id'))?.toString() ?? "";
   this._productService.getProductByid(this.productId,lang).subscribe((prd)=>{
    this.product =prd.entity;
    this.selectedImage = this.product?.images[0];
   });
  
  })
  
}

get Quality() { return this.ReviewForm.get('Quality'); };
get Price() { return this.ReviewForm.get('Price');}
get Value() { return this.ReviewForm.get('Value');}
get Nickname() { return this.ReviewForm.get('Nickname');}
get Summary() { return this.ReviewForm.get('Summary');}
get Review() { return this.ReviewForm.get('Review');}


updateSelectedImage(image: string) {
  this.selectedImage = image;
}

addToCart(productId:string) {
  this.addItem={
    productid:productId,
    quantity:1
  };
  this._shoppingCartService.addToCart(this.addItem).subscribe((res)=>{
  // console.log(res);
   this._shoppingCartService.updateCartCount();
  // alert(res.message);
  });
}

onSubmit(){

  this.productReview = {
  nickName:this.Nickname?.value,
  summary:this.Summary?.value,
  reviewText:this.Review?.value,
  qualityRating:Number(this.Quality?.value),
  priceRating:Number(this.Price?.value),
  valueRating:Number(this.Value?.value),
  productId:this.productId
}

//console.log(this.productReview);

this._productService.addReview(this.productReview).subscribe(
  (data)=>{
    if(data.isSuccess)
      {
        Swal.fire({
          title: "Done",
          text: "Review Added Successfully",
          icon: "success"
        });
      }
      this.router.navigate(['/Home']);
  },
  (err)=>
    {
    if(err.status===200)
      {
        Swal.fire({
          title: "Done",
          text: "Review Added Successfully",
          icon: "success"
        });
      }
      this.router.navigate(['/Home']);
  }
)

}

}

