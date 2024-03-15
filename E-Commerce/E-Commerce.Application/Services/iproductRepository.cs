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
        Task<resultDto<updateDto>> updateAsync(createDto product);
        Task<resultDto<getProductDto>> hardDeleteAsync(getProductDto product);
        Task<resultDto<getProductDto>> softDeleteAsync(getProductDto product);
        Task<listResultDto<getProductDto>> GetAllPaginationAsync(int items, int pagenumber, string[] includes = null);
        Task<resultDto<getProductDto>> getById(Guid ID);
        Task<listResultDto<getProductDto>> getbyName(string name);

    }
}
