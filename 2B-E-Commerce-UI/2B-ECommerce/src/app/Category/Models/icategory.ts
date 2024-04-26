export interface Icategory {
id:string;
name:string;
parentCategoryId?:string;
createdAt?:string;
children?:Icategory[];   
}
