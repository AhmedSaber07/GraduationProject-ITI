using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain.DTOs.productDto;
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
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> Get()
        {
            return Ok(await _productService.GetAllPaginationAsync(1, 1));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> Get(Guid id)
        {
            if (id != null)
            {
                return Ok(await _productService.getById(id));
            }
            return NotFound();
        }

        // POST api/<ProductController>
        //[HttpPost("/add")]
        //public async Task<ActionResult<resultDto<createDto>>> Post(createDto productDto)
        //{

        //        var x =await _productService.createAsync(productDto);
        //        return Ok(x);

        //}

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
