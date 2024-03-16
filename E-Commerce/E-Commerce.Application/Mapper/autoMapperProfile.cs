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

            //category
            CreateMap<Domain.DTOs.CategoryDto.CreateOrUpdateCategoryDto, Category>().ReverseMap();
            CreateMap<Domain.DTOs.CategoryDto.ReadCategoryDto, Category>().ReverseMap();

        }
    }
}
