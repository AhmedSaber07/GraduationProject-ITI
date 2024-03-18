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
        Task<resultDto<createDto>> createAsync(createDto product);  //Done
        Task<resultDto<updateDto>> updateAsync(updateDto product, Guid Id);
        Task<resultDto<updateDto>> updateStockQuantityAsync(int productStockQuantity, Guid Id); // Done
        Task<resultDto<updateDto>> updatePriceAsync(decimal price, Guid Id); //Done
        Task<resultDto<GetProductDto>> hardDeleteAsync(Guid Id); //Done
        Task<resultDto<GetProductDto>> softDeleteAsync(Guid Id); //Done
        Task<listResultDto<GetProductDto>> GetAllPaginationAsync(int items, int pagenumber, string[] includes = null); //Done
        Task<resultDto<GetProductDto>> getById(Guid ID, string[] includes = null); //Done
        Task<listResultDto<GetProductDto>> getbyNameAr(string nameAr, string[] includes = null); //Done
        Task<listResultDto<GetProductDto>> getbyNameEn(string nameEn, string[] includes = null); //Done
        Task<listResultDto<GetProductDto>> getbyStockQuantityAsync(int productStockQuantity, string[] includes = null); //Done
        Task<listResultDto<GetProductDto>> getbybrandAsync(Guid Id, string[] includes = null); //Done
        Task<bool> ProductExist(Guid Id);


    }
}
