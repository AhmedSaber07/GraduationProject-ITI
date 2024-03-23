using AutoMapper;
using E_Commerce.Domain.Models;
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

            // product-Image
            CreateMap<Domain.DTOs.ProductImageDto.CreateDto, ProductImage>().ReverseMap();
            CreateMap<Domain.DTOs.ProductImageDto.CreateWithProductDto, ProductImage>().ReverseMap();
            CreateMap<Domain.DTOs.ProductImageDto.UpdateDto, ProductImage>().ReverseMap();
            CreateMap<Domain.DTOs.ProductImageDto.GetProductImageDto, ProductImage>().ReverseMap();

            // review
            CreateMap<Domain.DTOs.ReviewDto.CreateDto, Review>().ReverseMap();
            CreateMap<Domain.DTOs.ReviewDto.UpdateDto, Review>().ReverseMap();
            CreateMap<Domain.DTOs.ReviewDto.GetReviewDto, Review>().ReverseMap();

            // order-item
            CreateMap<Domain.DTOs.OrderItemDto.CreateDto, orderItem>().ReverseMap();
            CreateMap<Domain.DTOs.OrderItemDto.UpdateDto, orderItem>().ReverseMap();
            CreateMap<Domain.DTOs.OrderItemDto.GetOrderItemDto, orderItem>().ReverseMap();


            //category
            CreateMap<Domain.DTOs.CategoryDto.CreateOrUpdateCategoryDto, Category>().ReverseMap();
            CreateMap<Domain.DTOs.CategoryDto.ReadCategoryDto, Category>().ReverseMap();
            CreateMap<Domain.DTOs.CategoryDto.getDto, Category>().ReverseMap();

            //shopping-cart
            CreateMap<Domain.DTOs.CartDto.CreateOrUpdateDto, Cart>().ReverseMap();
            CreateMap<Domain.DTOs.CartDto.GetCartWithProductsDto, Cart>().ReverseMap();
            CreateMap<Domain.DTOs.CartDto.GetCartDto, Cart>().ReverseMap();

            // order
            CreateMap<Domain.DTOs.OrderDto.CreateOrUpdateDto, Order>().ReverseMap();
            CreateMap<Domain.DTOs.OrderDto.GetOrderDto, Order>().ReverseMap();

            //brand

            CreateMap<Domain.DTOs.BrandDto.CreateDto, Brand>().ReverseMap();
            CreateMap<Domain.DTOs.BrandDto.GetBrandDto, Brand>().ReverseMap();
            CreateMap<Domain.DTOs.BrandDto.UpdateDto, Brand>().ReverseMap();

            //user
            CreateMap<Domain.DTOs.UserAccount.AddressDto, MyUser>().ReverseMap();


        }
    }
}
