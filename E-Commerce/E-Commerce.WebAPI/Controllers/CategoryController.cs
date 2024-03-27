using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Services;
using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly icategoryServices icategoryServices;

        public CategoryController(icategoryServices icategoryServices)
        {
            this.icategoryServices = icategoryServices;
        }

      
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult< resultDto<getDto>>> GetById(Guid id)
        {
            var category = await icategoryServices.getById(id);
            if (category == null)
            {
                return NotFound();
            }
            category.Entity.children = await icategoryServices.GetAllChildrenByCategoryId(id);
            return category;
        }
        [HttpGet("GetAllChildrenById/{id:guid}")]
        public async Task<ActionResult<List<getDto>>> GetAllChildrenById(Guid id)
        {
            var category = await  icategoryServices.GetAllChildrenByCategoryId(id);
            if (category == null)
            {
                return NotFound();
            }
           
            return category;
        }
        [HttpGet("Getall1")]
        public async Task<ActionResult<listResultDto<getDto>>> Getall1()
        {
            return Ok(await icategoryServices.getAll());
        }
        [HttpGet("UpdatedGetall")]
        public async Task<IActionResult> UpdatedGetall ()
        {
            return Ok( await icategoryServices.getAll2());
        }
        [HttpGet("getAllCattegoriesWtihProducts")]
        public async Task<ActionResult<List<getCategorywithProducts>>> getAllCattegoriesWtihProducts()
        {
            return Ok(await icategoryServices.getAllCattegoriesWtihProducts());
        }
        [HttpGet("getAllProductsByCategoryId/{id:guid}")]
        public async Task<ActionResult<List<getProductwithImage>>> getAllProductsByCategoryId(Guid id)
        {
            return Ok(await icategoryServices.getAllProductsByCategoryId(id));
        }
        [HttpPost]      
        public async Task<IActionResult> Post([FromBody] CreateOrUpdateCategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           await icategoryServices.createAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.id }, category);
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
                return BadRequest(ModelState);
            }

            await icategoryServices.updateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.id }, category);
        }
    }
}
