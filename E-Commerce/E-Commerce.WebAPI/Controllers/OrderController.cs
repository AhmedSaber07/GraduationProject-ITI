using AutoMapper;
using Company.Dtos.ViewResult;
using E_Commerce.Application.Services;
using E_Commerce.Domain.DTOs.OrderDto;
using E_Commerce.Domain.listResultDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly iorderService _orderservice;
        private readonly IMapper _mapper;
        public OrderController(iorderService orderService, IMapper mapper)
        {
            _orderservice = orderService;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<listResultDto<getOrdersWithoutItems>>> GetallOrders()
        {
            return Ok(await _orderservice.GetAllOrders());
            //var language = HttpContext.Request?.Headers["Accept-language"];
            //var orders = await _orderservice.GetAllOrders();
            //if (language.Equals("ar"))
            //{
            //    return Ok(_mapper.Map<listResultDto<getOrdersWithoutItemsArabic>>(orders));
            //}
            //else
            //{
            //    return Ok(_mapper.Map<listResultDto<getOrdersWithoutItemsEnglish>>(orders));
            //}
        }
        [HttpGet("GetUserOrders")]
        public async Task<ActionResult<listResultDto<GetOrderDto>>> getUserOrders(string email)
        {
            if (email != string.Empty)
            {
                var language = HttpContext.Request?.Headers["Accept-language"];
                var orders = await _orderservice.getUserOrders(email);
                if (orders is not null)
                {
                    if (language.Equals("ar"))
                    {
                        return Ok(_mapper.Map<listResultDto<GetOrderDtoArabic>>(orders));
                    }
                    else
                    {
                        return Ok(_mapper.Map<listResultDto<GetOrderDtoEnglish>>(orders));
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
        [HttpGet("GetItemsOfOrder")]
        public async Task<ActionResult<listResultDto<GetOrderDto>>> GetItemsOfOrder(int OrderNumber)
        {
            if (OrderNumber > 10000000-1)
            {
                var orderItems = await _orderservice.getItemsOfOrder(OrderNumber);
                if (orderItems is not null)
                {
                    return Ok(orderItems);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
        [HttpGet("GetOrderById")]
        public async Task<ActionResult<resultDto<GetOrderISDeletedDto>>> getOrderById(Guid orderId)
        {
            if (orderId != Guid.Empty)
            {
                var language = HttpContext.Request?.Headers["Accept-language"];
                var orders = await _orderservice.getOrderById(orderId);
                if (orders is not null)
                {
                    if (language.Equals("ar"))
                    {
                        return Ok(_mapper.Map<resultDto<GetOrderISDeletedDtoArabic>>(orders));
                    }
                    else if(language.Equals("en"))
                    {
                        return Ok(_mapper.Map<resultDto<GetOrderISDeletedDtoEnglish>>(orders));
                    }
                    else
                    {
                        return Ok(orders);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> CreateOrder(string email, string transactionId, Guid sessionId)
        {
            //var _ShoppingCartSessionId = getsessionId();
            if (email != string.Empty || transactionId != string.Empty)
            {
                var resultOrder = await _orderservice.createOrder(email, transactionId, sessionId);
                return Ok(resultOrder);
            }
            return BadRequest();
        }

        [HttpPut("{orderId}/{productId}/{quantity}")]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> updateOrderItemQuantity(Guid orderId, Guid productId, int quantity)
        {
            if (orderId != Guid.Empty || productId != Guid.Empty || quantity < 0)
            {
                var resultOrder = await _orderservice.updateOrderItemQuantity(orderId, productId, quantity);
                return Ok(resultOrder);
            }
            return BadRequest();
        }
        [HttpPut("ChangeOrderState/{ordernumber:int}/{state:int}")]
        public async Task<ActionResult<resultDto<CreateOrUpdateDto>>> ChangeOrderState(int ordernumber,int state)
        {
            if (ordernumber > 100000000 - 1 || state <= 5 && state >= 1)
            {
                var resultOrder = await _orderservice.orderStateChange(ordernumber,state);
                return Ok(resultOrder);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<resultDto<GetOrderDto>>> DeleteOrder(Guid id)
        {
            if (id != Guid.Empty)
            {
                return (await _orderservice.deleteOrder(id));
            }
            return NotFound();
        }
    }
}
