using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.OrderDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public interface iorderService
    {
        Task<resultDto<CreateOrUpdateDto>> createOrder(Guid userId, Guid paymentId, Guid SessionId);


        Task<resultDto<GetOrderDto>> deleteOrder(Guid orderId);

        //public  async Task<resultDto<CreateOrUpdateDto>> updateOrder(Guid orderId)
        //{

        //}
        Task<resultDto<GetOrderDto>> orderById(Guid orderId);

        Task<listResultDto<GetOrderDto>> userOrders(Guid userId);
        
    }
}
