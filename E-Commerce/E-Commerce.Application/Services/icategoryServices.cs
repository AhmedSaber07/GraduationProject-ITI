using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
namespace E_Commerce.Application.Services
{
    public interface icategoryServices
    {
        Task<resultDto<CreateOrUpdateCategoryDto>> createAsync(CreateOrUpdateCategoryDto category);
        Task<resultDto<CreateOrUpdateCategoryDto>> updateAsync(CreateOrUpdateCategoryDto category);
        Task<resultDto<ReadCategoryDto>> hardDeleteAsync(ReadCategoryDto category);
        Task<resultDto<ReadCategoryDto>> softDeleteAsync(ReadCategoryDto category);
        Task<resultDto<getDto>> getById(Guid ID);
         Task<listResultDto<getDto>> getAll();
        Task<listResultDto<getDto>> GetAllChildrenByCategoryId(Guid categoryId);


    }
}
