using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace E_Commerce.Application.Services
{
    public class productService : iproductService
    {
       
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public productService(IUnitOfWork _unit, IMapper mapper)
        {
            this._unit = _unit;
            _mapper = mapper;
        }
        public async Task<resultDto<createDto>> createAsync(createDto productDto)
        {
            createDto createdProduct = null;

            if (productDto is null)
            {
                return new resultDto<createDto>() { Entity = createdProduct, IsSuccess = false, Message = "Entered Values is Empty" };
            }
            Product productEntity = _mapper.Map<Product>(productDto);
            productEntity.Id = Guid.NewGuid();
            foreach (var image in productEntity.Images)
            {
                image.createdAt = DateTime.Now;
            }
            productEntity.createdAt = DateTime.Now;
            productEntity = await _unit.product.CreateAsync(productEntity);
            await _unit.product.SaveChangesAsync();
            createdProduct = _mapper.Map<createDto>(productEntity);
            return new resultDto<createDto>() { Entity = createdProduct, IsSuccess = true, Message = "Created Sucessfully" };

        }
        public async Task<IEnumerable<GetProductDto>> GetAllAsync(string[] includes = null)
        {
            var allProductsQuery = await _unit.product.GetAllAsync();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    allProductsQuery = allProductsQuery.Include(include);
                }
            }
            //listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>
            //{
            //    entities = _mapper.Map<IEnumerable<GetProductDto>>(await allProductsQuery.ToListAsync()),
            //    count = await allProductsQuery.CountAsync()
            //};
            var resultProducts = _mapper.Map<IEnumerable<GetProductDto>>(await allProductsQuery.ToListAsync());
            return resultProducts;

        }
        public async Task<listResultDto<GetProductDto>> GetAllPaginationAsync(int items, int pagenumber, string[] includes = null)
        {
            var allProductsQuery = await _unit.product.GetAllAsync();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    allProductsQuery = allProductsQuery.Include(include);
                }
            }
            var productsPage = await allProductsQuery.Skip(items * (pagenumber - 1)).Take(items).ToListAsync();
            //_mapper.Map<IEnumerable<GetProductDto>>(productsPage);
            listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>
            {
                entities = _mapper.Map<IEnumerable<GetProductDto>>(productsPage),
                count = await allProductsQuery.CountAsync()
            };
            return productList;

        }
        
        public async Task<List<GetProductDto>> GetAllPaginationAsyncByCategoryID(int items, int pagenumber,Guid categoryid, string[] includes = null)
        {
            var allProductsQuery = await _unit.product.GetAllAsync();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    allProductsQuery = allProductsQuery.Include(include).Where(p=>p.categoryId==categoryid);
                }
            }
            var productsPage = await allProductsQuery.Skip(items * (pagenumber - 1)).Take(items).ToListAsync();
            //_mapper.Map<IEnumerable<GetProductDto>>(productsPage);
            //listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>
            //{
            //    entities = _mapper.Map<IEnumerable<GetProductDto>>(productsPage),
            //    count = await allProductsQuery.CountAsync()
            //};
            List<GetProductDto> productListDto = _mapper.Map<List<GetProductDto>>(productsPage);
            return productListDto;

        }

        public async Task<resultDto<GetProductDto>> getById(Guid Id, string[] includes = null)
        {
            GetProductDto returnedProduct = null;
            try
            {
                if (Id == Guid.Empty)
                {
                    return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = false, Message = "ID Not Found" };
                }
                var product = await _unit.product.GetByIdAsync(Id, includes);
                returnedProduct = _mapper.Map<GetProductDto>(product);
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = true, Message = "Returned Sucessfully" };

            }
            catch (Exception ex)
            {
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = true, Message = ex.Message };
            }
        }

        public async Task<listResultDto<GetProductDto>> getbyNameAr(string nameAr, string[] includes = null)
        {
            var allProductsQuery = await _unit.product.GetAllAsync();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    allProductsQuery = allProductsQuery.Include(include);
                }
            }

            var products = allProductsQuery.Where(p => p.nameAr == nameAr).ToList();
            listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>();
            productList.entities = _mapper.Map<IEnumerable<GetProductDto>>(products);
            productList.count = products.Count();
            return productList;
        }

        public async Task<listResultDto<GetProductDto>> getbyNameEn(string nameEn, string[] includes = null)
        {
            var allProductsQuery = await _unit.product.GetAllAsync();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    allProductsQuery = allProductsQuery.Include(include);
                }
            }
            var products = await allProductsQuery.Where(p => p.nameEn == nameEn).ToListAsync();
            listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>();
            productList.entities = _mapper.Map<IEnumerable<GetProductDto>>(products);
            productList.count = products.Count();
            return productList;
        }
        public async Task<listResultDto<GetProductDto>> getbyStockQuantityAsync(int productStockQuantity, string[] includes = null)
        {
            var allProductsQuery = await _unit.product.GetAllAsync();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    allProductsQuery = allProductsQuery.Include(include);
                }
            }
            var products = await allProductsQuery.Where(p => p.stockQuantity == productStockQuantity).ToListAsync();
            listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>();
            productList.entities = _mapper.Map<IEnumerable<GetProductDto>>(products);
            productList.count = products.Count();
            return productList;
        }
        public async Task<listResultDto<GetProductDto>> getbybrandAsync(Guid brandId, string[] includes = null)
        {
            var allProductsQuery = await _unit.product.GetAllAsync();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    allProductsQuery = allProductsQuery.Include(include);
                }
            }
            var products = await allProductsQuery.Where(p => p.brandId == brandId).ToListAsync();
            listResultDto<GetProductDto> productList = new listResultDto<GetProductDto>()
            {
                entities = _mapper.Map<IEnumerable<GetProductDto>>(products),
                count = products.Count()
            };
            return productList;
        }

        public async Task<resultDto<GetProductDto>> hardDeleteAsync(Guid Id)
        {
            GetProductDto returnedProduct = null;
            try
            {
                if (Id == Guid.Empty)
                {
                    return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = false, Message = "Id Not Found" };

                }
                var productEntity = await _unit.product.GetByIdAsync(Id);
                var deletedProduct = await _unit.product.HardDeleteAsync(productEntity);
                deletedProduct.deletedAt = DateTime.UtcNow;
                await _unit.product.SaveChangesAsync();
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
                var productEntity = await _unit.product.GetByIdAsync(ID);
                productEntity.IsDeleted = true;
                productEntity.deletedAt = DateTime.UtcNow;
                await _unit.product.SaveChangesAsync();
                returnedProduct = _mapper.Map<GetProductDto>(productEntity);
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new resultDto<GetProductDto>() { Entity = returnedProduct, IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<resultDto<updateDto>> updateStockQuantityAsync(int productStockQuantity, Guid Id)
        {
            updateDto updatedProductStockQuantity = null;
            if (Id == Guid.Empty)
            {
                return new resultDto<updateDto>() { Entity = updatedProductStockQuantity, IsSuccess = false, Message = "Id Not Found" };
            }
            var productEntity = await _unit.product.GetByIdAsync(Id);
            productEntity.updatedAt = DateTime.Now;
            productEntity.stockQuantity += productStockQuantity;
            await _unit.product.SaveChangesAsync();
            updatedProductStockQuantity = _mapper.Map<updateDto>(productEntity);
            return new resultDto<updateDto>() { Entity = updatedProductStockQuantity, IsSuccess = true, Message = "Updated Sucessfully" };

        }
        public async Task<resultDto<updateDto>> updatePriceAsync(decimal productPrice, Guid Id)
        {
            updateDto updatedProductStockQuantity = null;
            if (Id == Guid.Empty)
            {
                return new resultDto<updateDto>() { Entity = updatedProductStockQuantity, IsSuccess = false, Message = "Id Not Found" };
            }
            var productEntity = await _unit.product.GetByIdAsync(Id);
            productEntity.updatedAt = DateTime.Now;
            productEntity.price = productPrice;
            await _unit.product.SaveChangesAsync();
            updatedProductStockQuantity = _mapper.Map<updateDto>(productEntity);
            return new resultDto<updateDto>() { Entity = updatedProductStockQuantity, IsSuccess = true, Message = "Updated Sucessfully" };

        }
        public async Task<resultDto<updateDto>> updateAsync(updateDto productDto, Guid Id)
        {
            updateDto updatedProduct = null;

            if (productDto is null)
            {
                return new resultDto<updateDto>() { Entity = updatedProduct, IsSuccess = false, Message = "Entered Values is Empty" };
            }
            Product productEntity = _mapper.Map<Product>(productDto);
            productEntity.Id = Id;
            productEntity = await _unit.product.UpdateAsync(productEntity);
            productEntity.updatedAt = DateTime.Now;
            await _unit.product.SaveChangesAsync();
            updatedProduct = _mapper.Map<updateDto>(productEntity);
            return new resultDto<updateDto>() { Entity = updatedProduct, IsSuccess = true, Message = "Created Sucessfully" };

        }

        public async Task<bool> ProductExist(Guid Id)
        {
            return await _unit.product.EntityExist(Id);
        }

        
    }
}
