using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.BrandDto;
using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.listResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public interface ibrandService
    {
        Task<resultDto<CreateDto>> createAsync(CreateDto brand);    
        Task<resultDto<GetBrandDto>> softDeleteAsync(Guid id);
        Task<resultDto<GetBrandDto>> getById(Guid ID);
        Task<listResultDto<GetBrandDto>> getAll();
       Task<resultDto<GetBrandDto>> HardDeleteAsync(Guid id);
        Task<List<getBrandForDropdown>> getAlldropdown();


    }
}
