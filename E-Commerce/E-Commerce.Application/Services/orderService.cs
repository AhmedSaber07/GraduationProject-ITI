using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Services;
using E_Commerce.Domain.DTOs.OrderDto;
using E_Commerce.Domain.DTOs.OrderItemDto;
using E_Commerce.Domain.Enums;
using E_Commerce.Domain.listResultDto;
using E_Commerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.WebAPI.Controllers
{
    public class orderService : iorderService
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<MyUser> _userManager;
        private readonly ishoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        public orderService(IUnitOfWork unit, IMapper mapper, ishoppingCartService shoppingCartService, UserManager<MyUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
        }
        public async Task<resultDto<CreateOrUpdateDto>> createOrder(string email, string transactionid, Guid SessionId)
        {
            CreateOrUpdateDto orderDto = null;
            var allCartData = await _unit.shoppingCart.GetAllAsync();
            var user = await _userManager.FindByEmailAsync(email);
            var cartItems = await allCartData.Where(c => c.SessionId == SessionId).Include(c => c.Product).ToListAsync();
            if (cartItems.Count <= 0)
            {
                return new resultDto<CreateOrUpdateDto> { Entity = null, IsSuccess = false, Message = "cart not found" };
            }
            List<Domain.DTOs.OrderItemDto.CreateDto> orderitemProducts = new List<Domain.DTOs.OrderItemDto.CreateDto>();

            decimal orderTotal = 0;

            foreach (var cartItem in cartItems)
            {
                var product = await _unit.product.GetByIdAsync(cartItem.ProductId);

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
                transactionid = transactionid,
                UserId = user.Id,
                TotalAmount = orderTotal,
                OrderItems = orderitemProducts,
                status_ar = OrderStateAr.في_انتظار,
                status_en = OrderStateEn.Pending
            };

            var orderEntity = _mapper.Map<Order>(ordercreation);
            orderEntity.createdAt = DateTime.Now;

            // Ensure orderEntity.Id is generated properly as a GUID
            orderEntity.Id = Guid.NewGuid();

            foreach (var item in orderEntity.OrderItems)
            {
                item.Id = Guid.NewGuid(); // Assuming OrderItemId is a GUID
                                          // Set the foreign key to orderEntity.Id
                item.OrderId = orderEntity.Id;
            }

            // Save changes
            await _unit.order.CreateAsync(orderEntity);
            await _unit.order.SaveChangesAsync();
            await _shoppingCartService.DeleteCart(SessionId);


            return new resultDto<CreateOrUpdateDto> { Entity = ordercreation, IsSuccess = true, Message = "order created" };
        }
        public async Task<resultDto<GetOrderDto>> deleteOrder(Guid orderId)
        {
            var orderEntity = await _unit.order.GetByIdAsync(orderId, ["OrderItems"]);
            if (orderEntity is null || orderEntity.IsDeleted == true)
            {
                return new resultDto<GetOrderDto> { Entity = null, IsSuccess = false, Message = "Order not Found" };
            }
            orderEntity.IsDeleted = true;
            orderEntity.deletedAt = DateTime.Now;

            foreach (var orderitem in orderEntity.OrderItems)
            {
                var product = await _unit.product.GetByIdAsync(orderitem.ProductId);
                orderitem.IsDeleted = true;
                product.stockQuantity += orderitem.Quantity;
            }
            await _unit.order.SaveChangesAsync();
            var deletedOrderDto = _mapper.Map<GetOrderDto>(orderEntity);

            return new resultDto<GetOrderDto> { Entity = deletedOrderDto, IsSuccess = true, Message = "Deleted Successfully" };
        }
        public async Task<resultDto<CreateOrUpdateDto>> updateOrderItemQuantity(Guid orderId, Guid productId, int quantity)
        {
            var allOrderData = await _unit.order.GetAllAsync();
            var orderEntity = await allOrderData.Where(o => o.Id == orderId).Include(o => o.OrderItems).ToListAsync();
            CreateOrUpdateDto orderDto;
            if (orderEntity.Count <= 0)
            {
                return new resultDto<CreateOrUpdateDto> { Entity = null, IsSuccess = false, Message = "Error In Order Id" };
            }
            decimal orderTotal = 0;
            foreach (var _order in orderEntity)
            {
                foreach (var _OrderItem in _order.OrderItems)
                {
                    var product = await _unit.product.GetByIdAsync(productId);
                    if (_OrderItem.ProductId == productId && product.stockQuantity >= (_OrderItem.Quantity + quantity) && quantity > 0)
                    {
                        _OrderItem.Quantity += quantity;
                        product.stockQuantity -= quantity;
                        _OrderItem.updatedAt = DateTime.Now;
                        await _unit.orderItem.SaveChangesAsync();
                    }
                    orderTotal += product.price * _OrderItem.Quantity;
                }
                _order.updatedAt = DateTime.Now;
                _order.TotalAmount = orderTotal;
                orderDto = _mapper.Map<CreateOrUpdateDto>(_order);
                return new resultDto<CreateOrUpdateDto> { Entity = orderDto, IsSuccess = true, Message = "Item Quantiy increased" };
            }
            return new resultDto<CreateOrUpdateDto> { Entity = null, IsSuccess = false, Message = "error" };


        }
        public async Task<resultDto<GetOrderISDeletedDto>> getOrderById(Guid orderId)
        {
            var orderEntity = await _unit.order.GetByIdAsync(orderId, ["OrderItems"]);
            if (orderEntity is null)
            {
                return new resultDto<GetOrderISDeletedDto> { Entity = null, IsSuccess = false, Message = "Order not Found" };
            }
            var OrderDto = _mapper.Map<GetOrderISDeletedDto>(orderEntity);

            return new resultDto<GetOrderISDeletedDto> { Entity = OrderDto, IsSuccess = true, Message = "order Retrived Successfully" };

        }
        public async Task<listResultDto<GetOrderDto>> getUserOrders(string email)
        {
            var allordersData = await _unit.order.GetAllAsync();
            var user = await _userManager.FindByEmailAsync(email);
            var userOrderEntity = await allordersData.Where(o => o.UserId == user.Id).ToListAsync();
            if (userOrderEntity is null)
            {
                return new listResultDto<GetOrderDto> { entities = null, count = 0 };
            }
            var OrderDto = _mapper.Map<IEnumerable<GetOrderDto>>(userOrderEntity);

            return new listResultDto<GetOrderDto> { entities = OrderDto, count = OrderDto.Count() };

        }
        public async Task<listResultDto<getOrderItemwithprice>> getItemsOfOrder(int ordernumber)
        {
            var allordersData = await _unit.order.GetAllAsync();
            var userOrderEntity = await allordersData.Where(o => o.OrderNumber == ordernumber).Include(E=>E.OrderItems).ToListAsync();
            if (userOrderEntity is null)
            {
                return new listResultDto<getOrderItemwithprice> { entities = null, count = 0 };
            }
            List<getOrderItemwithprice> orderitemsdto = new List<getOrderItemwithprice>();
            foreach (var _order in userOrderEntity)
            {
                foreach (var _orderItem in _order.OrderItems)
                {
                    var product = await _unit.product.GetByIdAsync(_orderItem.ProductId);
                    getOrderItemwithprice orderitemdto = new getOrderItemwithprice()
                    {
                        price = product.price,
                        productName = product.nameEn,
                        ItemTotalPrice = product.price * _orderItem.Quantity,
                        quantity = _orderItem.Quantity
                    };
                    orderitemsdto.Add(orderitemdto);
                }
            }
            return new listResultDto<getOrderItemwithprice> { entities = orderitemsdto, count = orderitemsdto.Count() };

        }

        public async Task<listResultDto<getOrdersWithoutItems>> GetAllOrders()
        {
            var allordersData = await _unit.order.GetAllAsync();
            var ordersDto = _mapper.Map<List<getOrdersWithoutItems>>(await allordersData.ToListAsync());
            return new listResultDto<getOrdersWithoutItems> { entities = ordersDto, count = ordersDto.Count() };
        }
    }
}
