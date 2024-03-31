using Company.Dtos.ViewResult;
using E_Commerce.Domain.DTOs.OrderDto;
using E_Commerce.Domain.DTOs.OrderItemDto;
using E_Commerce.Domain.listResultDto;


namespace E_Commerce.Application.Services
{
    public interface iorderService
    {
        Task<listResultDto<getOrdersWithoutItems>> GetAllOrders();
        Task<resultDto<CreateOrUpdateDto>> createOrder(string email, string transactionId, Guid SessionId);
        Task<resultDto<CreateOrUpdateDto>> updateOrderItemQuantity(Guid orderId, Guid productId, int quantity);
        Task<resultDto<GetOrderDto>> deleteOrder(Guid orderId);
        Task<resultDto<GetOrderISDeletedDto>> getOrderById(Guid orderId);
        Task<listResultDto<GetOrderDto>> getUserOrders(string email);
        Task<listResultDto<getOrderItemwithprice>> getItemsOfOrder(int ordernumber);
    }
}
