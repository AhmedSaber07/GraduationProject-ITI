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
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly iproductService _productService;
        public ProductController(iproductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<listResultDto<GetProductDto>>> Getall(int items, int pagenumber,[FromBody] string[] includes = null)
        {
            var x = await _productService.GetAllPaginationAsync(items, pagenumber,includes);
            return Ok(x);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> Getone(Guid id,[FromBody] string[] includes = null)
        {
            if (id != Guid.Empty)
            {
                var x = await _productService.getById(id,includes);
                return Ok(x.Entity);
            }
            return NotFound();
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> addproduct(createDto productDto)
        {
            if (ModelState.IsValid)
            {
                var resultProduct = await _productService.createAsync(productDto);
                return Created("Product", resultProduct );
            }
            return BadRequest(ModelState);

        }

        // PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
