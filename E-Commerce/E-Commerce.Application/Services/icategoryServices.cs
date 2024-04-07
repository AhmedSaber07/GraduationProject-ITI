using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
namespace E_Commerce.Application.Services
{
    public interface icategoryServices
    {
        Task<resultDto<CreateOrUpdateCategoryDto>> createAsync(CreateOrUpdateCategoryDto category);
        Task<resultDto<CreateOrUpdateCategoryDto>> updateAsync(CreateOrUpdateCategoryDto category);
        Task<resultDto<ReadCategoryDto>> HardDeleteAsync(Guid category);
        Task<resultDto<ReadCategoryDto>> softDeleteAsync(Guid category);
        Task<resultDto<CreateOrUpdateCategoryDto>> getById(Guid ID);
         Task<List<getDto>> getAll();
        Task<List<getDto>> GetAllChildrenByCategoryId(Guid categoryId);
       Task<List<getDto>> getAll2();
        Task<List<getCategorywithProducts>> getAllCattegoriesWtihProducts();
        Task<List<getProductwithImage>> getAllProductsByCategoryId(Guid id, int items, int pagenumber);
        Task<List<getCategoryForDropdown>> getAlldropdown();

    }
}
