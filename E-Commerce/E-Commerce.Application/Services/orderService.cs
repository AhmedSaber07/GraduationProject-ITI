using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain.DTOs.OrderDto;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.WebAPI.Controllers
{
    public class orderService
    {
        private readonly iorderRepository _orderRepository;
        private readonly ishoppingCartRepository _shoppingCartRepository;
        private readonly iproductRepository _productRepository;
        private readonly IMapper _mapper;
        public orderService(iorderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<resultDto<CreateOrUpdateDto>> createOrder(Guid userId, Guid paymentId, Guid sessionId)
        {
            CreateOrUpdateDto orderDto = null;
            var allCartData = await _shoppingCartRepository.GetAllAsync();
            var cartItems = await allCartData.Include(c => c.Product).Where(c => c.sessionId == sessionId).ToListAsync();

            List<Domain.DTOs.OrderItemDto.CreateDto> orderitemProducts = new List<Domain.DTOs.OrderItemDto.CreateDto>();

            decimal orderTotal = 0;

            foreach (var cartItem in cartItems)
            {
                var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
                Domain.DTOs.OrderItemDto.CreateDto productitem = new Domain.DTOs.OrderItemDto.CreateDto();
                productitem.ProductId = cartItem.ProductId;
                productitem.Price = cartItem.Product.price;
                if (product.stockQuantity >= cartItem.Quantity)
                {
                    productitem.Quantity = cartItem.Quantity;
                    product.stockQuantity -= cartItem.Quantity;
                }
                else
                {
                    return new resultDto<CreateOrUpdateDto> { Entity = null, IsSuccess = false, Message = "more than stock quantity" };
                }
                productitem.itemTotalPrice = productitem.Price * productitem.Quantity;
                orderTotal += productitem.itemTotalPrice;
                orderitemProducts.Add(productitem);
            }

            CreateOrUpdateDto ordercreation = new CreateOrUpdateDto()
            {
                PaymentId = paymentId,
                UserId = userId,
                TotalAmount = orderTotal,
                OrderItems = orderitemProducts,
                status_ar = "قيد انتظار الشحن",
                status_en = "Pending For Delivary"
            };

            var orderEntity = _mapper.Map<Order>(ordercreation);
            orderEntity.createdAt = DateTime.Now;
            await _orderRepository.CreateAsync(orderEntity);
            await _orderRepository.SaveChangesAsync();
            return new resultDto<CreateOrUpdateDto> { Entity = ordercreation, IsSuccess = true, Message = "order" };
        }
        public async Task<resultDto<GetOrderDto>> deleteOrder(Guid orderId)
        {
            var orderEntity =await _orderRepository.GetByIdAsync(orderId, ["OrderItems"]);
            if (orderEntity is null)
            {
                return new resultDto<GetOrderDto> { Entity = null, IsSuccess = false, Message = "Order not Found" };
            }
            orderEntity.IsDeleted = true;
            orderEntity.deletedAt= DateTime.Now;

            foreach(var orderitem in orderEntity.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(orderitem.ProductId);
                product.stockQuantity += orderitem.Quantity;
            }
            await _orderRepository.SaveChangesAsync();
            var deletedOrderDto=_mapper.Map<GetOrderDto>(orderEntity);

            return new resultDto<GetOrderDto> { Entity = deletedOrderDto, IsSuccess = true, Message = "Deleted Successfully" };
        }
        //public  async Task<resultDto<CreateOrUpdateDto>> updateOrder(Guid orderId)
        //{

        //}
        public async Task<resultDto<GetOrderDto>> orderById(Guid orderId)
        {
            var orderEntity = await _orderRepository.GetByIdAsync(orderId, ["OrderItems"]);
            if (orderEntity is null)
            {
                return new resultDto<GetOrderDto> { Entity = null, IsSuccess = false, Message = "Order not Found" };
            }
            var OrderDto = _mapper.Map<GetOrderDto>(orderEntity);

            return new resultDto<GetOrderDto> { Entity = OrderDto, IsSuccess = true, Message = "order Retrived Successfully" };

        }
        public async Task<listResultDto<GetOrderDto>> userOrder(Guid userId)
        {
            var allordersData= await _orderRepository.GetAllAsync();

            var userOrderEntity= allordersData.Include(o=>o.OrderItems).Where(o=>o.UserId == userId);
            if (userOrderEntity is null)
            {
                return new listResultDto<GetOrderDto> { entities = null, count=0 };
            }
            var OrderDto = _mapper.Map<IEnumerable<GetOrderDto>>(userOrderEntity);

            return new listResultDto<GetOrderDto> { entities = OrderDto, count = await userOrderEntity.CountAsync() };

        }
    }
}
