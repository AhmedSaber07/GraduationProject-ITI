using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.CartDto;
using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public interface ishoppingCartService
    {
        Task<resultDto<CreateOrUpdateDto>> AddTOCart(Guid productId, int quantity, Guid sessionid);
        Task<resultDto<GetCartDto>> DeleteCart(Guid sessionId);
        Task<resultDto<CreateOrUpdateDto>> IncreaseCartProductQuantity(Guid productId, Guid sessionId);
        Task<resultDto<CreateOrUpdateDto>> DecreaseCartProductQuantity(Guid productId, Guid sessionId);
        Task<resultDto<GetCartDto>> RemoveCartItem(Guid sessionId, Guid productId);
        Task<listResultDto<GetCartDto>> GetAllCartItems(Guid sessionId);
    }
}
