﻿using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain;
using E_Commerce.Domain.DTOs.BrandDto;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;
using System.ComponentModel;
//using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly iproductService _productService;
        private readonly IMapper _mapper;
        public ProductController(iproductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;

        }
        // GET
        [HttpGet]

        public async Task<ActionResult<listResultDto<GetProductDto>>> GetallPagination(int items, int pagenumber, [FromBody] string[] includes = null)
        {
            var language = HttpContext.Request?.Headers["Accept-Language"];
            var result = await _productService.GetAllPaginationAsync(items, pagenumber, includes);
            if (language.Equals("ar"))
            {
                return Ok(_mapper.Map<listResultDto<getProductDtoArabic>>(result));
            }
            else
            {
                return Ok(_mapper.Map<listResultDto<getProductDtoEnglish>>(result));
            }
            
        }
        [HttpGet("getall",Name = "Getall")]
        public async Task<ActionResult<List<getProductDtoArabic>>> Getall()
        {
            //var language = HttpContext.Request?.Headers["Accept-Language"];
            //var result = await _productService.GetAllAsync(includes);
            //if (language.Equals("ar"))
            //{
            //    return Ok(_mapper.Map<List<getProductDtoArabic>>(result));
            //}
            //else
            //{
            //    return Ok(_mapper.Map<List<getProductDtoEnglish>>(result));
            //}

            return Ok(await _productService.GetAllAsync(["Images","Reviews"]));
        }

        [HttpGet("{id:guid}", Name = "GetByProductId")]
        public async Task<ActionResult<resultDto<GetProductDto>>> GetOneById(Guid id)
        {
            if (id != Guid.Empty)
            {
                var language = HttpContext.Request?.Headers["Accept-Language"];
                var product = await _productService.getById(id);
                if (product is not null)
                {
                    if (language.Equals("ar"))
                    {
                        return Ok(_mapper.Map<resultDto<getProductwithImageArabic>>(product));
                    }
                    else if(language.Equals("en"))
                    {
                        return Ok(_mapper.Map<resultDto<getProductwithImageEnglish>>(product));
                    }
                    else
                    {
                        return Ok(product);
                    }
                }
                else
                {
                    return NotFound();

                }
            }
            return NotFound();
        }

        [HttpGet("ArbicName/{nameAr:alpha}", Name = "GetByArbicName")]
        public async Task<ActionResult<listResultDto<GetProductDto>>> GetByArbicName(string nameAr, [FromBody] string[] includes = null)
        {
            if (nameAr != string.Empty)
            {
                var language = HttpContext.Request?.Headers["Accept-Language"];
                var products = await _productService.getbyNameAr(nameAr, includes);
                if (products is not null)
                {
                    if (language.Equals("ar"))
                    {
                        return Ok(_mapper.Map<listResultDto<getProductDtoArabic>>(products));
                    }
                    else
                    {
                        return Ok(_mapper.Map<listResultDto<getProductDtoEnglish>>(products));
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
        [HttpGet("EnglishName/{nameEn:alpha}", Name = "GetByEnglishName")]
        public async Task<ActionResult<listResultDto<GetProductDto>>> GetByEnglishName(string nameEn, [FromBody] string[] includes = null)
        {
            if (nameEn != string.Empty)
            {
                var language = HttpContext.Request?.Headers["Accept-Language"];
                var products = await _productService.getbyNameEn(nameEn, includes);
                if (products is not null)
                {
                    if (language.Equals("ar"))
                    {
                        return Ok(_mapper.Map<listResultDto<getProductDtoArabic>>(products));
                    }
                    else
                    {
                        return Ok(_mapper.Map<listResultDto<getProductDtoEnglish>>(products));
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        [HttpGet("StockQuantity/{productStockQuantity:int}", Name = "GetByProductStockQuantity")]
        public async Task<ActionResult<listResultDto<GetProductDto>>> GetByProductStockQuantity(int productStockQuantity, [FromBody] string[] includes = null)
        {
            if (productStockQuantity != 0)
            {
                var language = HttpContext.Request?.Headers["Accept-Language"];
                var products = await _productService.getbyStockQuantityAsync(productStockQuantity, includes);
                if (products is not null)
                {
                    if (language.Equals("ar"))
                    {
                        return Ok(_mapper.Map<listResultDto<getProductDtoArabic>>(products));
                    }
                    else
                    {
                        return Ok(_mapper.Map<listResultDto<getProductDtoEnglish>>(products));
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        [HttpGet("BrandId/{brandId:guid}", Name = "GetProductsbyBrandAsync")]
        public async Task<ActionResult<listResultDto<GetProductDto>>> GetProductsbyBrandAsync(Guid brandId, [FromBody] string[] includes = null)
        {
            if (brandId != Guid.Empty)
            {
                var language = HttpContext.Request?.Headers["Accept-Language"];
                var products = await _productService.getbybrandAsync(brandId, includes);
                if (products is not null)
                {
                    if (language.Equals("ar"))
                    {
                        return Ok(_mapper.Map<listResultDto<getProductDtoArabic>>(products));
                    }
                    else
                    {
                        return Ok(_mapper.Map<listResultDto<getProductDtoEnglish>>(products));
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }



        // POST 
        [HttpPost]
        public async Task<ActionResult<resultDto<GetProductDto>>> Addproduct([FromBody] createDto productDto)
        {
            if (ModelState.IsValid)
            {
                var resultProduct = await _productService.createAsync(productDto);
                return Created("Product", resultProduct);
            }
            return BadRequest(ModelState);

        }

        //PUT 
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<resultDto<updateDto>>> UpdateProduct(Guid Id, [FromBody] updateDto productDto)
        {
            if (await _productService.ProductExist(Id))
            {
                if (ModelState.IsValid)
                {
                    var resultProduct = await _productService.updateAsync(productDto, Id);
                    return Created("Product", resultProduct);
                }
                return BadRequest(ModelState);
            }
            return NotFound();
        }
        [HttpPut("{Id:guid}/price")]
        public async Task<ActionResult<resultDto<updateDto>>> UpdateProductPrice(Guid Id, decimal price)
        {

            if (await _productService.ProductExist(Id))
            {
                if (ModelState.IsValid)
                {
                    var resultProduct = await _productService.updatePriceAsync(price, Id);
                    return Created("Product", resultProduct);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{Id:guid}/Product")]
        public async Task<ActionResult<resultDto<updateDto>>> UpdateProduct([FromBody] updateDto update,Guid Id)
        {

            if (await _productService.ProductExist(Id))
            {
                if (ModelState.IsValid)
                {
                    var resultProduct = await _productService.updateAsync(update,Id);
                    return Created("Product", resultProduct);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{Id:guid}/stockQuantity/{productStockQuantity:int}")]
        public async Task<ActionResult<resultDto<updateDto>>> updateStockQuantityAsync(Guid Id, int productStockQuantity)
        {

            if (await _productService.ProductExist(Id))
            {
                if (ModelState.IsValid)
                {
                    var resultProduct = await _productService.updateStockQuantityAsync(productStockQuantity, Id);
                    return Created("Product", resultProduct);
                }
            }
            return BadRequest(ModelState);
        }

        //DELETE
        [HttpDelete("HardDelete/{Id:guid}")]
        public async Task<IActionResult> HardDelete(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var product = await _productService.hardDeleteAsync(Id);
                if (product is not null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }   // problem in product image delete on cascade
        [HttpDelete("SoftDelete/{Id:guid}")]
        public async Task<ActionResult<resultDto<GetProductDto>>> SoftDelete(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                var product = await _productService.softDeleteAsync(Id);
                if (product is not null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
    }
}
