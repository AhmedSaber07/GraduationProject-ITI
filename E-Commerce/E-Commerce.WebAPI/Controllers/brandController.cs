using Company.Dtos.ViewResult;
using E_Commerce.Application.Services;
using E_Commerce.Domain.DTOs.BrandDto;
using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.listResultDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class brandController : ControllerBase
    {
        private readonly ibrandService _brandService;

        public brandController(ibrandService brandService)
        {
            this._brandService = brandService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<resultDto<GetBrandDto>>> Get(Guid id)
        {
            if (Guid.Empty == id)
                return NotFound();
            var brand = await _brandService.getById(id);  
            return brand;
        }
        [HttpGet]
        public async Task<ActionResult<listResultDto<GetBrandDto>>> Get()
        {
            return Ok(await _brandService.getAll());
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDto brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _brandService.createAsync(brand);
            return CreatedAtAction(nameof(Get), new { id = brand.id }, brand);
        }
    }
}
