using AutoMapper;
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
        private readonly IMapper _mapper;

        public brandController(ibrandService brandService, IMapper mapper)
        {
            this._brandService = brandService;
            _mapper = mapper;
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
            var lan = HttpContext.Request?.Headers["Accept-Language"];

            var allresult = await _brandService.getAll();
            if (lan.Equals("ar"))
            {
                return Ok(_mapper.Map<listResultDto<GetBrandDtoArabic>>(allresult));
            }
            else
            {
                return Ok(_mapper.Map<listResultDto<GetBrandDtoEnglish>>(allresult));
            }
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
      
        [HttpDelete("SoftDelete/{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();

            }
            var result = await _brandService.softDeleteAsync(id);

            return Ok(result);
        }
        [HttpDelete("HardDelete/{id:guid}")]
        public async Task<IActionResult> HardDelete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return NotFound();

            }
            var result = await _brandService.HardDeleteAsync(Id);

            return Ok(result);
        }
    }
}
