using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly iproductService _productService;
        public ProductController(iproductService productService)
        {
            _productService = productService;
        }
        // GET
        [HttpGet]
        public async Task<ActionResult<listResultDto<GetProductDto>>> GetallPagination(int items, int pagenumber, [FromBody] string[] includes = null)
        {
            return Ok(await _productService.GetAllPaginationAsync(items, pagenumber, includes));
        }
        [HttpGet("/getall",Name = "Getall")]
        public async Task<ActionResult<List<GetProductDto>>> Getall(string[] includes = null)
        {
            return Ok(await _productService.GetAllAsync(includes));
        }

        [HttpGet("{id:guid}", Name = "GetByProductId")]
        public async Task<ActionResult<resultDto<GetProductDto>>> GetOneById(Guid id)
        {
            if (id != Guid.Empty)
            {
                var product = await _productService.getById(id);
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

        [HttpGet("ArbicName/{nameAr:alpha}", Name = "GetByArbicName")]
        public async Task<ActionResult<listResultDto<GetProductDto>>> GetByArbicName(string nameAr, [FromBody] string[] includes = null)
        {
            if (nameAr != string.Empty)
            {
                var products = await _productService.getbyNameAr(nameAr, includes);
                if (products is not null)
                {
                    return Ok(products);
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
                var products = await _productService.getbyNameEn(nameEn, includes);
                if (products is not null)
                {
                    return Ok(products);
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
                var products = await _productService.getbyStockQuantityAsync(productStockQuantity, includes);
                if (products is not null)
                {
                    return Ok(products);
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
                var products = await _productService.getbybrandAsync(brandId, includes);
                if (products is not null)
                {
                    return Ok(products);
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
        //[HttpPut("{id:guid}")]
        //public async Task<ActionResult<resultDto<updateDto>>> UpdateProduct(Guid Id, [FromBody] updateDto productDto)
        //{
        //    if (await _productService.ProductExist(Id))
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var resultProduct = await _productService.updateAsync(productDto, Id);
        //            return Created("Product", resultProduct);
        //        }
        //        return BadRequest(ModelState);
        //    }
        //    return NotFound();
        //}
        [HttpPut("{id:guid}/price")]
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
        [HttpPut("{id:guid}/Product")]
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
        [HttpPut("{id:guid}/stockQuantity")]
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
        [HttpDelete("HardDelete/{id:guid}")]
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
        [HttpDelete("SoftDelete/{id:guid}")]
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
