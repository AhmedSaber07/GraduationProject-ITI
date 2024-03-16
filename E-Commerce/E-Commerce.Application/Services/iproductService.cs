using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface iproductService
    {
        Task<resultDto<createDto>> createAsync(createDto product);
        //Task<resultDto<updateDto>> updateAsync(updateDto product,Guid Id);
        Task<resultDto<GetProductDto>> hardDeleteAsync(Guid ID);
        Task<resultDto<GetProductDto>> softDeleteAsync(Guid ID);
        Task<listResultDto<GetProductDto>> GetAllPaginationAsync(int items, int pagenumber, string[] includes = null);
        Task<resultDto<GetProductDto>> getById(Guid ID);
        Task<listResultDto<GetProductDto>> getbyNameAr(string name);
        Task<listResultDto<GetProductDto>> getbyNameEn(string name);

    }
}
