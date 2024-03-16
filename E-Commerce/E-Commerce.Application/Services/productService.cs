using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class productService : iproductService
    {
        private readonly iproductRepository _productRepository;
        private readonly IMapper _mapper;
        public productService(iproductRepository iproductRepository, IMapper mapper)
        {
            _productRepository = iproductRepository;
            _mapper = mapper;
        }
        public async Task<resultDto<createDto>> createAsync(createDto productDto)
        {
            createDto createdProduct = null;
            try
            {
                if (productDto is null)
                {
                    return new resultDto<createDto>() { Entity = createdProduct, IsSuccess = false, Message = "Entered Values is Empty" };
                }
                Product productEntity = _mapper.Map<Product>(productDto);
                productEntity = await _productRepository.CreateAsync(productEntity);
                productEntity.createdAt= DateTime.Now;
                await _productRepository.SaveChangesAsync();
                createdProduct = _mapper.Map<createDto>(productEntity);
                return new resultDto<createDto>() { Entity = createdProduct, IsSuccess = true, Message = "Created Sucessfully" };

            }
            catch (Exception ex)
            {
                return new resultDto<createDto>() { Entity = createdProduct, IsSuccess = false, Message = ex.Message };

            }
        }

        public async Task<listResultDto<GetProductDto>> GetAllPaginationAsync(int items, int pagenumber, string[] includes = null)
        {
            var allProductsQuery = await _productRepository.GetAllAsync();
            var productsPage = allProductsQuery.Skip(items * (pagenumber - 1)).Take(items).ToList();
            //_mapper.Map<IEnumerable<GetProductDto>>(productsPage);
            listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>();
            productList.entities = _mapper.Map<IEnumerable<GetProductDto>>(productsPage);
            productList.count = allProductsQuery.Count();
            return productList;

        }

        public async Task<resultDto<GetProductDto>> getById(Guid ID)
        {
            GetProductDto returnedProduct = null;
            try
            {
                if (ID == Guid.Empty)
                {
                    return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = false, Message = "ID Not Found" };
                }
                var product = await _productRepository.GetByIdAsync(ID);
                returnedProduct = _mapper.Map<GetProductDto>(product);
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = true, Message = "Returned Sucessfully"};

            }
            catch (Exception ex)
            {
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = true, Message = ex.Message };
            }
        }

        public async Task<listResultDto<GetProductDto>> getbyNameAr(string name)
        {
            var allProductsQuery = await _productRepository.GetAllAsync();
            var products = allProductsQuery.Where(p => p.nameAr == name).ToList();
            listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>();
            productList.entities = _mapper.Map<IEnumerable<GetProductDto>>(products);
            productList.count = products.Count();
            return productList;


        }
        public async Task<listResultDto<GetProductDto>> getbyNameEn(string name)
        {
            var allProductsQuery = await _productRepository.GetAllAsync();
            var products = allProductsQuery.Where(p => p.nameEn == name).ToList();
            listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>();
            productList.entities = _mapper.Map<IEnumerable<GetProductDto>>(products);
            productList.count = products.Count();
            return productList;
        }

        public async Task<resultDto<GetProductDto>> hardDeleteAsync(Guid ID)
        {
            GetProductDto returnedProduct = null;
            try
            {
                if (ID == Guid.Empty)
                {
                    return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = false, Message = "Id Not Found" };

                }
                var productDto = await getById(ID);
                var productEntity = _mapper.Map<Product>(productDto.Entity);
                var deletedProduct = await _productRepository.HardDeleteAsync(productEntity);
                deletedProduct.deletedAt = DateTime.UtcNow;
                await _productRepository.SaveChangesAsync();
                returnedProduct = _mapper.Map<GetProductDto>(productEntity);
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = false, Message = ex.Message };

            }


        }

        public async Task<resultDto<GetProductDto>> softDeleteAsync(Guid ID)
        {
            GetProductDto returnedProduct = null;
            try
            {
                if (ID == Guid.Empty)
                {
                    return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = false, Message = "Id Not Found" };

                }
                var productDto = await getById(ID);
                var productEntity = _mapper.Map<Product>(productDto.Entity);
                productEntity.IsDeleted = true;
                productEntity.deletedAt = DateTime.UtcNow;
                await _productRepository.SaveChangesAsync();
                returnedProduct = _mapper.Map<GetProductDto>(productEntity);
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = false, Message = ex.Message };

            }
        }

        //public async Task<resultDto<updateDto>> updateAsync(updateDto productDto, Guid Id)
        //{
        //    updateDto updatedProduct = null;
        //    try
        //    {
        //        if (productDto is null)
        //        {
        //            return new resultDto<updateDto>() { Entity = updatedProduct, IsSuccess = false, Message = "Entered Values is Empty" };
        //        }
        //        Product productEntity = _mapper.Map<Product>(productDto);
        //        productEntity.Id = Id;
        //        productEntity = await _productRepository.UpdateAsync(productEntity);
        //        productEntity.updatedAt = DateTime.Now;
        //        await _productRepository.SaveChangesAsync();
        //        updatedProduct = _mapper.Map<createDto>(productEntity);
        //        return new resultDto<updateDto>() { Entity = updatedProduct, IsSuccess = true, Message = "Created Sucessfully" };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new resultDto<updateDto>() { Entity = updatedProduct, IsSuccess = false, Message = ex.Message };

        //    }
        //}
    }
}
