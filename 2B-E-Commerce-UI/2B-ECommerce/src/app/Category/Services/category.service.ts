import { DataSharingService } from './../../Shared/Services/data-sharing.service';
import { Injectable } from '@angular/core';
import { Icategory } from '../Models/icategory';
import { CategoryWithChildren } from '../Models/category-with-children';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CategoryViewList } from '../Models/category-view-list';
import { IdisplayProduct } from '../../Product/Models/idisplay-product';
import { Iproduct } from '../../Product/Models/iproduct';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private categoryApi = environment.apiUrl + '/Category';
Categories:Icategory[]=[];
  constructor(private httpClient: HttpClient,private dataSharingService:DataSharingService) { 
    // this.Categories=[
    //   {
    //     id:'#1',
    //     nameAr: 'كمبيوتر',
    //     children: [
    //       {
    //         id:'#11',
    //         nameAr: 'لاب توب',
    //         children: [
    //           {id:'#111', nameAr: 'اتش بي'},
    //           {id:'#112', nameAr: 'لينوفو'}
    //         ]
    //       },
    //       {
    //         id:'#12',
    //         nameAr: 'طابعات',
    //         children: [
    //           { id:'#121',nameAr: 'طابعات ليزر'}
    //         ]
    //       }
    //     ]
    //   },
    //   {
    //     id:'#2',
    //     nameAr: 'موبايل وتابلت',
    //     children: [
    //       {
    //         id:'#21',
    //         nameAr: 'موبايل',
    //         children: [
    //           { id:'#211',nameAr: 'ابل'}
    //           // Add more grandchildren here if needed
    //         ]
    //       }
    //       // Add more children here if needed
    //     ]
    //   }
    // ]
    this.dataSharingService.language$.subscribe(lang=>{
    this.getCategories(lang).subscribe((data)=>{
    this.Categories= data;  
    });
  });
  }
// getCategoeies():Icategory[]
// {
//   return this.Categories;
// }

//  findCategory(id:string,category: Icategory): CategoryWithChildren | null {
//   if (category.id === id) {
//       return { category, children: category.children || null };
//   }
//   if (category.children) {
//       for (const child of category.children) {
//           const result = this.findCategory(id,child);
//           if (result) {
//               return result;
//           }
//       }
//   }
//   return null;
// }

// getCategoryWithchildrenByid(id: string): CategoryWithChildren | null {
//   for (const category of this.Categories) {
//       const result = this.findCategory(id,category);
//       if (result) {
//         //console.log(result);
//           return result;
//       }
//   }
//   return null;
// }




getCategories(lang:string): Observable<Icategory[]> {
  const headers = new HttpHeaders().set('Accept-Language',lang);
  return this.httpClient.get<Icategory[]>(this.categoryApi+'/UpdatedGetall',{ headers: headers });
}



getCategoriesById(id:string,lang:string):Observable<Icategory[]>{
  const headers = new HttpHeaders().set('Accept-Language',lang);
  return this.httpClient.get<Icategory[]>(`${this.categoryApi}/GetAllChildrenById/${id}`,{ headers: headers })
}

getCategriesWithProducts(lang:string):Observable<IdisplayProduct[]>{
  const headers = new HttpHeaders().set('Accept-Language',lang);
  return this.httpClient.get<IdisplayProduct[]>(`${this.categoryApi}/getAllCattegoriesWtihProducts`,{ headers: headers });
}

getProductsByCategoryId(categoryId:string,lang:string):Observable<Iproduct[]>{
  const headers = new HttpHeaders().set('Accept-Language',lang);
  return this.httpClient.get<Iproduct[]>(`${this.categoryApi}/getAllProductsByCategoryId/${categoryId}`,{ headers: headers });
}

}
