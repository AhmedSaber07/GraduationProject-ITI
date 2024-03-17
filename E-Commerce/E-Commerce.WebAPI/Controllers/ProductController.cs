using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
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
        public async Task<ActionResult<listResultDto<GetProductDto>>> Get()
        {
            return Ok(await _productService.GetAllPaginationAsync(1, 1));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                return Ok(await _productService.getById(id));
            }
            return NotFound();
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> addproduct(createDto productDto)
        {

            var x = await _productService.createAsync(productDto);
            return Ok();

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
