using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.ProductImageDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mapper
{
    public class autoMapperProfile : Profile
    {
        public autoMapperProfile()
        {

            // product
            CreateMap<Domain.DTOs.productDto.createDto, Product>().ReverseMap();
            CreateMap<Domain.DTOs.productDto.updateDto, Product>().ReverseMap();
            CreateMap<Domain.DTOs.productDto.GetProductDto, Product>().ReverseMap();
            CreateMap<Product, Domain.DTOs.productDto.getProductwithImage>().ForMember(dest => dest.Images,
                opt => opt.MapFrom(src=>
                           src.Images.Select(image => image.imageUrl).ToList()));
            CreateMap<Product,Domain.DTOs.productDto.getProductCartDto>().ForMember(dest => dest.Images,
                opt => opt.MapFrom(src =>
                           src.Images.Select(image => image.imageUrl).ToList())).ReverseMap();

            // product-Image
            CreateMap<Domain.DTOs.ProductImageDto.CreateDto, ProductImage>().ReverseMap();
            CreateMap<Domain.DTOs.ProductImageDto.CreateWithProductDto, ProductImage>().ReverseMap();
            CreateMap<Domain.DTOs.ProductImageDto.UpdateDto, ProductImage>().ReverseMap();
            CreateMap<Domain.DTOs.ProductImageDto.GetProductImageDto, ProductImage>().ReverseMap();
            CreateMap<Domain.DTOs.ProductImageDto.getImage, ProductImage>().ReverseMap();

            // review
            CreateMap<Domain.DTOs.ReviewDto.CreateOrUpdateDto, Review>().ReverseMap();
            CreateMap<Domain.DTOs.ReviewDto.GetReviewDto, Review>().ReverseMap();

            // order-item
            CreateMap<Domain.DTOs.OrderItemDto.CreateDto, orderItem>().ReverseMap();
            CreateMap<Domain.DTOs.OrderItemDto.UpdateDto, orderItem>().ReverseMap();
            CreateMap<Domain.DTOs.OrderItemDto.GetOrderItemDto, orderItem>().ReverseMap();


            //category
            CreateMap<Domain.DTOs.CategoryDto.CreateOrUpdateCategoryDto, Category>().ReverseMap();
            CreateMap<Domain.DTOs.CategoryDto.ReadCategoryDto, Category>().ReverseMap();
            CreateMap<Domain.DTOs.CategoryDto.getDto, Category>().ReverseMap();


            // payment 
            CreateMap<Domain.DTOs.PaymentDto.CreateDto, Payment>().ReverseMap();

            //shopping-cart
            CreateMap<Domain.DTOs.CartDto.CreateOrUpdateDto, Cart>().ReverseMap();
            CreateMap<Domain.DTOs.CartDto.GetCartWithProductsDto, Cart>().ReverseMap();
            CreateMap<Domain.DTOs.CartDto.GetCartDto, Cart>().ReverseMap();

            // order
            CreateMap<Domain.DTOs.OrderDto.CreateOrUpdateDto, Order>().ReverseMap();
            CreateMap<Domain.DTOs.OrderDto.GetOrderDto, Order>().ReverseMap();
            CreateMap<Domain.DTOs.OrderDto.GetOrderISDeletedDto, Order>().ReverseMap();
            CreateMap<Domain.DTOs.OrderDto.getOrdersWithoutItems, Order>().ReverseMap();

            //brand

            CreateMap<Domain.DTOs.BrandDto.CreateDto, Brand>().ReverseMap();
            CreateMap<Domain.DTOs.BrandDto.GetBrandDto, Brand>().ReverseMap();
            CreateMap<Domain.DTOs.BrandDto.UpdateDto, Brand>().ReverseMap();

            //user
            CreateMap<Domain.DTOs.UserAccount.AddressDto, MyUser>().ReverseMap();

            
            #region localization

            //Brand
            CreateMap< listResultDto<Domain.DTOs.BrandDto.GetBrandDto>,listResultDto<Domain.DTOs.BrandDto.GetBrandDtoArabic>> ().ReverseMap();
            CreateMap< listResultDto<Domain.DTOs.BrandDto.GetBrandDto>,listResultDto<Domain.DTOs.BrandDto.GetBrandDtoEnglish>> ().ReverseMap();
            CreateMap< resultDto<Domain.DTOs.BrandDto.GetBrandDto>,resultDto<Domain.DTOs.BrandDto.GetBrandDtoEnglish>> ().ReverseMap();
            CreateMap< resultDto<Domain.DTOs.BrandDto.GetBrandDto>,resultDto<Domain.DTOs.BrandDto.GetBrandDtoArabic>> ().ReverseMap();
            CreateMap<Domain.DTOs.BrandDto.GetBrandDto, Domain.DTOs.BrandDto.GetBrandDtoEnglish>().
                ForMember(dest=>dest.name , opt=>opt.MapFrom(src=>src.nameEn)).ReverseMap();
            CreateMap<Domain.DTOs.BrandDto.GetBrandDto, Domain.DTOs.BrandDto.GetBrandDtoArabic>().
                ForMember(dest => dest.name, opt => opt.MapFrom(src => src.nameAr)).ReverseMap();

            //Product
            CreateMap<listResultDto<Domain.DTOs.productDto.GetProductDto>, listResultDto<Domain.DTOs.productDto.getProductDtoArabic>>().ReverseMap();
            CreateMap<listResultDto<Domain.DTOs.productDto.GetProductDto>, listResultDto<Domain.DTOs.productDto.getProductDtoEnglish>>().ReverseMap();
            CreateMap<resultDto<Domain.DTOs.productDto.GetProductDto>, resultDto<Domain.DTOs.productDto.getProductDtoArabic>>().ReverseMap();
            CreateMap<resultDto<Domain.DTOs.productDto.GetProductDto>, resultDto<Domain.DTOs.productDto.getProductDtoEnglish>>().ReverseMap();
            CreateMap<Domain.DTOs.productDto.GetProductDto, Domain.DTOs.productDto.getProductDtoArabic>().
                 ForMember(dest => 
                 dest.name, 
                 opt => opt.MapFrom(src => src.nameAr)).
                 ForMember(dest => 
                 dest.description, 
                 opt => opt.MapFrom(src => src.descriptionAr)).
                 ForMember(dest => 
                 dest.color, 
                 opt => opt.MapFrom(src => src.colorAr)).ReverseMap();
            
            CreateMap<Domain.DTOs.productDto.GetProductDto, Domain.DTOs.productDto.getProductDtoEnglish>().
                 ForMember(dest =>
                 dest.name, 
                 opt => opt.MapFrom(src => src.nameEn)).
                 ForMember(dest => 
                 dest.description, 
                 opt => opt.MapFrom(src => src.descriptionEn)).
                 ForMember(dest =>
                 dest.color,
                 opt => opt.MapFrom(src => src.colorEn)).ReverseMap();

            CreateMap<listResultDto<Domain.DTOs.productDto.getProductwithImage>, listResultDto<Domain.DTOs.productDto.getProductwithImageArabic>>().ReverseMap();
            CreateMap<listResultDto<Domain.DTOs.productDto.getProductwithImage>, listResultDto<Domain.DTOs.productDto.getProductwithImageEnglish>>().ReverseMap();
            CreateMap<resultDto<Domain.DTOs.productDto.getProductwithImage>, resultDto<Domain.DTOs.productDto.getProductwithImageArabic>>().ReverseMap();
            CreateMap<resultDto<Domain.DTOs.productDto.getProductwithImage>, resultDto<Domain.DTOs.productDto.getProductwithImageEnglish>>().ReverseMap();
            CreateMap<Domain.DTOs.productDto.getProductwithImage, Domain.DTOs.productDto.getProductwithImageArabic>().
                 ForMember(dest =>
                 dest.name,
                 opt => opt.MapFrom(src => src.nameAr)).
                 ForMember(dest =>
                 dest.description,
                 opt => opt.MapFrom(src => src.descriptionAr)).
                 ForMember(dest =>
                 dest.color,
                 opt => opt.MapFrom(src => src.colorAr)).ReverseMap();

            CreateMap<Domain.DTOs.productDto.getProductwithImage, Domain.DTOs.productDto.getProductwithImageEnglish>().
                 ForMember(dest =>
                 dest.name,
                 opt => opt.MapFrom(src => src.nameEn)).
                 ForMember(dest =>
                 dest.description,
                 opt => opt.MapFrom(src => src.descriptionEn)).
                 ForMember(dest =>
                 dest.color,
                 opt => opt.MapFrom(src => src.colorEn)).ReverseMap();

            CreateMap<listResultDto<Domain.DTOs.productDto.getProductCartDto>, listResultDto<Domain.DTOs.productDto.getProductCartDtoArabic>>().ReverseMap();
            CreateMap<listResultDto<Domain.DTOs.productDto.getProductCartDto>, listResultDto<Domain.DTOs.productDto.getProductCartDtoEnglish>>().ReverseMap();
            CreateMap<resultDto<Domain.DTOs.productDto.getProductCartDto>, resultDto<Domain.DTOs.productDto.getProductCartDtoArabic>>().ReverseMap();
            CreateMap<resultDto<Domain.DTOs.productDto.getProductCartDto>, resultDto<Domain.DTOs.productDto.getProductCartDtoEnglish>>().ReverseMap();
            CreateMap<Domain.DTOs.productDto.getProductCartDto, Domain.DTOs.productDto.getProductCartDtoArabic>().
                 ForMember(dest =>
                 dest.name,
                 opt => opt.MapFrom(src => src.nameAr)).
                 ForMember(dest =>
                 dest.description,
                 opt => opt.MapFrom(src => src.descriptionAr)).
                 ReverseMap();

            CreateMap<Domain.DTOs.productDto.getProductCartDto, Domain.DTOs.productDto.getProductCartDtoEnglish>().
                 ForMember(dest =>
                 dest.name,
                 opt => opt.MapFrom(src => src.nameEn)).
                 ForMember(dest =>
                 dest.description,
                 opt => opt.MapFrom(src => src.descriptionEn)).
                 ReverseMap();

            #endregion

        }
    }
}
