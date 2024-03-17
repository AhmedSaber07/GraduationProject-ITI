using Company.Dtos.ViewResult;
using E_Commerce.Application.Services;
using E_Commerce.Domain.DTOs.CategoryDto;
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

      
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult< resultDto<getDto>>> Get(Guid id)
        {
            var category = await icategoryServices.getById(id);
            if (category == null)
            {
                return NotFound();
            }
            category.Entity.children = await icategoryServices.GetAllChildrenByCategoryId(id);
            return category;
        }
        [HttpGet]
        public async Task<ActionResult<listResultDto<getDto>>> Get()
        {        
            return Ok(await icategoryServices.getAll());
        }
        [HttpPost]      
        public async Task<IActionResult> Post([FromBody] CreateOrUpdateCategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           await icategoryServices.createAsync(category);
            return CreatedAtAction(nameof(Get), new { id = category.id }, category);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
          var result=  await icategoryServices.softDeleteAsync(id);

            return Ok(result);
        }
    }
}
