using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Services;
using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly icategoryServices icategoryServices;
        private readonly IMapper _mapper;

        public CategoryController(icategoryServices icategoryServices, IMapper mapper)
        {
            this.icategoryServices = icategoryServices;
            _mapper = mapper;
        }

      
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult< resultDto<CreateOrUpdateCategoryDto>>> GetById(Guid id)
        {
            var category = await icategoryServices.getById(id);
            if (category == null)
            {
                return NotFound();
            }
            
            return category;
        }
        [HttpGet("GetAllChildrenById/{id:guid}")]
        public async Task<ActionResult<List<getDto>>> GetAllChildrenById(Guid id)
        {
            var language = HttpContext.Request?.Headers["Accept-language"];
            var category = await  icategoryServices.GetAllChildrenByCategoryId(id);

            if (category == null)
            {
                return NotFound();
            }
            if (language.Equals("ar"))
            {
                return Ok(_mapper.Map<List<getDtoArabic>>(category));
            }
            else
            {
                return Ok(_mapper.Map<List<getDtoEnglish>>(category));
            }

           // return category;
        }
        [HttpGet("Getall1")]
        public async Task<ActionResult<listResultDto<getDto>>> Getall1()
        {
            var language = HttpContext.Request?.Headers["Accept-language"];

            var categorys = await icategoryServices.getAll();
            if (language.Equals("ar"))
            {
                return Ok(_mapper.Map<List<getDtoArabic>>(categorys));
            }
            else
            {
                return Ok(_mapper.Map<List<getDtoEnglish>>(categorys));
            }
        }
        [HttpGet("UpdatedGetall")]
        public async Task<IActionResult> UpdatedGetall ()
        {
            var language = HttpContext.Request?.Headers["Accept-language"];
            var categorys= await icategoryServices.getAll2();

            if (language.Equals("ar"))
            {
                return Ok(_mapper.Map<List<getDtoArabic>>(categorys));
            }
            else
            {
                return Ok(_mapper.Map<List<getDtoEnglish>>(categorys));
            }
        }
        [HttpGet("getAllCattegoriesWtihProducts")]
        public async Task<ActionResult<List<getCategorywithProducts>>> getAllCattegoriesWtihProducts()
        {
            var language = HttpContext.Request?.Headers["Accept-language"];
            var categorys = await icategoryServices.getAllCattegoriesWtihProducts();
            if (language.Equals("ar"))
            {
                return Ok(_mapper.Map<List<getCategorywithProductsArabic>>(categorys));
            }
            else
            {
                return Ok(_mapper.Map<List<getCategorywithProductsEnglish>>(categorys));
            }
        }
        [HttpGet("getAllProductsByCategoryId/{id:guid}")]
        public async Task<ActionResult<List<getProductwithImage>>> getAllProductsByCategoryId(Guid id)
        {
            var language = HttpContext.Request?.Headers["Accept-language"];
            var categorys = await icategoryServices.getAllProductsByCategoryId(id);

            if (language.Equals("ar"))
            {
                return Ok(_mapper.Map<List<getProductwithImageArabic>>(categorys));
            }
            else
            {
                return Ok(_mapper.Map<List<getProductwithImageEnglish>>(categorys));
            }
        }
        [HttpPost]      
        public async Task<ActionResult<resultDto<CreateOrUpdateCategoryDto>>> Post([FromBody] CreateOrUpdateCategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

         var x= await icategoryServices.createAsync(category);
            return x;
          
        }
        [HttpDelete("SoftDelete/{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();

            }
            var result=  await icategoryServices.softDeleteAsync(id);

            return Ok(result);
        }
        [HttpDelete("HardDelete/{id:guid}")]
        public async Task<IActionResult> HardDelete(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return NotFound();
              
            }
           var result= await icategoryServices.HardDeleteAsync(Id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateCategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(500,"Enter Vaild Data");
            }

            await icategoryServices.updateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.id }, category);
        }
    }
}
